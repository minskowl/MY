using System;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.Shell
{
    public class ShellMenu
    {
        // Fields
        internal xbfcc67b4a8919b21 x1dbcf1591cc2cceb;
        internal IntPtr x4b1e528311f74227;
        internal int xaf633da7d1e327ec;
        internal ShellMenu xb6a159a84cb992d6;

        // Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellMenu"/> class.
        /// </summary>
        /// <param name="hMenu">The h menu.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="defaultInsertIndex">Default index of the insert.</param>
        internal ShellMenu(IntPtr hMenu, ShellMenu parent, int defaultInsertIndex)
        {
            x4b1e528311f74227 = hMenu;
            xb6a159a84cb992d6 = parent;
            x1dbcf1591cc2cceb = parent.x1dbcf1591cc2cceb;
            xaf633da7d1e327ec = defaultInsertIndex;
        }

        internal ShellMenu(IntPtr hMenu, int firstCmdID, int defaultInsertIndex)
        {
            x4b1e528311f74227 = hMenu;
            x1dbcf1591cc2cceb = new xbfcc67b4a8919b21();
            x1dbcf1591cc2cceb.xf79f6f55056dea4c = firstCmdID;
            x1dbcf1591cc2cceb.x6f7e5ea46c16afdf = firstCmdID;
            x1dbcf1591cc2cceb.x4b1e528311f74227 = hMenu;
            xaf633da7d1e327ec = defaultInsertIndex;
        }

        public ShellMenuItem AddItem(string caption)
        {
            return InsertItem(caption, caption, caption, xaf633da7d1e327ec++);
        }

        public ShellMenuItem AddItem(string caption, int id)
        {
            return InsertItem(caption, caption, caption, xaf633da7d1e327ec++, id);
        }

        public ShellMenuItem AddItem(string caption, string verb, string helpString)
        {
            return InsertItem(caption, verb, helpString, xaf633da7d1e327ec++);
        }

        public ShellMenuItem AddItem(string caption, string verb, string helpString, int id)
        {
            return InsertItem(caption, verb, helpString, xaf633da7d1e327ec++, id);
        }

        public ShellMenuItem Getitem(int index)
        {
            if (x1dbcf1591cc2cceb == null)
            {
                return null;
            }
            int itemCount = ItemCount;
            if ((index < 0) || (index >= itemCount))
            {
                return null;
            }
            ShellMenuItem item = x1dbcf1591cc2cceb.xc28600085ccd9908(this, x443cc432acaadb1d.GetMenuItemID(x4b1e528311f74227, index));
            if (item == null)
            {
                item = new ShellMenuItem(this, null, null, null, x443cc432acaadb1d.GetMenuItemID(x4b1e528311f74227, index));
            }
            return item;
        }

        public ShellMenuItem GetItemFromID(int id)
        {
            if (x1dbcf1591cc2cceb == null)
            {
                return null;
            }
            ShellMenuItem item = x1dbcf1591cc2cceb.xc28600085ccd9908(this, id);
            if (item == null)
            {
                int itemCount = ItemCount;
                for (int i = 0; i < itemCount; i++)
                {
                    MENUITEMINFO structure = new MENUITEMINFO();
                    structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                    structure.x8240369a843c7611 = 2;
                    User32.GetMenuItemInfo(x4b1e528311f74227, (uint)i, 1, ref structure);
                    if (structure.x9f8d45ce61065766 == id)
                    {
                        return new ShellMenuItem(this, null, null, null, id);
                    }
                }
            }
            return item;
        }

        public ShellMenuItem InsertItem(string caption, int index)
        {
            return InsertItem(caption, caption, caption, index);
        }

        public ShellMenuItem InsertItem(string caption, int index, int id)
        {
            return InsertItem(caption, caption, caption, index, id);
        }

        public ShellMenuItem InsertItem(string caption, string verb, string helpString, int index)
        {
            int itemCount = ItemCount;
            if ((index < -1) || (index > itemCount))
            {
                return null;
            }
            if (itemCount == index)
            {
                index = -1;
            }
            x443cc432acaadb1d.InsertMenu(x4b1e528311f74227, index, x3d386b25758ba442.x8a088cc0adc307e6, x1dbcf1591cc2cceb.x6f7e5ea46c16afdf, caption);
            if (index == -1)
            {
                index = itemCount;
            }
            x75c9b935933f9eca(index, x1dbcf1591cc2cceb.x6f7e5ea46c16afdf);
            ShellMenuItem item = new ShellMenuItem(this, caption, verb, helpString, x1dbcf1591cc2cceb.x6f7e5ea46c16afdf);
            x1dbcf1591cc2cceb.xed8fd5934e38f59d(item);
            return item;
        }

        public ShellMenuItem InsertItem(string caption, string verb, string helpString, int index, int id)
        {
            int itemCount = ItemCount;
            if ((index < -1) || (index > itemCount))
            {
                return null;
            }
            if (itemCount == index)
            {
                index = -1;
            }
            x443cc432acaadb1d.InsertMenu(x4b1e528311f74227, index, x3d386b25758ba442.x8a088cc0adc307e6, id, caption);
            if (index == -1)
            {
                index = itemCount;
            }
            x75c9b935933f9eca(index, id);
            ShellMenuItem item = new ShellMenuItem(this, caption, verb, helpString, id);
            x1dbcf1591cc2cceb.xed8fd5934e38f59d(item, id);
            return item;
        }

        public void RemoveAll()
        {
            for (int i = ItemCount - 1; i >= 0; i--)
            {
                RemoveItem(i);
            }
        }

        public void RemoveItem(int index)
        {
            int itemCount = ItemCount;
            if ((index >= 0) && (index < itemCount))
            {
                MENUITEMINFO structure = new MENUITEMINFO();
                structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                structure.x8240369a843c7611 = 2;
                User32.GetMenuItemInfo(x4b1e528311f74227, (uint)index, 1, ref structure);
                IntPtr subMenu = x443cc432acaadb1d.GetSubMenu(x4b1e528311f74227, index);
                if (subMenu != IntPtr.Zero)
                {
                    new ShellMenu(subMenu, this, 0).RemoveAll();
                    x443cc432acaadb1d.DestroyMenu(subMenu);
                }
                x443cc432acaadb1d.RemoveMenu(x4b1e528311f74227, index, 0x400);
                x1dbcf1591cc2cceb.x2237804176f4caea(this, (int)structure.x9f8d45ce61065766);
            }
        }

        internal void x75c9b935933f9eca(int xc0c4c459c6ccbd00, int xeaf1b27180c0557b)
        {
            MENUITEMINFO structure = new MENUITEMINFO();
            structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
            structure.x8240369a843c7611 = 2;
            structure.x9f8d45ce61065766 = (uint)xeaf1b27180c0557b;
            User32.SetMenuItemInfo(x4b1e528311f74227, (uint)xc0c4c459c6ccbd00, 1, ref structure);
        }

        // Properties
        public int ItemCount
        {
            get
            {
                return x443cc432acaadb1d.GetMenuItemCount(x4b1e528311f74227);
            }
        }
    }





}
