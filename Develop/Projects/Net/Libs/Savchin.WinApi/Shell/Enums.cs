using System;

namespace Savchin.WinApi.Shell
{
    public enum PropertySheetButton
    {
        Apply = 4,
        Cancel = 5,
        Help = 6,
        OK = 3
    }

    [Flags]
    public enum ExecuteMenuItemFlags
    {
        ControlKeyDown = 0x40000000,
        DontDisplayUI = 0x400,
        ShiftKeyDown = 0x10000000
    }

    [Flags]
    public enum QueryContextMenuFlags
    {
        CanRename = 0x10,
        DefaultOnly = 1,
        Explore = 4,
        ForFileMenu = 0x10000,
        IncludeStatic = 0x40,
        NoDefault = 0x20,
        Normal = 0,
        NoVerbs = 8,
        Reserved = -65536,
        VerbsOnly = 2
    }





    public enum NotifyResult
    {
        NoError,
        Invalid,
        InvalidNoChangePage
    }
    [Flags]
    internal enum x41a7610950815f51
    {
        x0534a8caf4c48f88 = 1,
        x784c7ef45bb1c58e = 4,
        xa438ff15e7379de8 = 2,
        xfc6e00ec171e858b = 0
    }

    [Flags]
    internal enum x3d386b25758ba442
    {
        x63d1bdcfa94f1b1d = 0x10,
        x8a088cc0adc307e6 = 0x400,
        xab4c528250c99d5b = 0x800,
        xd9240a9422ef5cef = 0x100
    }

    /// <summary>
    /// xf77504068909535c
    /// </summary>
    [Flags]
    internal enum pspFlags
    {
        /// <summary>
        /// 
        /// </summary>
        PSP_USEHEADERTITLE = 0x00001000,
        /// <summary>
        /// 
        /// </summary>
        PSP_USEHICON = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        PSP_USETITLE = 0x00000008,
        /// <summary>
        /// 
        /// </summary>
        PSP_USECALLBACK =  0x00000080,
        /// <summary>
        /// 
        /// </summary>
        PSP_DLGINDIRECT =  0x00000001,
        /// <summary>
        /// 
        /// </summary>
        PSP_RTLREADING =  0x00000010,
        /// <summary>
        /// 
        /// </summary>
        PSP_USEICONID = 0x00000004,
        /// <summary>
        /// 
        /// </summary>
        PSP_HIDEHEADER = 0x00000800,
        /// <summary>
        /// 
        /// </summary>
        PSP_USEHEADERSUBTITLE = 0x00002000,
        /// <summary>
        /// 
        /// </summary>
        PSP_PREMATURE = 0x00000400,
        /// <summary>
        /// 
        /// </summary>
        PSP_DEFAULT  =0x00000000,
        /// <summary>
        /// 
        /// </summary>
        PSP_USEREFPARENT =0x00000040,
        /// <summary>
        /// 
        /// </summary>
        PSP_HASHELP = 0x00000020
    }
    [Flags]
    internal enum x02cf16fd44571d3c
    {
        x1187b3e5bf919b50 = 0x20000000,
        x17f743b28e9fdff5 = 0x400,
        x72436d2ced8291be = 0x8000,
        x85ce713a02c95e0b = 0x20000000,
        x8b628d76181ad3f2 = 0x20,
        x8ef67a514d8c0f3c = 0x10000000,
        xabecc423bf19d198 = 0x10,
        xc1fe5ebcb77e6353 = 0x4000,
        xc6204c6cd0f4009b = 0x100000,
        xfc2fc37134c21d51 = 0
    }


    internal enum x2370ad8218c5b454 : uint
    {
        x4fc8bfd0f16f31b4 = 0x40,
        xda9160f15ec46eb4 = 8
    }


    internal enum x879758f4cab61ddd
    {
        xae1bd93d002e5fd5 = 1,
        xf22ca9d72ffb7392 = 2
    }
}
