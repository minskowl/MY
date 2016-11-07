using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.OleStorage
{
    [ComImport, Guid("00000138-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyStorage
    {
        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        //void ReadMultiple([In] uint cpspec, [In, ComAliasName("Microsoft.VisualStudio.OLE.Interop.PROPSPEC"), MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPSPEC[] rgpspec,
        //    [Out, ComAliasName("Microsoft.VisualStudio.OLE.Interop.PROPVARIANT"), MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPVARIANT[] rgpropvar);
   
        void ReadMultiple([In] int cpspec, [In, Out, MarshalAs(UnmanagedType.Struct)] ref PROPSPEC rgpspec,
            [MarshalAs(UnmanagedType.Struct)] out PROPVARIANT rgpropvar);
        [PreserveSig]
        int WriteMultiple([In] int cpspec,
            [In, MarshalAs(UnmanagedType.Struct)] ref PROPSPEC rgpspec,
            [In] ref PROPVARIANT rgpropvar,
            [In, Optional, DefaultParameterValue(2)] int propidNameFirst);

        void DeleteMultiple([In] int cpspec, [In] ref PROPSPEC rgpspec);
        void ReadPropertyNames([In] long cpropid, [In] ref int rgpropid, [MarshalAs(UnmanagedType.LPWStr)] out string rglpwstrName);
        void WritePropertyNames([In] int cpropid, [In] ref int rgpropid, [In, MarshalAs(UnmanagedType.LPWStr)] ref string rglpwstrName);
        void DeletePropertyNames([In] int cpropid, [In] ref int rgpropid);
        void Commit([In] CommitFlags grfCommitFlags);
        void Revert();
        [return: MarshalAs(UnmanagedType.Interface)]
        IEnumSTATPROPSTG Enum();
        void SetTimes([In] ref long pctime, [In] ref long patime, [In] ref long pmtime);
        void SetClass([In] ref Guid clsid);
        [return: MarshalAs(UnmanagedType.Struct)]
        StatPropSetStg Stat();
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00000139-0000-0000-C000-000000000046")]
    internal interface IEnumSTATPROPSTG
    {
        [PreserveSig]
        int Next([In] int celt, out StatPropStg STATPROPSTG, [Optional, DefaultParameterValue(0)] out int pceltFetched);
        void Skip([In] int celt);
        void Reset();
        [return: MarshalAs(UnmanagedType.Interface)]
        IEnumSTATPROPSTG Clone();
    }


    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Size = 12), ComVisible(false)]
    public struct StatPropStg
    {
        // Fields
        [FieldOffset(4)]
        internal int PropID;
        [MarshalAs(UnmanagedType.LPWStr), FieldOffset(0)]
        internal string PropName;
        [FieldOffset(8)]
        internal short VariantType;

        // Methods
        public override bool Equals(object objA)
        {
            return (objA.GetHashCode() == this.GetHashCode());
        }

        public override int GetHashCode()
        {
            string Name;
            if (this.PropName == null)
            {
                Name = "#" + this.PropID;
            }
            else
            {
                Name = this.PropName;
            }
            return Name.ToUpper().GetHashCode();
        }

        // Properties
        public string Name
        {
            get
            {
                if (this.PropName == null)
                {
                    return ("#" + this.PropID);
                }
                return this.PropName;
            }
        }

        public VarEnum Type
        {
            get
            {
                return (VarEnum)this.VariantType;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct StatPropSetStg
    {
        public Guid FmtID;
        public Guid ClsID;
        public PropertySetStorage.Flags Flags;
        public long mtime;
        public long ctime;
        public long atime;
        public DateTime CreationTime
        {
            get
            {
                return DateTime.FromFileTime(this.ctime);
            }
            set
            {
                this.ctime = value.ToFileTime();
            }
        }
        public DateTime LastModifiedTime
        {
            get
            {
                return DateTime.FromFileTime(this.mtime);
            }
            set
            {
                this.mtime = value.ToFileTime();
            }
        }
        public DateTime LastAccessTime
        {
            get
            {
                return DateTime.FromFileTime(this.atime);
            }
            set
            {
                this.atime = value.ToFileTime();
            }
        }
        public override bool Equals(object objA)
        {
            return this.FmtID.Equals(RuntimeHelpers.GetObjectValue(objA));
        }

        public override int GetHashCode()
        {
            return this.FmtID.GetHashCode();
        }
    }


    //[StructLayout(LayoutKind.Sequential)]
    //internal struct PROPVARIANT
    //{
    //    internal VARTYPE vt;
    //    internal ushort wReserved1;
    //    internal ushort wReserved2;
    //    internal ushort wReserved3;
    //    internal PropVariantUnion union;
    //}



    internal enum VARTYPE : short
    {
        VT_BSTR = 8,
        VT_FILETIME = 0x40,
        VT_LPSTR = 30
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct BLOB
    {
        public uint cbSize;
        public IntPtr pBlobData;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BSTRBLOB
    {
        public uint cbSize;
        public IntPtr pData;
    }






    [StructLayout(LayoutKind.Sequential)]
    internal struct CArray
    {
        public uint cElems;
        public IntPtr pElems;
    }




    [StructLayout(LayoutKind.Sequential)]
    internal struct CY
    {
        public uint Lo;
        public int Hi;
    }

 

 


    [StructLayout(LayoutKind.Explicit)]
    internal struct PropVariantUnion
    {
        // Fields
        [FieldOffset(0)]
        internal BLOB blob;
        [FieldOffset(0)]
        internal short boolVal;
        [FieldOffset(0)]
        internal BSTRBLOB bstrblobVal;
        [FieldOffset(0)]
        internal IntPtr bstrVal;
        [FieldOffset(0)]
        internal byte bVal;
        [FieldOffset(0)]
        internal CArray cArray;
        [FieldOffset(0)]
        internal sbyte cVal;
        [FieldOffset(0)]
        internal CY cyVal;
        [FieldOffset(0)]
        internal double date;
        [FieldOffset(0)]
        internal double dblVal;
        [FieldOffset(0)]
        internal FILETIME filetime;
        [FieldOffset(0)]
        internal float fltVal;
        [FieldOffset(0)]
        internal long hVal;
        [FieldOffset(0)]
        internal int intVal;
        [FieldOffset(0)]
        internal short iVal;
        [FieldOffset(0)]
        internal int lVal;
        [FieldOffset(0)]
        internal IntPtr parray;
        [FieldOffset(0)]
        internal IntPtr pboolVal;
        [FieldOffset(0)]
        internal IntPtr pbstrVal;
        [FieldOffset(0)]
        internal IntPtr pbVal;
        [FieldOffset(0)]
        internal IntPtr pclipdata;
        [FieldOffset(0)]
        internal IntPtr pcVal;
        [FieldOffset(0)]
        internal IntPtr pcyVal;
        [FieldOffset(0)]
        internal IntPtr pdate;
        [FieldOffset(0)]
        internal IntPtr pdblVal;
        [FieldOffset(0)]
        internal IntPtr pdecVal;
        [FieldOffset(0)]
        internal IntPtr pdispVal;
        [FieldOffset(0)]
        internal IntPtr pfltVal;
        [FieldOffset(0)]
        internal IntPtr pintVal;
        [FieldOffset(0)]
        internal IntPtr piVal;
        [FieldOffset(0)]
        internal IntPtr plVal;
        [FieldOffset(0)]
        internal IntPtr pparray;
        [FieldOffset(0)]
        internal IntPtr ppdispVal;
        [FieldOffset(0)]
        internal IntPtr ppunkVal;
        [FieldOffset(0)]
        internal IntPtr pscode;
        [FieldOffset(0)]
        internal IntPtr pStorage;
        [FieldOffset(0)]
        internal IntPtr pStream;
        [FieldOffset(0)]
        internal IntPtr pszVal;
        [FieldOffset(0)]
        internal IntPtr puintVal;
        [FieldOffset(0)]
        internal IntPtr puiVal;
        [FieldOffset(0)]
        internal IntPtr pulVal;
        [FieldOffset(0)]
        internal IntPtr punkVal;
        [FieldOffset(0)]
        internal IntPtr puuid;
        [FieldOffset(0)]
        internal IntPtr pvarVal;
        [FieldOffset(0)]
        internal IntPtr pVersionedStream;
        [FieldOffset(0)]
        internal IntPtr pwszVal;
        [FieldOffset(0)]
        internal int scode;
        [FieldOffset(0)]
        internal ulong uhVal;
        [FieldOffset(0)]
        internal uint uintVal;
        [FieldOffset(0)]
        internal ushort uiVal;
        [FieldOffset(0)]
        internal uint ulVal;
    }



 

 

 

 


 

}