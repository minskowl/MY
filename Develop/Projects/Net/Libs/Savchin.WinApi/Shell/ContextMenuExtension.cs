using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;


namespace Savchin.WinApi.Shell
{

    /// <summary>
    /// ContextMenuExtension
    /// </summary>
    public class ContextMenuExtension : IContextMenu, IContextMenu2, IContextMenu3, IShellExtInit
    {
        // Fields
        internal string[] x58c3a0df2fc736ea;
        private string x8b5b49efcd08ae0a = string.Empty;
        private ShellMenu xcbf78b15dd820156;
        private object xdd09610a7ca17912 = null;

        // Methods
        public virtual string GetMenuItemHelp(ShellMenuItem item)
        {
            return item.x33c74c46fd3c4244;
        }

        public virtual string GetMenuItemVerb(ShellMenuItem item)
        {
            return item._verb;
        }

        protected virtual void OnDrawMenuItem(EZSDrawItemEventArgs e)
        {
            e.DrawBackground();
            using (var brush = (e.ForeColor.IsSystemColor) ? SystemBrushes.FromSystemColor(e.ForeColor) : new SolidBrush(e.ForeColor))
            {

                e.Graphics.DrawString(e.MenuItem.Caption, e.Font, brush, new PointF((float)e.Bounds.Left, (float)e.Bounds.Top));
            }
        }

        protected virtual bool OnExecuteMenuItem(ExecuteItemEventArgs e)
        {
            return false;
        }

        protected virtual void OnGetMenuItems(GetMenuitemsEventArgs e)
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

        protected virtual void OnMeasureMenuItem(EZSMeasureItemEventArgs e)
        {
            e.ItemHeight = SystemInformation.MenuHeight;
        }

        protected virtual void OnPopupSubMenu(ShellMenuItem menuItem)
        {
        }

