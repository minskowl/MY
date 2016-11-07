using System;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.Shell
{
    [ComImport, Guid("BCFCE0A0-EC17-11d0-8D10-00A0C90F2719"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IContextMenu3
    {
        [PreserveSig]
        int x1bdb3deb8261a3c4(IntPtr x4b1e528311f74227, int x77673e48b8a47af3, int xe8d21c371f0934ef, int x25f7ac57a1bdea71, QueryContextMenuFlags x65e70cabb0b3496f);
        [PreserveSig]
        int xa52f941111a440e3(ref x4c5aeec2d27988ea x56d3397f0a52724d);
        [PreserveSig]
        int x91b6b7eef159d5e5(IntPtr xa37cb707db312687, x41a7610950815f51 x232f9fe4bb34460d, IntPtr xdc993c5e33b6b52b, IntPtr x46dbea5adfba8a62, int xfcffe10b2208dfca);
        [PreserveSig]
        int xcc89ceada61630f6(int x0282e7344f7472d6, IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781);
        [PreserveSig]
        int xc2207af80883aa4f(int x0282e7344f7472d6, IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781, IntPtr xf9dfb93824931f99);
    }

 

    [ComImport, Guid("000214E4-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IContextMenu
    {
        [PreserveSig]
        int x1bdb3deb8261a3c4(IntPtr x4b1e528311f74227, int x77673e48b8a47af3, int xe8d21c371f0934ef, int x25f7ac57a1bdea71, QueryContextMenuFlags x65e70cabb0b3496f);
        [PreserveSig]
        int xa52f941111a440e3(ref x4c5aeec2d27988ea x56d3397f0a52724d);
        [PreserveSig]
        int x91b6b7eef159d5e5(IntPtr xa37cb707db312687, x41a7610950815f51 x232f9fe4bb34460d, IntPtr xdc993c5e33b6b52b, IntPtr x46dbea5adfba8a62, int xfcffe10b2208dfca);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214F4-0000-0000-C000-000000000046")]
    internal interface IContextMenu2
    {
        [PreserveSig]
        int x1bdb3deb8261a3c4(IntPtr x4b1e528311f74227, int x77673e48b8a47af3, int xe8d21c371f0934ef, int x25f7ac57a1bdea71, QueryContextMenuFlags x65e70cabb0b3496f);
        [PreserveSig]
        int xa52f941111a440e3(ref x4c5aeec2d27988ea x56d3397f0a52724d);
        [PreserveSig]
        int x91b6b7eef159d5e5(IntPtr xa37cb707db312687, x41a7610950815f51 x232f9fe4bb34460d, IntPtr xdc993c5e33b6b52b, IntPtr x46dbea5adfba8a62, int xfcffe10b2208dfca);
        [PreserveSig]
        int xcc89ceada61630f6(int x0282e7344f7472d6, IntPtr x716e0bc3eafdded2, IntPtr x130fbcecf32fe781);
    }


    [ComImport, Guid("000214E8-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellExtInit
    {
        /// <summary>
        /// Initializes the specified xdceb127ad2fd117b.
        /// 
        /// x20aee281977480cf
        /// </summary>
        /// <param name="xdceb127ad2fd117b">The xdceb127ad2fd117b.</param>
        /// <param name="x4b9447f495c6b4a1">The x4b9447f495c6b4a1.</param>
        /// <param name="x9a3377d64b14bf3d">The x9a3377d64b14bf3d.</param>
        /// <returns></returns>
        [PreserveSig]
        int Initialize(IntPtr xdceb127ad2fd117b, IntPtr x4b9447f495c6b4a1, IntPtr x9a3377d64b14bf3d);
    }
    /// <summary>
    /// IShellPropSheetExt
    /// xa00be55fe23180b8
    /// </summary>
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E9-0000-0000-C000-000000000046")]
    internal interface IShellPropSheetExt
    {
        /// <summary>
        /// Adds the pages.
        /// x124dc7e81059ee01
        /// </summary>
        /// <param name="x704f1bf9025ef9b0">The x704f1bf9025ef9b0.</param>
        /// <param name="x130fbcecf32fe781">The x130fbcecf32fe781.</param>
        /// <returns></returns>
        [PreserveSig]
        int AddPages(IntPtr x704f1bf9025ef9b0, IntPtr x130fbcecf32fe781);

        /// <summary>
        /// Replaces the page.
        /// x7deab78cd968ebd6
        /// </summary>
        /// <param name="x8c79b1dbfea9ffd4">The x8c79b1dbfea9ffd4.</param>
        /// <param name="x424e3e029d675a23">The x424e3e029d675a23.</param>
        /// <param name="x130fbcecf32fe781">The x130fbcecf32fe781.</param>
        /// <returns></returns>
        [PreserveSig]
        int ReplacePage(int x8c79b1dbfea9ffd4, int x424e3e029d675a23, int x130fbcecf32fe781);
    }


}
