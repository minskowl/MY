using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Savchin.WinApi.Shell
{
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000010e-0000-0000-C000-000000000046")]
    public interface IOleDataObject
    {
        [PreserveSig]
        int GetData(ref FORMATETC pformatetc, ref STGMEDIUM pmedium);
        [PreserveSig]
        int GetDataHere(ref FORMATETC pformatetc, ref STGMEDIUM pmedium);
        [PreserveSig]
        int QueryGetData(ref FORMATETC pformatetc);
        [PreserveSig]
        int GetCanonicalFormatEtc(ref FORMATETC pFormatetcIn, ref FORMATETC pFormatetcOut);
        [PreserveSig]
        int SetData(ref FORMATETC pformatetc, ref STGMEDIUM pmedium, [MarshalAs(UnmanagedType.Bool)] bool fRelease);
        [PreserveSig]
        int EnumFormatEtc(int dwDirection, IntPtr ppenumFormatetc);
        [PreserveSig]
        int DAdvise(ref FORMATETC pformatetc, int advf, IntPtr pAdvSink, ref int pdwConnection);
        [PreserveSig]
        int DUnadvise(int dwConnection);
        [PreserveSig]
        int EnumDAdvise(IntPtr ppenumAdvise);
    }

 

 

}