        public static void RegisterExtension(Type t)
        {
            string str = RegistryKeyNameAttribute.x0ba0e0095169fb2c(t);
            foreach (TargetExtensionAttribute attribute in TargetExtensionAttribute.AttributesFromType(t))
            {
                RegistryKey key;
                if (Enum.IsDefined(typeof(RegistryHive), attribute.rh))
                {
                    key = attribute.RootKey.CreateSubKey(attribute.fileExtension + @"\Shellex\ContextMenuHandlers\" + str);
                }
                else
                {
                    key = Registry.ClassesRoot.CreateSubKey(attribute.ProgID + @"\Shellex\ContextMenuHandlers\" + str);
                }
                key.SetValue(string.Empty, t.GUID.ToString("B"));
                key.Close();
            }
            x443cc432acaadb1d.xa346e04a75e6b993(t);
            //  x443cc432acaadb1d.SHChangeNotify(0x8000000, 0, IntPtr.Zero, IntPtr.Zero);
        }

        public static void UnRegisterExtension(Type t)
        {
            string str = RegistryKeyNameAttribute.x0ba0e0095169fb2c(t);
            foreach (TargetExtensionAttribute attribute in TargetExtensionAttribute.AttributesFromType(t))
            {
                if (Enum.IsDefined(typeof(RegistryHive), attribute.rh))
                {
                    attribute.RootKey.DeleteSubKey(attribute.fileExtension + @"\Shellex\ContextMenuHandlers\" + str, false);
                }
                else
                {
                    Registry.ClassesRoot.DeleteSubKey(attribute.ProgID + @"\Shellex\ContextMenuHandlers\" + str, false);
                }
            }
            x443cc432acaadb1d.x43908261d1e1c129(t);
            //  x443cc432acaadb1d.SHChangeNotify(0x8000000, 0, IntPtr.Zero, IntPtr.Zero);
        }

        int IContextMenu.x91b6b7eef159d5e5(IntPtr xa37cb707db312687, x41a7610950815f51 x232f9fe4bb34460d, IntPtr xdc993c5e33b6b52b, IntPtr x46dbea5adfba8a62, int xfcffe10b2208dfca)
        {
            return xeb60d1009423214a(xa37cb707db312687, x232f9fe4bb34460d, xdc993c5e33b6b52b, x46dbea5adfba8a62, xfcffe10b2208dfca);
        }


        int IContextMenu2.x91b6b7eef159d5e5(IntPtr xa37cb707db312687, x41a7610950815f51 x232f9fe4bb34460d, IntPtr xdc993c5e33b6b52b, IntPtr x46dbea5adfba8a62, int xfcffe10b2208dfca)
        {
            return xeb60d1009423214a(xa37cb707db312687, x232f9fe4bb34460d, xdc993c5e33b6b52b, x46dbea5adfba8a62, xfcffe10b2208dfca);
        }

        int IContextMenu3.x91b6b7eef159d5e5(IntPtr xa37cb707db312687, x41a7610950815f51 x232f9fe4bb34460d, IntPtr xdc993c5e33b6b52b, IntPtr x46dbea5adfba8a62, int xfcffe10b2208dfca)
        {
            return xeb60d1009423214a(xa37cb707db312687, x232f9fe4bb34460d, xdc993c5e33b6b52b, x46dbea5adfba8a62, xfcffe10b2208dfca);
        }

        private int x4195d1a0483fcfaa(int x0282e7344f7472d6, IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781)
        {
            if (x0282e7344f7472d6 == 0x2c)
            {
                return x48428b70d50d804c(x716e0bc3eafdded2, x130fbcecf32fe781);
            }
            if (x0282e7344f7472d6 == 0x2b)
            {
                return xa7869b3752ffd1ad(x716e0bc3eafdded2, x130fbcecf32fe781);
            }
            if (x0282e7344f7472d6 == 0x117)
            {
                return xa569c51e2c415f79(x716e0bc3eafdded2, x130fbcecf32fe781);
            }
            return 0;
        }

        private int x48428b70d50d804c(IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781)
        {
            try
            {
                x1b5344ef9f1013cc structure = (x1b5344ef9f1013cc)Marshal.PtrToStructure(x130fbcecf32fe781, typeof(x1b5344ef9f1013cc));
                IntPtr dC = x443cc432acaadb1d.GetDC(IntPtr.Zero);
                Graphics g = Graphics.FromHdc(dC);
                EZSMeasureItemEventArgs e = new EZSMeasureItemEventArgs(xcbf78b15dd820156.x1dbcf1591cc2cceb.xc28600085ccd9908(structure.xdf116082e33a0b32), g, structure.x918e8eebd9706b9a, structure.x25117dc454d94c27);
                try
                {
                    OnMeasureMenuItem(e);
                }
                finally
                {
                    g.Dispose();
                }
                x443cc432acaadb1d.ReleaseDC(IntPtr.Zero, dC);
                structure.x25117dc454d94c27 = e.ItemHeight;
                structure.x918e8eebd9706b9a = e.ItemWidth;
                Marshal.StructureToPtr(structure, x130fbcecf32fe781, false);
            }
            catch
            {
            }
            return 0;
        }


        int IContextMenu.xa52f941111a440e3(ref x4c5aeec2d27988ea x56d3397f0a52724d)
        {
            return xedfc3e36e083cd18(ref x56d3397f0a52724d);
        }
        int IContextMenu2.xa52f941111a440e3(ref x4c5aeec2d27988ea x56d3397f0a52724d)
        {
            return xedfc3e36e083cd18(ref x56d3397f0a52724d);
        }

        int IContextMenu3.xa52f941111a440e3(ref x4c5aeec2d27988ea x56d3397f0a52724d)
        {
            return xedfc3e36e083cd18(ref x56d3397f0a52724d);
        }





        private int xa569c51e2c415f79(IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781)
        {
            try
            {
                ShellMenuItem menuItem = xcbf78b15dd820156.x1dbcf1591cc2cceb.x625a8691de7c9283(x716e0bc3eafdded2);
                if (menuItem != null)
                {
                    OnPopupSubMenu(menuItem);
                }
            }
            catch
            {
            }
            return 0;
        }

        private int xa7869b3752ffd1ad(IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781)
        {
            x3ff95812464be8ed xffbeed = (x3ff95812464be8ed)Marshal.PtrToStructure(x130fbcecf32fe781, typeof(x3ff95812464be8ed));
            using (Graphics graphics = Graphics.FromHdc(xffbeed.xea0205f8180e5e40))
            {
                ShellMenuItem menuitem = xcbf78b15dd820156.x1dbcf1591cc2cceb.x625a8691de7c9283(xffbeed.x8adb1f45d0d1fc14);
                if (menuitem != null)
                {
                    menuitem = menuitem.SubMenu.GetItemFromID(xffbeed.xdf116082e33a0b32);
                }
                if (menuitem == null)
                {
                    menuitem = xcbf78b15dd820156.x1dbcf1591cc2cceb.xc28600085ccd9908(xffbeed.xdf116082e33a0b32);
                }
                EZSDrawItemEventArgs e = new EZSDrawItemEventArgs(menuitem, graphics, SystemInformation.MenuFont, Rectangle.FromLTRB(xffbeed.x8d41594059465577.xa447fc54e41dfe06, xffbeed.x8d41594059465577.xc941868c59399d3e, xffbeed.x8d41594059465577.xfc2074a859a5db8c, xffbeed.x8d41594059465577.xaf9a0436a70689de), (DrawItemState)xffbeed.x970e523b845fd5c8);
                OnDrawMenuItem(e);
            }
            return 0;
        }

        int IContextMenu.x1bdb3deb8261a3c4(IntPtr x4b1e528311f74227, int x77673e48b8a47af3, int xe8d21c371f0934ef, int x25f7ac57a1bdea71, QueryContextMenuFlags x65e70cabb0b3496f)
        {
            return xe6b590f4a33ffa7d(x4b1e528311f74227, x77673e48b8a47af3, xe8d21c371f0934ef, x25f7ac57a1bdea71, x65e70cabb0b3496f);
        }

        int IContextMenu2.x1bdb3deb8261a3c4(IntPtr x4b1e528311f74227, int x77673e48b8a47af3, int xe8d21c371f0934ef, int x25f7ac57a1bdea71, QueryContextMenuFlags x65e70cabb0b3496f)
        {
            return xe6b590f4a33ffa7d(x4b1e528311f74227, x77673e48b8a47af3, xe8d21c371f0934ef, x25f7ac57a1bdea71, x65e70cabb0b3496f);
        }
        int IContextMenu3.x1bdb3deb8261a3c4(IntPtr x4b1e528311f74227, int x77673e48b8a47af3, int xe8d21c371f0934ef, int x25f7ac57a1bdea71, QueryContextMenuFlags x65e70cabb0b3496f)
        {
            return xe6b590f4a33ffa7d(x4b1e528311f74227, x77673e48b8a47af3, xe8d21c371f0934ef, x25f7ac57a1bdea71, x65e70cabb0b3496f);
        }

        int IContextMenu3.xc2207af80883aa4f(int x0282e7344f7472d6, IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781, IntPtr xf9dfb93824931f99)
        {
            return x4195d1a0483fcfaa(x0282e7344f7472d6, x716e0bc3eafdded2, x130fbcecf32fe781);
        }

        int IContextMenu2.xcc89ceada61630f6(int x0282e7344f7472d6, IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781)
        {
            return x4195d1a0483fcfaa(x0282e7344f7472d6, x716e0bc3eafdded2, x130fbcecf32fe781);
        }
        int IContextMenu3.xcc89ceada61630f6(int x0282e7344f7472d6, IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781)
        {
            return x4195d1a0483fcfaa(x0282e7344f7472d6, x716e0bc3eafdded2, x130fbcecf32fe781);
        }


        int IShellExtInit.Initialize(IntPtr xdceb127ad2fd117b, IntPtr x4b9447f495c6b4a1, IntPtr x9a3377d64b14bf3d)
        {
            xdd09610a7ca17912 = null;
            try
            {
                if (xdceb127ad2fd117b != IntPtr.Zero)
                {
                    StringBuilder path = new StringBuilder(260);
                    x443cc432acaadb1d.SHGetPathFromIDList(xdceb127ad2fd117b, path);
                    x8b5b49efcd08ae0a = path.ToString();
                }
                if (x4b9447f495c6b4a1 != IntPtr.Zero)
                {
                    xdd09610a7ca17912 = Marshal.GetObjectForIUnknown(x4b9447f495c6b4a1);
                    if (xdd09610a7ca17912 is IOleDataObject)
                    {
                        IOleDataObject obj2 = xdd09610a7ca17912 as IOleDataObject;
                        x58c3a0df2fc736ea = x443cc432acaadb1d.x0810e12d53bb2dfb(obj2);
                        obj2 = null;
                    }
                }
                if (x58c3a0df2fc736ea == null)
                {
                    x58c3a0df2fc736ea = new string[0];
                }
                if (!OnInitialize())
                {
                    return x443cc432acaadb1d.x15f59d42384ec1d4;
                }
                if (!OnInitializeEx(xdd09610a7ca17912 as IOleDataObject))
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
                if ((xdd09610a7ca17912 != null) && (xdd09610a7ca17912 is IOleDataObject))
                {
                    Marshal.ReleaseComObject(xdd09610a7ca17912);
                }
                xdd09610a7ca17912 = null;
            }
            return 0;
        }

        private object xc813b8b627005ff4()
        {
            return xdd09610a7ca17912;
        }


        private int xe6b590f4a33ffa7d(IntPtr x4b1e528311f74227, int x77673e48b8a47af3, int xe8d21c371f0934ef, int x25f7ac57a1bdea71, QueryContextMenuFlags x65e70cabb0b3496f)
        {
            try
            {
                xcbf78b15dd820156 = new ShellMenu(x4b1e528311f74227, xe8d21c371f0934ef, x77673e48b8a47af3);
                GetMenuitemsEventArgs e = new GetMenuitemsEventArgs(xcbf78b15dd820156, x65e70cabb0b3496f, x77673e48b8a47af3, (x25f7ac57a1bdea71 - xe8d21c371f0934ef) + 1);
                OnGetMenuItems(e);
                if ((xcbf78b15dd820156.x1dbcf1591cc2cceb.x6f7e5ea46c16afdf + e.xa60d4d9c2d90066f) > x25f7ac57a1bdea71)
                {
                    throw new InvalidOperationException("Number of menu items is greater than allowed by host(Windows Explorer)");
                }
                return ((xcbf78b15dd820156.x1dbcf1591cc2cceb.x6f7e5ea46c16afdf - xcbf78b15dd820156.x1dbcf1591cc2cceb.xf79f6f55056dea4c) + e.xa60d4d9c2d90066f);
            }
            catch
            {
                return x443cc432acaadb1d.x15f59d42384ec1d4;
            }
        }



        private int xeb60d1009423214a(IntPtr xa37cb707db312687, x41a7610950815f51 x232f9fe4bb34460d, IntPtr xdc993c5e33b6b52b, IntPtr x46dbea5adfba8a62, int xfcffe10b2208dfca)
        {
            try
            {
                if ((x232f9fe4bb34460d & x41a7610950815f51.x0534a8caf4c48f88) == x41a7610950815f51.x0534a8caf4c48f88)
                {
                    ShellMenuItem item = xcbf78b15dd820156.x1dbcf1591cc2cceb.x24fe407f6d871e28(xa37cb707db312687.ToInt32());
                    string menuItemHelp = GetMenuItemHelp(item);
                    if (item != null)
                    {
                        if ((x232f9fe4bb34460d & x41a7610950815f51.x784c7ef45bb1c58e) == x41a7610950815f51.x784c7ef45bb1c58e)
                        {
                            x443cc432acaadb1d.x2994e7e7a2efe4d9(menuItemHelp, x46dbea5adfba8a62, xfcffe10b2208dfca * 2);
                        }
                        else
                        {
                            x443cc432acaadb1d.xdf8e4b332b4a57af(menuItemHelp, x46dbea5adfba8a62, xfcffe10b2208dfca);
                        }
                    }
                }
                else
                {
                    ShellMenuItem item2 = xcbf78b15dd820156.x1dbcf1591cc2cceb.x24fe407f6d871e28(xa37cb707db312687.ToInt32());
                    string menuItemVerb = GetMenuItemVerb(item2);
                    if (item2 != null)
                    {
                        if ((x232f9fe4bb34460d & x41a7610950815f51.x784c7ef45bb1c58e) == x41a7610950815f51.x784c7ef45bb1c58e)
                        {
                            x443cc432acaadb1d.x2994e7e7a2efe4d9(menuItemVerb, x46dbea5adfba8a62, xfcffe10b2208dfca * 2);
                        }
                        else
                        {
                            x443cc432acaadb1d.xdf8e4b332b4a57af(menuItemVerb, x46dbea5adfba8a62, xfcffe10b2208dfca);
                        }
                    }
                }
            }
            catch
            {
                return 1;
            }
            return 0;
        }

        private int xedfc3e36e083cd18(ref x4c5aeec2d27988ea x56d3397f0a52724d)
        {
            try
            {
                ShellMenuItem menuItem = null;
                if (x443cc432acaadb1d.xefc704ff04352756(x56d3397f0a52724d.xe5cf2142b5b1d595) != 0)
                {
                    string caption = Marshal.PtrToStringAnsi(x56d3397f0a52724d.xe5cf2142b5b1d595);
                    menuItem = new ShellMenuItem(null, caption, caption, caption, -1);
                }
                else
                {
                    int num = x56d3397f0a52724d.xe5cf2142b5b1d595.ToInt32();
                    menuItem = xcbf78b15dd820156.x1dbcf1591cc2cceb.x24fe407f6d871e28(num);
                }
                if (menuItem != null)
                {
                    x0bdae4f1006d9d27.x9d748d087524e2b4(this, 3);
                    ExecuteItemEventArgs e = new ExecuteItemEventArgs(menuItem, (ExecuteMenuItemFlags)x56d3397f0a52724d.x8240369a843c7611, x56d3397f0a52724d.x7cc24662a4086c94);
                    if (!OnExecuteMenuItem(e))
                    {
                        return x443cc432acaadb1d.x15f59d42384ec1d4;
                    }
                }
                else
                {
                    return x443cc432acaadb1d.x15f59d42384ec1d4;
                }
            }
            catch
            {
                return 1;
            }
            return 0;
        }


        // Properties
        public string[] TargetFiles
        {
            get
            {
                return x58c3a0df2fc736ea;
            }
        }

        public string TargetFolder
        {
            get
            {
                return x8b5b49efcd08ae0a;
            }
        }
    }




}

