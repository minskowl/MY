using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.WinApi.Shell
{
    /// <summary>
    /// ExtensionInstaller
    /// </summary>
    public static class ExtensionInstaller
    {
        /// <summary>
        /// Registers the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public static void Register(Type type)
        {
            if (type.IsSubclassOf(typeof(ContextMenuExtension)))
                ContextMenuExtension.RegisterExtension(type);
            else if (type.IsSubclassOf(typeof(PropertySheetExtension)))
                PropertySheetExtension.RegisterExtension(type);
            else
                throw new ArgumentOutOfRangeException(string.Format("Type '{0}' is not extenssion", type.FullName));


        }

        /// <summary>
        /// Uns the register.
        /// </summary>
        /// <param name="type">The type.</param>
        public static void UnRegister(Type type)
        {
            if (type.IsSubclassOf(typeof(ContextMenuExtension)))
                ContextMenuExtension.UnRegisterExtension(type);
            else if (type.IsSubclassOf(typeof(PropertySheetExtension)))
                PropertySheetExtension.UnRegisterExtension(type);
            else
                throw new ArgumentOutOfRangeException(string.Format("Type '{0}' is not extenssion", type.FullName));


        }
        /// <summary>
        /// Applies the changes.
        /// </summary>
        public static void ApplyChanges()
        {
            Shell32.SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
