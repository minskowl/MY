using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Savchin.WinApi.OleStorage
{
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000000c-0000-0000-C000-000000000046")]
    internal interface IStream
    {
        int Read([Out] IntPtr pv, [In] int cb);
        int Write([In] IntPtr pv, [In] int cb);
        long Seek([In] long dlibMove, [In] int dwOrigin);
        void SetSize([In] long libNewSize);
        void CopyTo([In, MarshalAs(UnmanagedType.Interface)] IStream pstm, [In] long cb, out long pcbRead, out long pcbWritten);
        void Commit([In] int grfCommitFlags);
        void Revert();
        void LockRegion([In] long libOffset, [In] long cb, [In] int dwLockType);
        void UnlockRegion([In] long libOffset, [In] long cb, [In] int dwLockType);
        void Stat(out StatStg pstatstg, [In] StatStg.Flags grfStatFlag);
        [return: MarshalAs(UnmanagedType.Interface)]
        IStream Clone();
    }




    [ComImport, Guid("0000000d-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumSTATSTG
    {
        [PreserveSig]
        int Next([In] int celt, out StatStg rgelt, [Optional, DefaultParameterValue(0)] out int pceltFetched);
        void Skip([In] int celt);
        void Reset();
        [return: MarshalAs(UnmanagedType.Interface)]
        IEnumSTATSTG Clone();
    }

 

 

    [ComImport, Guid("0000013A-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertySetStorage
    {
        [return: MarshalAs(UnmanagedType.Interface)]
        IPropertyStorage Create([In, MarshalAs(UnmanagedType.Struct)] ref Guid rfmtid, [In, MarshalAs(UnmanagedType.Struct)] ref Guid pclsid, [In] int grfFlags, [In] Modes grfMode);
        [return: MarshalAs(UnmanagedType.Interface)]
        IPropertyStorage Open([In] ref Guid rfmtid, [In] Modes grfMode);
        void Delete([In] ref Guid rfmtid);
        [return: MarshalAs(UnmanagedType.Interface)]
        IEnumSTATPROPSETSTG Enum();
    }
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000013B-0000-0000-C000-000000000046")]
    internal interface IEnumSTATPROPSETSTG
    {
        [PreserveSig]
        int Next([In] int celt, [MarshalAs(UnmanagedType.Struct)] out StatPropSetStg Stat, [Optional, DefaultParameterValue(0)] out int pceltFetched);
        void Skip([In] int celt);
        void Reset();
        [return: MarshalAs(UnmanagedType.Interface)]
        IEnumSTATPROPSETSTG Clone();
    }
    
    [ComImport, InterfaceType((short)1), Guid("00000012-0000-0000-C000-000000000046")]
    public interface IRootStorage
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SwitchToFile([In, MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }


    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Size = 8)]
    public struct PROPSPEC
    {
        // Fields
        [FieldOffset(4)]
        public IntPtr Name_Or_ID;
        [FieldOffset(0)]
        public PRPSPEC ulKind;
    }

 

 


    public enum VariantType
    {
        Empty = 0,
        Null = 1,
        Short = 2,
        Integer = 3,
        Single = 4,
        Double = 5,
        Currency = 6,
        Date = 7,
        String = 8,
        Object = 9,
        Error = 10,
        Boolean = 11,
        Variant = 12,
        DataObject = 13,
        Decimal = 14,
        Byte = 17,
        Char = 18,
        Long = 20,
        UserDefinedType = 36,
        Array = 8192,
    }


}
