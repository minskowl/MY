using System;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.OleStorage
{
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000000b-0000-0000-C000-000000000046")]
    internal interface IStorage
    {
        [return: MarshalAs(UnmanagedType.Interface)]
        IStream CreateStream([In, MarshalAs(UnmanagedType.LPWStr)] string Name, [In] Modes grfMode, [In, Optional, DefaultParameterValue(0)] int reserved1, [In, Optional, DefaultParameterValue(0)] int reserved2);
        [return: MarshalAs(UnmanagedType.Interface)]
        IStream OpenStream([In, MarshalAs(UnmanagedType.LPWStr)] string Name, [In] int reserved1, [In] Modes grfMode, [In, Optional, DefaultParameterValue(0)] int reserved2);
        [return: MarshalAs(UnmanagedType.Interface)]
        IStorage CreateStorage([In, MarshalAs(UnmanagedType.LPWStr)] string Name, [In] Modes grfMode, [In, Optional, DefaultParameterValue(0)] int reserved1, [In, Optional, DefaultParameterValue(0)] int reserved2);
        [return: MarshalAs(UnmanagedType.Interface)]
        IStorage OpenStorage([In, MarshalAs(UnmanagedType.LPWStr)] string Name, [In, MarshalAs(UnmanagedType.Interface)] IStorage pstgPriority, [In] Modes grfMode, [In] int snbExclude, [In, Optional, DefaultParameterValue(0)] int reserved);
        void CopyTo([In] int ciidExclude, [In, MarshalAs(UnmanagedType.LPStruct)] Guid[] rgiidExclude, [In] int snbExclude, [In, MarshalAs(UnmanagedType.Interface)] IStorage pstgDest);
        void MoveElementTo([In, MarshalAs(UnmanagedType.LPWStr)] string Name, [In, MarshalAs(UnmanagedType.Interface)] IStorage pstgDest, [In, MarshalAs(UnmanagedType.LPWStr)] string pwcsNewName, [In] int grfFlags);
        void Commit([In] CommitFlags grfCommitFlags);
        void Revert();
        [return: MarshalAs(UnmanagedType.Interface)]
        IEnumSTATSTG EnumElements([In, Optional, DefaultParameterValue(0)] int reserved1, [In, Optional, DefaultParameterValue(0)] int reserved2, [In, Optional, DefaultParameterValue(0)] int reserved3);
        void DestroyElement([In, MarshalAs(UnmanagedType.LPWStr)] string Name);
        void RenameElement([In, MarshalAs(UnmanagedType.LPWStr)] string OldName, [In, MarshalAs(UnmanagedType.LPWStr)] string NewName);
        void SetElementTimes([In, MarshalAs(UnmanagedType.LPWStr)] string Name, [In] ref long pctime, [In] ref long patime, [In] ref long pmtime);
        void SetClass([In] ref Guid clsid);
        void SetStateBits([In] int grfStateBits, [In] int grfMask);
        void Stat(out StatStg pstatstg, [In] StatStg.Flags grfStatFlag);
    }



}