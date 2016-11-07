using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Savchin.WinApi.OleStorage
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PROPARRAY
    {
        internal uint cElems;
        internal IntPtr pElems;
    }





    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    internal struct PROPVARIANT
    {
        // Fields
        [FieldOffset(8)]
        internal short boolVal;
        [FieldOffset(8)]
        internal byte bVal;
        [FieldOffset(8)]
        internal PROPARRAY ca;
        [FieldOffset(8)]
        internal sbyte cVal;
        [FieldOffset(8)]
        internal double dblVal;
        [FieldOffset(8)]
        internal FILETIME filetime;
        [FieldOffset(8)]
        internal float fltVal;
        [FieldOffset(8)]
        internal int intVal;
        [FieldOffset(8)]
        internal short iVal;
        [FieldOffset(8)]
        internal long lVal;
        [FieldOffset(8)]
        internal IntPtr pclsidVal;
        [FieldOffset(8)]
        internal IntPtr pszVal;
        [FieldOffset(8)]
        internal IntPtr punkVal;
        [FieldOffset(8)]
        internal IntPtr pwszVal;
        [FieldOffset(8)]
        internal uint uintVal;
        [FieldOffset(8)]
        internal ushort uiVal;
        [FieldOffset(8)]
        internal ulong ulVal;
        [FieldOffset(0)]
        internal ushort varType;
        [FieldOffset(2)]
        internal ushort wReserved1;
        [FieldOffset(4)]
        internal ushort wReserved2;
        [FieldOffset(6)]
        internal ushort wReserved3;

        // Methods
        [SecurityCritical, SecurityTreatAsSafe]
        internal unsafe void Clear()
        {
            VarEnum varType = (VarEnum)this.varType;
            if (((varType & VarEnum.VT_VECTOR) == VarEnum.VT_EMPTY) && (varType != VarEnum.VT_BLOB))
            {
                switch (varType)
                {
                    case VarEnum.VT_LPWSTR:
                    case VarEnum.VT_LPSTR:
                    case VarEnum.VT_CLSID:
                        Marshal.FreeCoTaskMem(this.pwszVal);
                        goto Label_010A;
                }
                if (varType == VarEnum.VT_UNKNOWN)
                {
                    Marshal.FreeHGlobal(this.punkVal);
                }
            }
            else if (this.ca.pElems != IntPtr.Zero)
            {
                switch ((varType & ~VarEnum.VT_VECTOR))
                {
                    case VarEnum.VT_UNKNOWN:


                        IntPtr pElems = this.ca.pElems;
                        int num4 = 0;
                        num4 = sizeof(IntPtr);
                        for (uint i = 0; i < this.ca.cElems; i++)
                        {
                            Marshal.FreeHGlobal(Marshal.ReadIntPtr(pElems, (int)(i * num4)));
                        }
                        break;

                    case VarEnum.VT_LPWSTR:
                    case VarEnum.VT_LPSTR:
                        {
                            IntPtr ptr = this.ca.pElems;
                            int num3 = 0;
                            num3 = sizeof(IntPtr);
                            for (uint j = 0; j < this.ca.cElems; j++)
                            {
                                Marshal.FreeCoTaskMem(Marshal.ReadIntPtr(ptr, (int)(j * num3)));
                            }
                            break;
                        }
                }
                Marshal.FreeCoTaskMem(this.ca.pElems);
            }
        Label_010A:
            varType = VarEnum.VT_EMPTY;
        }

        [SecurityCritical]
        private static unsafe void CopyBytes(byte* pbTo, int cbTo, byte* pbFrom, int cbFrom)
        {
            if (cbFrom > cbTo)
            {
                throw new InvalidOperationException("Image_InsufficientBufferSize");
            }
            byte* numPtr2 = pbFrom;
            byte* numPtr = pbTo;
            for (int i = 0; i < cbFrom; i++)
            {
                numPtr[i] = numPtr2[i];
            }
        }

        [SecurityCritical, SecurityTreatAsSafe]
        internal void Init(object value)
        {
            if (value == null)
            {
                this.varType = 0;
            }
            else if (value is Array)
            {
                Type type2 = value.GetType();
                if (type2 == typeof(sbyte[]))
                {
                    this.InitVector(value as Array, typeof(sbyte), VarEnum.VT_I1);
                }
                else if (type2 == typeof(byte[]))
                {
                    this.InitVector(value as Array, typeof(byte), VarEnum.VT_UI1);
                }
                else if (value is char[])
                {
                    this.varType = 30;
                    this.pszVal = Marshal.StringToCoTaskMemAnsi(new string(value as char[]));
                }
                else if (value is char[][])
                {
                    char[][] chArray = value as char[][];
                    string[] strArray = new string[chArray.GetLength(0)];
                    for (int i = 0; i < chArray.Length; i++)
                    {
                        strArray[i] = new string(chArray[i]);
                    }
                    this.Init(strArray, true);
                }
                else if (type2 == typeof(short[]))
                {
                    this.InitVector(value as Array, typeof(short), VarEnum.VT_I2);
                }
                else if (type2 == typeof(ushort[]))
                {
                    this.InitVector(value as Array, typeof(ushort), VarEnum.VT_UI2);
                }
                else if (type2 == typeof(int[]))
                {
                    this.InitVector(value as Array, typeof(int), VarEnum.VT_I4);
                }
                else if (type2 == typeof(uint[]))
                {
                    this.InitVector(value as Array, typeof(uint), VarEnum.VT_UI4);
                }
                else if (type2 == typeof(long[]))
                {
                    this.InitVector(value as Array, typeof(long), VarEnum.VT_I8);
                }
                else if (type2 == typeof(ulong[]))
                {
                    this.InitVector(value as Array, typeof(ulong), VarEnum.VT_UI8);
                }
                else if (value is float[])
                {
                    this.InitVector(value as Array, typeof(float), VarEnum.VT_R4);
                }
                else if (value is double[])
                {
                    this.InitVector(value as Array, typeof(double), VarEnum.VT_R8);
                }
                else if (value is Guid[])
                {
                    this.InitVector(value as Array, typeof(Guid), VarEnum.VT_CLSID);
                }
                else if (value is string[])
                {
                    this.Init(value as string[], false);
                }
                else
                {
                    if (!(value is bool[]))
                    {
                        throw new InvalidOperationException("Image_PropertyNotSupported");
                    }
                    bool[] flagArray = value as bool[];
                    short[] array = new short[flagArray.Length];
                    for (int j = 0; j < flagArray.Length; j++)
                    {
                        array[j] = flagArray[j] ? ((short)(-1)) : ((short)0);
                    }
                    this.InitVector(array, typeof(short), VarEnum.VT_BOOL);
                }
            }
            else
            {
                Type type = value.GetType();
                if (value is string)
                {
                    this.varType = 0x1f;
                    this.pwszVal = Marshal.StringToCoTaskMemUni(value as string);
                }
                else if (type == typeof(sbyte))
                {
                    this.varType = 0x10;
                    this.cVal = (sbyte)value;
                }
                else if (type == typeof(byte))
                {
                    this.varType = 0x11;
                    this.bVal = (byte)value;
                }
                else if (type == typeof(FILETIME))
                {
                    this.varType = 0x40;
                    this.filetime = (FILETIME)value;
                }
                else if (value is char)
                {
                    this.varType = 30;
                    this.pszVal = Marshal.StringToCoTaskMemAnsi(new string(new char[] { (char)value }));
                }
                else if (type == typeof(short))
                {
                    this.varType = 2;
                    this.iVal = (short)value;
                }
                else if (type == typeof(ushort))
                {
                    this.varType = 0x12;
                    this.uiVal = (ushort)value;
                }
                else if (type == typeof(int))
                {
                    this.varType = 3;
                    this.intVal = (int)value;
                }
                else if (type == typeof(uint))
                {
                    this.varType = 0x13;
                    this.uintVal = (uint)value;
                }
                else if (type == typeof(long))
                {
                    this.varType = 20;
                    this.lVal = (long)value;
                }
                else if (type == typeof(ulong))
                {
                    this.varType = 0x15;
                    this.ulVal = (ulong)value;
                }
                else if (value is float)
                {
                    this.varType = 4;
                    this.fltVal = (float)value;
                }
                else if (value is double)
                {
                    this.varType = 5;
                    this.dblVal = (double)value;
                }
                else if (value is Guid)
                {
                    byte[] source = ((Guid)value).ToByteArray();
                    this.varType = 0x48;
                    this.pclsidVal = Marshal.AllocCoTaskMem(source.Length);
                    Marshal.Copy(source, 0, this.pclsidVal, source.Length);
                }
                else if (value is bool)
                {
                    this.varType = 11;
                    this.boolVal = ((bool)value) ? ((short)(-1)) : ((short)0);
                }
                else
                {
                    throw new NotSupportedException(" Not supported type " + value.GetType().Name);
                }
                //else if (value is BitmapMetadataBlob)
                //{
                //    this.Init((value as BitmapMetadataBlob).InternalGetBlobValue(), typeof(byte), VarEnum.VT_BLOB);
                //}
                //else
                //{
                //    if (!(value is BitmapMetadata))
                //    {
                //        throw new InvalidOperationException(SR.Get("Image_PropertyNotSupported"));
                //    }
                //    IntPtr zero = IntPtr.Zero;
                //    BitmapMetadata metadata = value as BitmapMetadata;
                //    SafeMILHandle internalMetadataHandle = metadata.InternalMetadataHandle;
                //    if ((internalMetadataHandle == null) || internalMetadataHandle.IsInvalid)
                //    {
                //        throw new NotImplementedException();
                //    }
                //    Guid guid = MILGuidData.IID_IWICMetadataQueryReader;
                //    HRESULT.Check(UnsafeNativeMethods.MILUnknown.QueryInterface(internalMetadataHandle, ref guid, out zero));
                //    this.varType = 13;
                //    this.punkVal = zero;
                //}
            }
        }

        [SecurityCritical, SecurityTreatAsSafe]
        internal unsafe void Init(string[] value, bool fAscii)
        {
            this.varType = fAscii ? ((ushort)30) : ((ushort)0x1f);
            this.varType = (ushort)(this.varType | 0x1000);
            this.ca.cElems = 0;
            this.ca.pElems = IntPtr.Zero;
            int length = value.Length;
            if (length > 0)
            {
                IntPtr zero = IntPtr.Zero;
                int num3 = 0;
                num3 = sizeof(IntPtr);
                long num5 = num3 * length;
                int index = 0;
                try
                {
                    IntPtr val = IntPtr.Zero;
                    zero = Marshal.AllocCoTaskMem((int)num5);
                    index = 0;
                    while (index < length)
                    {
                        if (fAscii)
                        {
                            val = Marshal.StringToCoTaskMemAnsi(value[index]);
                        }
                        else
                        {
                            val = Marshal.StringToCoTaskMemUni(value[index]);
                        }
                        Marshal.WriteIntPtr(zero, index * num3, val);
                        index++;
                    }
                    this.ca.cElems = (uint)length;
                    this.ca.pElems = zero;
                    zero = IntPtr.Zero;
                }
                finally
                {
                    if (zero != IntPtr.Zero)
                    {
                        for (int i = 0; i < index; i++)
                        {
                            Marshal.FreeCoTaskMem(Marshal.ReadIntPtr(zero, i * num3));
                        }
                        Marshal.FreeCoTaskMem(zero);
                    }
                }
            }
        }

        [SecurityTreatAsSafe, SecurityCritical]
        internal unsafe void Init(Array array, Type type, VarEnum vt)
        {
            this.varType = (ushort)vt;
            this.ca.cElems = 0;
            this.ca.pElems = IntPtr.Zero;
            int length = array.Length;
            if (length > 0)
            {
                long num = Marshal.SizeOf(type) * length;
                IntPtr zero = IntPtr.Zero;
                GCHandle handle = new GCHandle();
                try
                {
                    zero = Marshal.AllocCoTaskMem((int)num);
                    handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                    CopyBytes((byte*)zero, (int)num, (byte*)handle.AddrOfPinnedObject(), (int)num);
                    this.ca.cElems = (uint)length;
                    this.ca.pElems = zero;
                    zero = IntPtr.Zero;
                }
                finally
                {
                    if (handle.IsAllocated)
                    {
                        handle.Free();
                    }
                    if (zero != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(zero);
                    }
                }
            }
        }

        [SecurityCritical, SecurityTreatAsSafe]
        internal void InitVector(Array array, Type type, VarEnum varEnum)
        {
            this.Init(array, type, varEnum | VarEnum.VT_VECTOR);
        }

        [SecurityTreatAsSafe, SecurityCritical]
        internal unsafe object ToObject(object syncObject)
        {
            VarEnum varType = (VarEnum)this.varType;
            if ((varType & VarEnum.VT_VECTOR) == VarEnum.VT_EMPTY)
            {
                switch (varType)
                {
                    case VarEnum.VT_FILETIME:
                        return this.filetime;

                    case VarEnum.VT_BLOB:

                        //byte[] destination = new byte[this.ca.cElems];
                        //Marshal.Copy(this.ca.pElems, destination, 0, (int)this.ca.cElems);
                        //return new BitmapMetadataBlob(destination);
                        return null;
                    case VarEnum.VT_CLSID:
                        {
                            byte[] buffer2 = new byte[0x10];
                            Marshal.Copy(this.pclsidVal, buffer2, 0, 0x10);
                            return new Guid(buffer2);
                        }
                    case VarEnum.VT_EMPTY:
                        return null;

                    case VarEnum.VT_NULL:
                    case VarEnum.VT_CY:
                    case VarEnum.VT_DATE:
                    case VarEnum.VT_BSTR:
                    case VarEnum.VT_DISPATCH:
                    case VarEnum.VT_ERROR:
                    case VarEnum.VT_VARIANT:
                    case VarEnum.VT_DECIMAL:
                    case (VarEnum.VT_DECIMAL | VarEnum.VT_NULL):
                        goto Label_06B3;

                    case VarEnum.VT_I2:
                        return this.iVal;

                    case VarEnum.VT_I4:
                        return this.intVal;

                    case VarEnum.VT_R4:
                        return this.fltVal;

                    case VarEnum.VT_R8:
                        return this.dblVal;

                    case VarEnum.VT_BOOL:
                        return (this.boolVal != 0);

                    case VarEnum.VT_UNKNOWN:
                        //{
                        //    IntPtr zero = IntPtr.Zero;
                        //    Guid guid2 = MILGuidData.IID_IWICMetadataQueryWriter;
                        //    Guid guid = MILGuidData.IID_IWICMetadataQueryReader;
                        //    try
                        //    {
                        //        if (UnsafeNativeMethods.MILUnknown.QueryInterface(this.punkVal, ref guid2, out zero) == 0)
                        //        {
                        //            SafeMILHandle metadataHandle = new SafeMILHandle(zero, 0L);
                        //            zero = IntPtr.Zero;
                        //            return new BitmapMetadata(metadataHandle, false, false, syncObject);
                        //        }
                        //        int hr = UnsafeNativeMethods.MILUnknown.QueryInterface(this.punkVal, ref guid, out zero);
                        //        if (hr == 0)
                        //        {
                        //            SafeMILHandle handle = new SafeMILHandle(zero, 0L);
                        //            zero = IntPtr.Zero;
                        //            return new BitmapMetadata(handle, true, false, syncObject);
                        //        }
                        //        HRESULT.Check(hr);
                        //    }
                        //    finally
                        //    {
                        //        if (zero != IntPtr.Zero)
                        //        {
                        //            UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref zero);
                        //        }
                        //    }
                        //    goto Label_06B3;
                        //}
                        return null;
                    case VarEnum.VT_I1:
                        return this.cVal;

                    case VarEnum.VT_UI1:
                        return this.bVal;

                    case VarEnum.VT_UI2:
                        return this.uiVal;

                    case VarEnum.VT_UI4:
                        return this.uintVal;

                    case VarEnum.VT_I8:
                        return this.lVal;

                    case VarEnum.VT_UI8:
                        return this.ulVal;

                    case VarEnum.VT_LPSTR:
                        return Marshal.PtrToStringAnsi(this.pszVal);

                    case VarEnum.VT_LPWSTR:
                        return Marshal.PtrToStringUni(this.pwszVal);
                }
            }
            else
            {
                switch ((varType & ~VarEnum.VT_VECTOR))
                {
                    case VarEnum.VT_EMPTY:
                        return null;

                    case VarEnum.VT_NULL:
                    case VarEnum.VT_CY:
                    case VarEnum.VT_DATE:
                    case VarEnum.VT_BSTR:
                    case VarEnum.VT_DISPATCH:
                    case VarEnum.VT_ERROR:
                    case VarEnum.VT_VARIANT:
                    case VarEnum.VT_UNKNOWN:
                    case VarEnum.VT_DECIMAL:
                    case (VarEnum.VT_DECIMAL | VarEnum.VT_NULL):
                        goto Label_06B3;

                    case VarEnum.VT_I2:
                        {
                            short[] numArray8 = new short[this.ca.cElems];
                            Marshal.Copy(this.ca.pElems, numArray8, 0, (int)this.ca.cElems);
                            return numArray8;
                        }
                    case VarEnum.VT_I4:
                        {
                            int[] numArray6 = new int[this.ca.cElems];
                            Marshal.Copy(this.ca.pElems, numArray6, 0, (int)this.ca.cElems);
                            return numArray6;
                        }
                    case VarEnum.VT_R4:
                        {
                            float[] numArray2 = new float[this.ca.cElems];
                            Marshal.Copy(this.ca.pElems, numArray2, 0, (int)this.ca.cElems);
                            return numArray2;
                        }
                    case VarEnum.VT_R8:
                        {
                            double[] numArray = new double[this.ca.cElems];
                            Marshal.Copy(this.ca.pElems, numArray, 0, (int)this.ca.cElems);
                            return numArray;
                        }
                    case VarEnum.VT_BOOL:
                        {
                            bool[] flagArray = new bool[this.ca.cElems];
                            for (int i = 0; i < this.ca.cElems; i++)
                            {
                                flagArray[i] = Marshal.ReadInt16(this.ca.pElems, i * 2) != 0;
                            }
                            return flagArray;
                        }
                    case VarEnum.VT_I1:
                        {
                            sbyte[] numArray9 = new sbyte[this.ca.cElems];
                            for (int j = 0; j < this.ca.cElems; j++)
                            {
                                numArray9[j] = (sbyte)Marshal.ReadByte(this.ca.pElems, j);
                            }
                            return numArray9;
                        }
                    case VarEnum.VT_UI1:
                        {
                            byte[] buffer4 = new byte[this.ca.cElems];
                            Marshal.Copy(this.ca.pElems, buffer4, 0, (int)this.ca.cElems);
                            return buffer4;
                        }
                    case VarEnum.VT_UI2:
                        {
                            ushort[] numArray7 = new ushort[this.ca.cElems];
                            for (int k = 0; k < this.ca.cElems; k++)
                            {
                                numArray7[k] = (ushort)Marshal.ReadInt16(this.ca.pElems, k * 2);
                            }
                            return numArray7;
                        }
                    case VarEnum.VT_UI4:
                        {
                            uint[] numArray5 = new uint[this.ca.cElems];
                            for (int m = 0; m < this.ca.cElems; m++)
                            {
                                numArray5[m] = (uint)Marshal.ReadInt32(this.ca.pElems, m * 4);
                            }
                            return numArray5;
                        }
                    case VarEnum.VT_I8:
                        {
                            long[] numArray4 = new long[this.ca.cElems];
                            Marshal.Copy(this.ca.pElems, numArray4, 0, (int)this.ca.cElems);
                            return numArray4;
                        }
                    case VarEnum.VT_UI8:
                        {
                            ulong[] numArray3 = new ulong[this.ca.cElems];
                            for (int n = 0; n < this.ca.cElems; n++)
                            {
                                numArray3[n] = (ulong)Marshal.ReadInt64(this.ca.pElems, n * 8);
                            }
                            return numArray3;
                        }
                    case VarEnum.VT_LPSTR:
                        {
                            string[] strArray2 = new string[this.ca.cElems];
                            int num11 = 0;
                            num11 = sizeof(IntPtr);
                            for (int num2 = 0; num2 < this.ca.cElems; num2++)
                            {
                                IntPtr ptr = Marshal.ReadIntPtr(this.ca.pElems, num2 * num11);
                                strArray2[num2] = Marshal.PtrToStringAnsi(ptr);
                            }
                            return strArray2;
                        }
                    case VarEnum.VT_LPWSTR:
                        {
                            string[] strArray = new string[this.ca.cElems];
                            int num10 = 0;
                            num10 = sizeof(IntPtr);
                            for (int num = 0; num < this.ca.cElems; num++)
                            {
                                IntPtr ptr2 = Marshal.ReadIntPtr(this.ca.pElems, num * num10);
                                strArray[num] = Marshal.PtrToStringUni(ptr2);
                            }
                            return strArray;
                        }
                    case VarEnum.VT_CLSID:
                        {
                            Guid[] guidArray = new Guid[this.ca.cElems];
                            for (int num3 = 0; num3 < this.ca.cElems; num3++)
                            {
                                byte[] buffer3 = new byte[0x10];
                                Marshal.Copy(this.ca.pElems, buffer3, num3 * 0x10, 0x10);
                                guidArray[num3] = new Guid(buffer3);
                            }
                            return guidArray;
                        }
                }
            }
        Label_06B3:
            throw new NotSupportedException("Image_PropertyNotSupported");
        }

        // Properties
        internal bool RequiresSyncObject
        {
            [SecurityCritical, SecurityTreatAsSafe]
            get
            {
                return (this.varType == 13);
            }
        }
    }



}
