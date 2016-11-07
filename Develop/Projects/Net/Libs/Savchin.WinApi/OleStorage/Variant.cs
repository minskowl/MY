using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.OleStorage
{
    [StructLayout(LayoutKind.Explicit, Size = 0x10)]
    internal struct Variant
    {
        // Fields
        [FieldOffset(8)]
        public byte Byte;
        [FieldOffset(8)]
        public long Long;
        [FieldOffset(8)]
        public IntPtr Ptr;
        [FieldOffset(0)]
        public short vt;

        // Methods
        public void Clear()
        {
            if ((((this.vt == 0x1f) || (this.vt == 30)) ? 1 : 0) != 0)
            {
                Marshal.FreeHGlobal(this.Ptr);
            }
            else
            {

                Clear(ref this);
            }
            this.vt = 0;
            this.Long = 0L;
        }

        [DllImport("oleaut32.dll", EntryPoint = "VariantClear")]
        public static extern void Clear(ref Variant Var);

        public static Variant FromObject(object Obj, bool UnicodeStrs)
        {
            var FromObject = new Variant();
            switch (((int)Information.VarType(RuntimeHelpers.GetObjectValue(Obj))))
            {
                case 7:
                    FromObject.vt = 0x40;
                    FromObject.Long = Convert.ToDateTime(Obj).ToFileTime();
                    return FromObject;

                case 8:
                    if (!UnicodeStrs)
                    {
                        FromObject.vt = (short)VarEnum.VT_LPSTR;
                        FromObject.Ptr = Marshal.StringToHGlobalAnsi(Convert.ToString(Obj));
                        return FromObject;
                    }
                    FromObject.vt = (short)VarEnum.VT_LPWSTR;
                    FromObject.Ptr = Marshal.StringToHGlobalUni(Convert.ToString(Obj));
                    return FromObject;

                case 9:
                case 10:
                case 12:
                case 13:
                case 14:
                case 15:
                case 0x10:
                    return FromObject;

                case 11:
                    FromObject.vt = 11;
                    FromObject.Byte = Convert.ToByte(Obj);
                    return FromObject;

                case 0x11:
                    FromObject.vt = 0x11;
                    FromObject.Byte = Convert.ToByte(Obj);
                    return FromObject;
            }
            return FromObject;
        }

        public object ToObject()
        {
            object ToObject;
            var type = (VarEnum)this.vt;
            switch (type)
            {
                case VarEnum.VT_LPSTR:
                    ToObject = Marshal.PtrToStringAnsi(this.Ptr);
                    Marshal.FreeCoTaskMem(this.Ptr);
                    return ToObject;

                case VarEnum.VT_LPWSTR:
                    ToObject = Marshal.PtrToStringUni(this.Ptr);
                    Marshal.FreeCoTaskMem(this.Ptr);
                    return ToObject;

                case VarEnum.VT_FILETIME:
                    return DateTime.FromFileTime(this.Long);

                case VarEnum.VT_I4:
                    return this.Ptr.ToInt32();

                case VarEnum.VT_BOOL:
                    return (this.Byte > 0);
            }
            if (type == VarEnum.VT_EMPTY || type == VarEnum.VT_NULL)
            {
                return DBNull.Value;
            }
            return (VarEnum)this.vt;
        }
    }
}