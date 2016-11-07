// ReSharper disable InconsistentNaming
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Savchin.WinApi.Shell
{


    public class PropertySheetExtension : UserControl, IShellExtInit, IShellPropSheetExt, IConvertible
    {
        #region Helpers
        // Fields
        private IntPtr hWndForm;
        private IntPtr hWndPage;
        private IntPtr hPropertySheetPage;

        private bool _showHelpButton;
        internal string[] Files;


        private PROPSHEETPAGE _psp;
        private static readonly ArrayList _extensionReferences = new ArrayList();
        private Bitmap _icon;
        private bool xd8242b1dae713758;
        // Properties
        public Bitmap Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
            }
        }

        public bool ShowHelpButton
        {
            get
            {
                return _showHelpButton;
            }
            set
            {
                _showHelpButton = value;
            }
        }

        public string[] TargetFiles
        {
            get
            {
                return Files;
            }
        }

        [Bindable(true), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
        #endregion

        // Methods
        public PropertySheetExtension()
        {
            Name = "PropertySheetExtension";
            Size = new Size(240, 200);
            ResizeRedraw = true;
            Size = new Size(350, 450);
            if (((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 5)) && (Environment.OSVersion.Version.Minor > 0))
            {
                xd8242b1dae713758 = true;
            }
        }

        #region Interface
        public bool Apply()
        {
            if (User32.SendMessage(hWndForm, 0x46e, hWndPage, IntPtr.Zero).ToInt32() != 1)
            {
                return false;
            }
            return true;
        }

        public void CancelToClose()
        {
            User32.SendMessage(hWndForm, 0x46b, IntPtr.Zero, IntPtr.Zero);
        }

        public void Changed()
        {
            User32.SendMessage(hWndForm, 0x468, hWndPage, IntPtr.Zero);
        }
        public void PressButton(PropertySheetButton button)
        {
            User32.SendMessage(hWndForm, 0x471, (IntPtr)((long)button), IntPtr.Zero);
        }

        /// <summary>
        /// Uns the register extension.
        /// </summary>
        /// <param name="t">The t.</param>
        public static void UnRegisterExtension(Type t)
        {
            string str = RegistryKeyNameAttribute.x0ba0e0095169fb2c(t);
            foreach (TargetExtensionAttribute attribute in TargetExtensionAttribute.AttributesFromType(t))
            {
                if (Enum.IsDefined(typeof(RegistryHive), attribute.rh))
                {
                    attribute.RootKey.DeleteSubKey(attribute.fileExtension + @"\Shellex\PropertySheetHandlers\" + str, false);
                }
                else
                {
                    Registry.ClassesRoot.DeleteSubKey(attribute.ProgID + @"\Shellex\PropertySheetHandlers\" + str, false);
                }
            }

        }
        /// <summary>
        /// Registers the extension.
        /// </summary>
        /// <param name="t">The t.</param>
        public static void RegisterExtension(Type t)
        {
            string str = RegistryKeyNameAttribute.x0ba0e0095169fb2c(t);
            foreach (TargetExtensionAttribute attribute in TargetExtensionAttribute.AttributesFromType(t))
            {
                RegistryKey key;
                if (Enum.IsDefined(typeof(RegistryHive), attribute.rh))
                {
                    key = attribute.RootKey.CreateSubKey(attribute.fileExtension + @"\Shellex\PropertySheetHandlers\" + str);
                }
                else
                {
                    key = Registry.ClassesRoot.CreateSubKey(attribute.ProgID + @"\Shellex\PropertySheetHandlers\" + str);
                }
                key.SetValue(string.Empty, t.GUID.ToString("B"));
                key.Close();
            }
        }

        public void RemovePage()
        {
            User32.SendMessage(hWndForm, 0x466, IntPtr.Zero, hPropertySheetPage);
        }

        public void Reset()
        {
            User32.SendMessage(hWndForm, 0x46d, hWndPage, IntPtr.Zero);
        }
        #endregion

        #region   Protected virtual
        protected virtual bool OnActivate()
        {
            return true;
        }

        protected virtual NotifyResult OnApply()
        {
            return NotifyResult.NoError;
        }

        protected virtual void OnCancel()
        {
        }

        protected virtual void OnCreatePage()
        {
        }

        protected virtual bool OnInitialize()
        {
            return true;
        }

        protected virtual bool OnInitializeEx(IOleDataObject dataObject)
        {
            return true;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            try
            {
                base.OnPaintBackground(pevent);
                if (xd8242b1dae713758 && (x443cc432acaadb1d.IsThemeActive() != 0))
                {
                    IntPtr hTheme = x443cc432acaadb1d.OpenThemeData(base.Handle, "Tab");
                    IntPtr hdc = pevent.Graphics.GetHdc();
                    Rectangle clientRectangle = base.ClientRectangle;
                    x443cc432acaadb1d.DrawThemeBackground(hTheme, hdc, 10, 1, ref clientRectangle, ref clientRectangle);
                    pevent.Graphics.ReleaseHdc(hdc);
                    x443cc432acaadb1d.CloseThemeData(hTheme);
                }
            }
            catch
            {
            }
        }

        protected virtual bool OnQueryCancel()
        {
            return true;
        }

        protected virtual void OnShowHelp()
        {
        }

        protected virtual bool OnValidateChanges()
        {
            return true;
        }
        #endregion

        #region IShellPropSheetExt

        int IShellPropSheetExt.AddPages(IntPtr lpfnAddPage, IntPtr x130fbcecf32fe781)
        {
            try
            {
                CreatePropertySheetPage();

                if (hPropertySheetPage.Equals(0))
                {
                    _extensionReferences.Remove(this);
                    return 1;
                }

                var func = CFunctionPointer.Create(lpfnAddPage, typeof(bool), typeof(IntPtr), typeof(IntPtr));
                if ((bool)func.Invoke(new object[] { hPropertySheetPage, x130fbcecf32fe781 }))
                    return 0;


                _extensionReferences.Remove(this);
                Comctl32.DestroyPropertySheetPage(hPropertySheetPage);
                return 1;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return 1;
            }


        }

        int IShellPropSheetExt.ReplacePage(int x8c79b1dbfea9ffd4, int x424e3e029d675a23, int x130fbcecf32fe781)
        {
            return 0;
        }
        #endregion



        #region CallBacks
        private int PropSheetPageCallback(IntPtr x96e7d32425e52ebf, x879758f4cab61ddd x5bf2804bd1654533, ref PROPSHEETPAGE xa65184d44a47025b)
        {
            if ((x5bf2804bd1654533 != x879758f4cab61ddd.xf22ca9d72ffb7392) && (x5bf2804bd1654533 == x879758f4cab61ddd.xae1bd93d002e5fd5))
            {
                _extensionReferences.Remove(this);
                Marshal.FreeCoTaskMem(_psp.pResource);
                if (_icon != null)
                {
                    _icon.Dispose();
                    _icon = null;
                }
                base.Dispose();
                GC.SuppressFinalize(this);
            }
            return 1;
        }
        private int WndProc(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam)
        {
            if (uMsg == 0x110)
            {
                x0bdae4f1006d9d27.x9d748d087524e2b4(this, 2);
                x443cc432acaadb1d.SetParent(base.Handle, hWnd);
                hWndPage = hWnd;
                hWndForm = x443cc432acaadb1d.GetParent(hWnd);
                base.Visible = true;
                return 1;
            }
            if (uMsg == 15)
            {
                base.Invalidate(true);
                return 0;
            }
            if (uMsg == 0x4e)
            {
                xd0b43587806b92d7 xdbbd = (xd0b43587806b92d7)Marshal.PtrToStructure(lParam, typeof(xd0b43587806b92d7));
                if (xdbbd.x8baacca1e4006e06.x9035cf16181332fc == -200)
                {
                    bool flag = OnActivate();
                    x443cc432acaadb1d.x1240f363affcf768(hWnd, 0, flag ? IntPtr.Zero : ((IntPtr)(-1)));
                    return 1;
                }
                if (xdbbd.x8baacca1e4006e06.x9035cf16181332fc == -202)
                {
                    NotifyResult result = OnApply();
                    x443cc432acaadb1d.x1240f363affcf768(hWnd, 0, (IntPtr)((long)result));
                    return 1;
                }
                if (xdbbd.x8baacca1e4006e06.x9035cf16181332fc != -203)
                {
                    if (xdbbd.x8baacca1e4006e06.x9035cf16181332fc != -205)
                    {
                        if (xdbbd.x8baacca1e4006e06.x9035cf16181332fc == -201)
                        {
                            bool flag2 = OnValidateChanges();
                            x443cc432acaadb1d.x1240f363affcf768(hWnd, 0, flag2 ? IntPtr.Zero : ((IntPtr)1));
                            return 1;
                        }
                        if (xdbbd.x8baacca1e4006e06.x9035cf16181332fc == -209)
                        {
                            bool flag3 = OnQueryCancel();
                            x443cc432acaadb1d.x1240f363affcf768(hWnd, 0, flag3 ? IntPtr.Zero : ((IntPtr)1));
                            return 1;
                        }
                    }
                    else
                    {
                        OnShowHelp();
                    }
                }
                else
                {
                    OnCancel();
                }
            }
            else if (uMsg == 5)
            {
                base.Width = lParam.ToInt32() & 0xffff;
                base.Height = lParam.ToInt32() / 0x10000;
                OnCreatePage();
            }
            return 0;
        }
        #endregion

        private IntPtr CreatePropertySheetPage()
        {
            if (hPropertySheetPage == IntPtr.Zero)
            {
                _psp = new PROPSHEETPAGE();
                _psp.hInstance = IntPtr.Zero;
                _psp.dwSize = Marshal.SizeOf(_psp);
                _psp.dwFlags = pspFlags.PSP_USECALLBACK | pspFlags.PSP_USETITLE | pspFlags.PSP_DLGINDIRECT;
                _psp.lParam = IntPtr.Zero;
                if (RightToLeft == RightToLeft.Yes)
                {
                    _psp.dwFlags |= pspFlags.PSP_RTLREADING;
                }
                if (_icon != null)
                {
                    _psp.dwFlags |= pspFlags.PSP_USEHICON;
                    _psp.hIcon = _icon.GetHicon();
                }
                if (_showHelpButton)
                {
                    _psp.dwFlags |= pspFlags.PSP_HASHELP;
                }
                _psp.pResource = GetDlgTemplate();
                _psp.pfnDlgProc = new DialogProc(WndProc);
                _psp.pfnCallback = new PropSheetPageProc(PropSheetPageCallback);
                _psp.pszTitle = Text;
                hPropertySheetPage = Comctl32.CreatePropertySheetPage(ref _psp);
                if (hPropertySheetPage != IntPtr.Zero)
                {
                    _extensionReferences.Add(this);
                }
            }
            return hPropertySheetPage;
        }



        internal IntPtr GetDlgTemplate()
        {
            var structure = new DLGTEMPLATE();

            structure.cx = 0;
            structure.cy = 0;

            structure.style = DS.SETFONT;
            structure.fontPointSize = 8;
            byte[] source = ConvertByteArray("MS Shell Dlg");
            int num = Marshal.SizeOf(typeof(DLGTEMPLATE));
            IntPtr ptr = Marshal.AllocHGlobal(num + source.Length);
            Marshal.StructureToPtr(structure, ptr, false);
            Marshal.Copy(source, 0, (IntPtr)(((long)ptr) + num), source.Length);
            return ptr;
        }

        #region IConvertible
        ulong IConvertible.ToUInt64(IFormatProvider x83d0c26038874b92)
        {
            return 0L;
        }

        double IConvertible.ToDouble(IFormatProvider x83d0c26038874b92)
        {
            return 0.0;
        }

        object IConvertible.ToType(Type x1739441627a0afcd, IFormatProvider x83d0c26038874b92)
        {
            if (x1739441627a0afcd.Equals(typeof(IntPtr)))
            {
                return CreatePropertySheetPage();
            }
            return null;
        }

        string IConvertible.ToString(IFormatProvider x83d0c26038874b92)
        {
            return string.Empty;
        }

        float IConvertible.ToSingle(IFormatProvider x83d0c26038874b92)
        {
            return 0f;
        }

        long IConvertible.ToInt64(IFormatProvider x83d0c26038874b92)
        {
            return 0L;
        }

        char IConvertible.ToChar(IFormatProvider x83d0c26038874b92)
        {
            return 'a';
        }

        DateTime IConvertible.ToDateTime(IFormatProvider x83d0c26038874b92)
        {
            return DateTime.Now;
        }

        bool IConvertible.ToBoolean(IFormatProvider x83d0c26038874b92)
        {
            return false;
        }

        ushort IConvertible.ToUInt16(IFormatProvider x83d0c26038874b92)
        {
            return 0;
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Boolean;
        }

        decimal IConvertible.ToDecimal(IFormatProvider x83d0c26038874b92)
        {
            return 0M;
        }

        byte IConvertible.ToByte(IFormatProvider x83d0c26038874b92)
        {
            return 0;
        }

        sbyte IConvertible.ToSByte(IFormatProvider x83d0c26038874b92)
        {
            return 0;
        }
        short IConvertible.ToInt16(IFormatProvider x83d0c26038874b92)
        {
            return 0;
        }

        int IConvertible.ToInt32(IFormatProvider x83d0c26038874b92)
        {
            return 0;
        }

        uint IConvertible.ToUInt32(IFormatProvider x83d0c26038874b92)
        {
            return 0;
        }

        #endregion

        int IShellExtInit.Initialize(IntPtr xdceb127ad2fd117b, IntPtr x4b9447f495c6b4a1, IntPtr x9a3377d64b14bf3d)
        {

            IOleDataObject objectForIUnknown = null;
            try
            {
                if (x4b9447f495c6b4a1 != IntPtr.Zero)
                {
                    objectForIUnknown = Marshal.GetObjectForIUnknown(x4b9447f495c6b4a1) as IOleDataObject;
                    if (objectForIUnknown != null)
                    {
                        Files = x443cc432acaadb1d.GetFileList(objectForIUnknown);
                    }
                }
                if (Files == null)
                {
                    Files = new string[0];
                }
                if (!OnInitialize())
                {
                    return x443cc432acaadb1d.x15f59d42384ec1d4;
                }
                if (!OnInitializeEx(objectForIUnknown))
                {
                    return x443cc432acaadb1d.x15f59d42384ec1d4;
                }
            }
            catch
            {
                return x443cc432acaadb1d.x15f59d42384ec1d4;
            }
            finally
            {
                if (objectForIUnknown != null)
                {
                    Marshal.ReleaseComObject(objectForIUnknown);
                }
            }
            return 0;
        }





        internal byte[] ConvertByteArray(string text)
        {
            int length = text.Length;
            byte[] buffer = new byte[(length + 1) * 2];
            char[] chArray = text.ToCharArray();
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                buffer[index++] = (byte)chArray[i];
                buffer[index++] = 0;
            }
            buffer[index++] = 0;
            buffer[index] = 0;
            return buffer;
        }




    }



    // ReSharper restore InconsistentNaming

}
