using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.Shell
{
    internal class xbfcc67b4a8919b21
    {
        // Fields
        internal IntPtr x4b1e528311f74227;
        internal int x6f7e5ea46c16afdf;
        internal ArrayList xa73e17ae29927ab0 = new ArrayList();
        internal int xf79f6f55056dea4c;

        // Methods
        internal void x2237804176f4caea(ShellMenu x071bde1041617fce, int xeaf1b27180c0557b)
        {
            for (int i = this.xa73e17ae29927ab0.Count - 1; i >= 0; i--)
            {
                ShellMenuItem item = this.xa73e17ae29927ab0[i] as ShellMenuItem;
                if ((item._owner.x4b1e528311f74227 == x071bde1041617fce.x4b1e528311f74227) && (item.xeaf1b27180c0557b == xeaf1b27180c0557b))
                {
                    this.xa73e17ae29927ab0.RemoveAt(i);
                }
            }
        }

        internal ShellMenuItem x24fe407f6d871e28(int x541a67b95acd3459)
        {
            return this.xc28600085ccd9908(x541a67b95acd3459 + this.xf79f6f55056dea4c);
        }

        internal ShellMenuItem x625a8691de7c9283(IntPtr x66504ce4f96688f4)
        {
            foreach (ShellMenuItem item in this.xa73e17ae29927ab0)
            {
                MENUITEMINFO structure = new MENUITEMINFO();
                structure.x2e94540690ec6f24 = (uint)Marshal.SizeOf(structure);
                structure.x8240369a843c7611 = 4;
                int num = item.xc8519eab1f8249ff(item.xeaf1b27180c0557b);
                User32.GetMenuItemInfo(item._owner.x4b1e528311f74227, (uint)num, 1, ref structure);
                if (structure.x66504ce4f96688f4 == x66504ce4f96688f4)
                {
                    return item;
                }
            }
            return null;
        }

        internal ShellMenuItem xc28600085ccd9908(int x541a67b95acd3459)
        {
            for (int i = 0; i < this.xa73e17ae29927ab0.Count; i++)
            {
                ShellMenuItem item = this.xa73e17ae29927ab0[i] as ShellMenuItem;
                if (item.xeaf1b27180c0557b == x541a67b95acd3459)
                {
                    return item;
                }
            }
            return null;
        }

        internal ShellMenuItem xc28600085ccd9908(ShellMenu xb6a159a84cb992d6, int x541a67b95acd3459)
        {
            for (int i = 0; i < this.xa73e17ae29927ab0.Count; i++)
            {
                ShellMenuItem item = this.xa73e17ae29927ab0[i] as ShellMenuItem;
                if ((item.xeaf1b27180c0557b == x541a67b95acd3459) && (item._owner.x4b1e528311f74227 == xb6a159a84cb992d6.x4b1e528311f74227))
                {
                    return item;
                }
            }
            return null;
        }

        internal void xed8fd5934e38f59d(ShellMenuItem xccb63ca5f63dc470)
        {
            xccb63ca5f63dc470.xeaf1b27180c0557b = this.x6f7e5ea46c16afdf;
            this.x6f7e5ea46c16afdf++;
            this.xa73e17ae29927ab0.Add(xccb63ca5f63dc470);
        }

        internal void xed8fd5934e38f59d(ShellMenuItem xccb63ca5f63dc470, int xeaf1b27180c0557b)
        {
            xccb63ca5f63dc470.xeaf1b27180c0557b = xeaf1b27180c0557b;
            this.xa73e17ae29927ab0.Add(xccb63ca5f63dc470);
        }
    }

 

}
