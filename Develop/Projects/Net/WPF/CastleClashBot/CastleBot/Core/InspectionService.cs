using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using ChristianMoser.WpfInspector.Win32;
using System.Threading;
using Castle.Core;
using CastleBot.Core;

namespace ChristianMoser.WpfInspector.Services
{
    public class InspectionService
    {
        #region Private Members

        private IntPtr _hookHandle;
        private readonly Process32Service _process32Service;
        private readonly uint _wmInspect = NativeMethods.RegisterWindowMessage("WM_INSPECT");

        #endregion

        #region Construction

        public InspectionService()
        {
            if (PlatformHelper.Is64BitProcess)
            {
                _process32Service = ServiceLocator.Resolve<Process32Service>();
                _process32Service.Start32BitProcessHelper();
            }
        }

        #endregion

        public string Inspect(ManagedApplicationInfo applicationInfo)
        {
     
                using (var process = Process.GetProcessById(applicationInfo.ProcessId))
                {
                    if (PlatformHelper.Is64BitProcess && PlatformHelper.IsWow64Process(process.Handle))
                    {
                        // This is a 64-bit process, so we use the 32-bit process helper to inspect the app
                        _process32Service.Inspect(applicationInfo);
                    }
                    else
                    {
                        // The application to inspect has the same type 32/64 bit as we do
                        InspectInternal(applicationInfo);
                    }
                }
                return null;
     
        }

        private void InspectInternal(ManagedApplicationInfo applicationInfo)
        {
            int processId;
            var threadId = (uint)NativeMethods.GetWindowThreadProcessId(applicationInfo.HWnd, out processId);
            using (var process = Process.GetProcessById(processId))
            {
                string version = applicationInfo.RuntimeVersion.Contains("4") ? "40" : "35";
                string hookName = string.Format("Hook{0}_{1}.dll", applicationInfo.Bitness, version);

                IntPtr hInstance = NativeMethods.LoadLibrary(hookName);

                if (hInstance == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                var assm = GetType().Assembly;

                string methodIdentifier = string.Concat(assm.Location,
                                                        "$CastleController.Inspector$Inject");

                int bufLen = (methodIdentifier.Length + 1) * Marshal.SizeOf(typeof(char));
                IntPtr hProcess = NativeMethods.OpenProcess(NativeMethods.ProcessAccessFlags.All, false, processId);
                IntPtr remoteAdress = NativeMethods.VirtualAllocEx(hProcess, IntPtr.Zero, (uint)bufLen,
                                                                   NativeMethods.AllocationType.Commit,
                                                                   NativeMethods.MemoryProtection.ReadWrite);

                if (remoteAdress != IntPtr.Zero)
                {
                    IntPtr address = Marshal.StringToHGlobalUni(methodIdentifier);
                    uint size = (uint)(sizeof(char) * methodIdentifier.Length);

                    int bytesWritten;
                    NativeMethods.WriteProcessMemory(hProcess, remoteAdress, address, size, out bytesWritten);

                    if (bytesWritten == 0)
                    {
                        throw Marshal.GetExceptionForHR(Marshal.GetLastWin32Error());
                    }

                    UIntPtr procAdress = NativeMethods.GetProcAddress(hInstance, "MessageHookProc");

                    if (procAdress == UIntPtr.Zero)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    _hookHandle = NativeMethods.SetWindowsHookEx(NativeMethods.HookType.WH_CALLWNDPROC, procAdress, hInstance, threadId);

                    if (_hookHandle != IntPtr.Zero)
                    {
                        NativeMethods.SendMessage(applicationInfo.HWnd, _wmInspect, remoteAdress, IntPtr.Zero);
                        NativeMethods.UnhookWindowsHookEx(_hookHandle);
                    }
                    else
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }

                    NativeMethods.VirtualFreeEx(process.Handle, remoteAdress, bufLen, NativeMethods.AllocationType.Release);
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                NativeMethods.FreeLibrary(hInstance);
            }
        }

    }
}
