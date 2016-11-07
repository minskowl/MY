using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.Shell
{

    [StructLayout(LayoutKind.Sequential)]
    internal struct x3ff95812464be8ed
    {
        internal int x726d735978806f64;
        internal int x38bdd6fa12513293;
        internal int xdf116082e33a0b32;
        internal int xd860b1a4f5726780;
        internal int x970e523b845fd5c8;
        internal IntPtr x8adb1f45d0d1fc14;
        internal IntPtr xea0205f8180e5e40;
        internal x8dd4b7a13a696a09 x8d41594059465577;
        internal IntPtr x5f9e5115fd737f65;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct x1b5344ef9f1013cc
    {
        internal int x726d735978806f64;
        internal int x38bdd6fa12513293;
        internal int xdf116082e33a0b32;
        internal int x918e8eebd9706b9a;
        internal int x25117dc454d94c27;
        internal int x5f9e5115fd737f65;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct x4c5aeec2d27988ea
    {
        internal int x2e94540690ec6f24;
        internal x02cf16fd44571d3c x8240369a843c7611;
        internal IntPtr x7cc24662a4086c94;
        internal IntPtr xe5cf2142b5b1d595;
        internal IntPtr x5ffa6bcfcc39a103;
        internal IntPtr xbbbf60163ce4f664;
        internal int xaf8548ec012800aa;
        internal int xca2cbf6b860c0c9e;
        internal IntPtr x583e202363b74672;
    }


    [StructLayout(LayoutKind.Sequential)]
    internal struct x8dd4b7a13a696a09
    {
        internal int xa447fc54e41dfe06;
        internal int xc941868c59399d3e;
        internal int xfc2074a859a5db8c;
        internal int xaf9a0436a70689de;
        public static implicit operator Rectangle(x8dd4b7a13a696a09 r)
        {
            return new Rectangle(r.xa447fc54e41dfe06, r.xc941868c59399d3e, r.xfc2074a859a5db8c - r.xa447fc54e41dfe06, r.xaf9a0436a70689de - r.xc941868c59399d3e);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct xd0b43587806b92d7
    {
        internal x53cb129830509e4d x8baacca1e4006e06;
        internal IntPtr x130fbcecf32fe781;
    }


    [StructLayout(LayoutKind.Sequential)]
    internal struct x53cb129830509e4d
    {
        internal IntPtr xfd38ecc6963c683b;
        internal UIntPtr x4f3b221b51385ccd;
        internal int x9035cf16181332fc;
    }
   
   
    /*
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 2)]
    internal struct x0b92a08e52b9ff9c
    {
        internal x2370ad8218c5b454 x44ecfea61c937b8e;
        internal uint xdec78e8ca1b23607;
        internal ushort xab3ad07eee64fe38;
        internal short x;
        internal short y;
        internal short cx;
        internal short cy;
        internal short x27563cc5c53efffb;
        internal short x4c02e7c160d65a3b;
        internal short x6548c9a581d109dc;
        internal short x592f8828575d6342;
        [MarshalAs(UnmanagedType.LPWStr)]
        internal string x34db0a9fc671eb21;
    }*/
    public enum DS : uint
    {
        SETFONT = 0x40,
        FIXEDSYS = 0x0008
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Auto)]
    public struct DLGTEMPLATE
    {
        internal DS style;
        internal uint extendedStyle;
        internal ushort cdit;
        internal short x;
        internal short y;
        internal short cx;
        internal short cy;
        internal short menuResource;
        internal short windowClass;
        internal short titleArray;
        internal short fontPointSize;
        [MarshalAs(UnmanagedType.LPWStr)]
        internal string fontTypeface;
    }
}
