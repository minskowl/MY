using System;
using System.Runtime.InteropServices;
using Savchin.Extensions.ContextMenus;
using Savchin.WinApi.Shell;

namespace Savchin.Extensions.Core
{
    /// <summary>
    /// Instalator
    /// </summary>
    public class Instalator
    {
        [ComRegisterFunction]
        public static void Register(Type t)
        {
            ExtensionInstaller.Register(typeof(AllFileContextMenuExtension));
            ExtensionInstaller.Register(typeof(ExeFileContextMenuExtension));
            ExtensionInstaller.Register(typeof(SlnFileContextMenuExtension));
            ExtensionInstaller.Register(typeof(DescriptionControl));

            //            ExtensionInstaller.Register(typeof(TestControl1));
            ExtensionInstaller.ApplyChanges();
        }

        [ComUnregisterFunction]
        public static void UnRegister(Type t)
        {
            ExtensionInstaller.UnRegister(typeof(AllFileContextMenuExtension));
            ExtensionInstaller.UnRegister(typeof(ExeFileContextMenuExtension));
            ExtensionInstaller.UnRegister(typeof(SlnFileContextMenuExtension));
            ExtensionInstaller.UnRegister(typeof(DescriptionControl));
            //ExtensionInstaller.UnRegisterExtension(typeof(TestControl1));

            ExtensionInstaller.ApplyChanges();
        }




    }
}
