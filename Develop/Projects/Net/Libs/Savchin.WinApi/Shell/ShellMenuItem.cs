using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.Shell
{
    public class ShellMenuItem
    {
        // Fields
        internal ShellMenu _owner;
        internal bool x07d4c1c683eae0fd;
        internal string _caption;
        internal bool x2fef7d841879a711;
        internal string x33c74c46fd3c4244;
        internal bool x364c1e3b189d47fe;
        internal string _verb;
        internal bool x3de314ab70bbd9bf;
        internal bool x55b757cf868cbe52;
        internal bool x81c56a56f929ec31;
        internal bool xc5c612d33b31299f;
        internal bool xe17ea6c34f806184;
        internal int xeaf1b27180c0557b;
        internal bool xf1d951ff17c8de80;
        internal bool xf3a4c29d9fe0ddc7;
        internal object xffe521cc76054baf = null;

        // Methods
        internal ShellMenuItem(ShellMenu owner, string caption, string verb, string helpstring, int id)
        {
            _owner = owner;
            _caption = caption;
            if (caption == null)
            {
                _caption = string.Empty;
            }
            xeaf1b27180c0557b = id;
            _verb = verb;
            if (verb == null)
            {
                _verb = string.Empty;
            }
            x33c74c46fd3c4244 = helpstring;
            if (helpstring == null)
            {
                helpstring = string.Empty;
            }
            xf1d951ff17c8de80 = false;
            x55b757cf868cbe52 = false;
            x07d4c1c683eae0fd = false;
            xf3a4c29d9fe0ddc7 = false;
            x364c1e3b189d47fe = true;
            xc5c612d33b31299f = false;
            x2fef7d841879a711 = true;
            x81c56a56f929ec31 = false;
            x3de314ab70bbd9bf = false;
            xe17ea6c34f806184 = false;
        }

        public void SetBitmap(Bitmap bitmap)
        {
            if (x22ab5dfa6f12e902)
            {
                int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                x443cc432acaadb1d.SetMenuItemBitmaps(_owner.x4b1e528311f74227, (uint)num, 0x400, bitmap.GetHbitmap(), IntPtr.Zero);
            }
        }

        internal int xc8519eab1f8249ff(int xeaf1b27180c0557b)
        {
            int menuItemCount = x443cc432acaadb1d.GetMenuItemCount(_owner.x4b1e528311f74227);
            for (uint i = 0; i < menuItemCount; i++)
            {
                MENUITEMINFO structure = new MENUITEMINFO();
                structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                structure.x8240369a843c7611 = 2;
                User32.GetMenuItemInfo(_owner.x4b1e528311f74227, i, 1, ref structure);
                if (structure.x9f8d45ce61065766 == xeaf1b27180c0557b)
                {
                    return (int)i;
                }
            }
            return -1;
        }

        // Properties
        public bool BarBreak
        {
            get
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x100;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    return ((structure.x482c55f56771fee8 & 0x20) != 0);
                }
                return x55b757cf868cbe52;
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x100;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    if (value)
                    {
                        structure.x482c55f56771fee8 |= 0x20;
                    }
                    else
                    {
                        structure.x482c55f56771fee8 &= 0xffffffdf;
                    }
                    x55b757cf868cbe52 = value;
                    User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                }
            }
        }

        public bool Break
        {
            get
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x100;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    return ((structure.x482c55f56771fee8 & 0x40) != 0);
                }
                return xf1d951ff17c8de80;
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x100;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    if (value)
                    {
                        structure.x482c55f56771fee8 |= 0x40;
                    }
                    else
                    {
                        structure.x482c55f56771fee8 &= 0xffffffbf;
                    }
                    xf1d951ff17c8de80 = value;
                    User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                }
            }
        }

        public string Caption
        {
            get
            {
                if (x22ab5dfa6f12e902)
                    unsafe
                    {
                        byte* numPtr = stackalloc byte[1*600];
                        int idItem = xc8519eab1f8249ff(xeaf1b27180c0557b);
                        x443cc432acaadb1d.GetMenuString(_owner.x4b1e528311f74227, idItem,
                                                        (IntPtr) numPtr, 300, 0x400);
                        return Marshal.PtrToStringAuto((IntPtr) numPtr);
                    }
                return _caption;
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    _caption = value;
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x40;
                    structure.xf29b74a7173844ea = value;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                }
            }
        }

        public bool Checked
        {
            get
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 1;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    return ((structure.xcaac28f758874f3c & 8) != 0);
                }
                return x07d4c1c683eae0fd;
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 1;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    if (value)
                    {
                        structure.xcaac28f758874f3c |= 8;
                    }
                    else
                    {
                        structure.xcaac28f758874f3c &= 0xfffffff7;
                    }
                    x07d4c1c683eae0fd = value;
                    User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                }
            }
        }

        public bool DefaultItem
        {
            get
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 1;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    return ((structure.xcaac28f758874f3c & 0x1000) != 0);
                }
                return xc5c612d33b31299f;
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 1;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    if (value)
                    {
                        structure.xcaac28f758874f3c |= 0x1000;
                    }
                    else
                    {
                        structure.xcaac28f758874f3c &= 0xffffefff;
                    }
                    xc5c612d33b31299f = value;
                    User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                }
            }
        }

        public bool Enabled
        {
            get
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 1;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    return ((structure.xcaac28f758874f3c & 3) != 3);
                }
                return x2fef7d841879a711;
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 1;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    if (value)
                    {
                        structure.xcaac28f758874f3c &= 0xfffffffc;
                    }
                    else
                    {
                        structure.xcaac28f758874f3c |= 3;
                    }
                    x2fef7d841879a711 = value;
                    User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                }
            }
        }

        public bool HasSubMenu
        {
            get
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 4;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    return (structure.x66504ce4f96688f4 != IntPtr.Zero);
                }
                return xe17ea6c34f806184;
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 4;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    IntPtr hMenu = structure.x66504ce4f96688f4;
                    if (value != (hMenu != IntPtr.Zero))
                    {
                        if (value)
                        {
                            structure.x66504ce4f96688f4 = x443cc432acaadb1d.CreatePopupMenu();
                        }
                        else
                        {
                            structure.x66504ce4f96688f4 = IntPtr.Zero;
                            x443cc432acaadb1d.DestroyMenu(hMenu);
                        }
                        xe17ea6c34f806184 = value;
                        User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    }
                }
            }
        }

        public string HelpString
        {
            get
            {
                return x33c74c46fd3c4244;
            }
            set
            {
                x33c74c46fd3c4244 = value;
            }
        }

        public int ID
        {
            get
            {
                return xeaf1b27180c0557b;
            }
        }

        public ShellMenu Owner
        {
            get
            {
                return _owner;
            }
        }

        public bool OwnerDraw
        {
            get
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x100;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    return ((structure.x482c55f56771fee8 & 0x100) != 0);
                }
                return x81c56a56f929ec31;
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x100;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    if (value)
                    {
                        structure.x482c55f56771fee8 |= 0x100;
                    }
                    else
                    {
                        structure.x482c55f56771fee8 &= 0xfffffeff;
                    }
                    x81c56a56f929ec31 = value;
                    User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                }
            }
        }

        public bool RadioChecked
        {
            get
            {
                if (!x22ab5dfa6f12e902)
                {
                    return xf3a4c29d9fe0ddc7;
                }
                MENUITEMINFO structure = new MENUITEMINFO();
                structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                structure.x8240369a843c7611 = 0x101;
                int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                return (((structure.x482c55f56771fee8 & 0x200) != 0) && ((structure.xcaac28f758874f3c & 8) != 0));
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x101;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    if (value)
                    {
                        structure.xcaac28f758874f3c |= 8;
                        structure.x482c55f56771fee8 |= 0x200;
                    }
                    else
                    {
                        structure.xcaac28f758874f3c &= 0xfffffff7;
                        structure.x482c55f56771fee8 &= 0xfffffdff;
                    }
                    xf3a4c29d9fe0ddc7 = value;
                    User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                }
            }
        }

        public bool Separator
        {
            get
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x100;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    return ((structure.x482c55f56771fee8 & 0x800) != 0);
                }
                return x3de314ab70bbd9bf;
            }
            set
            {
                if (x22ab5dfa6f12e902)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 0x100;
                    int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                    User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                    if (value)
                    {
                        structure.x482c55f56771fee8 |= 0x800;
                    }
                    else
                    {
                        structure.x482c55f56771fee8 &= 0xfffff7ff;
                    }
                    x3de314ab70bbd9bf = value;
                    User32.SetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                }
            }
        }

        public ShellMenu SubMenu
        {
            get
            {
                if (!x22ab5dfa6f12e902)
                {
                    return null;
                }
                MENUITEMINFO structure = new MENUITEMINFO();
                structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                structure.x8240369a843c7611 = 4;
                int num = xc8519eab1f8249ff(xeaf1b27180c0557b);
                User32.GetMenuItemInfo(_owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                if (structure.x66504ce4f96688f4 == IntPtr.Zero)
                {
                    return null;
                }
                ShellMenu menu = new ShellMenu(structure.x66504ce4f96688f4, _owner, 0);
                menu.xaf633da7d1e327ec = menu.ItemCount;
                return menu;
            }
        }

        public object Tag
        {
            get
            {
                return xffe521cc76054baf;
            }
            set
            {
                xffe521cc76054baf = value;
            }
        }

        public string Verb
        {
            get
            {
                return _verb;
            }
            set
            {
                _verb = value;
            }
        }

        internal bool x22ab5dfa6f12e902
        {
            get
            {
                return ((_owner != null) && (x443cc432acaadb1d.IsMenu(_owner.x1dbcf1591cc2cceb.x4b1e528311f74227) != 0));
            }
        }
    }





}
