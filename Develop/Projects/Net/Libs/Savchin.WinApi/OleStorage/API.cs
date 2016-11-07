using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Savchin.WinApi.OleStorage
{

    internal sealed class API
    {
        // Fields
        public const int S_OK = 0;
        public const int STG_E_FILEALREADYEXISTS = -2147286960;

        // Methods
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int CreateStreamOnHGlobal(IntPtr Global, [MarshalAs(UnmanagedType.Bool)] bool DeleteOnRelease, [MarshalAs(UnmanagedType.Interface)] out IStream Stream);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int GetHGlobalFromStream([MarshalAs(UnmanagedType.Interface)] IStream pstm, ref IntPtr phglobal);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int ReadClassStg([MarshalAs(UnmanagedType.Interface)] IStorage pStg, ref Guid pclsid);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int ReadClassStm([MarshalAs(UnmanagedType.Interface)] IStream pStg, ref Guid pclsid);
        [DllImport("shlwapi", EntryPoint = "SHCreateStreamOnFileW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int SHCreateStreamOnFile([MarshalAs(UnmanagedType.VBByRefStr)] ref string FileName, int Flags, [MarshalAs(UnmanagedType.Interface)] ref IStream Stream);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int StgCreateDocfile([MarshalAs(UnmanagedType.LPWStr)] string Name, int mode, int reserved, ref IStorage Stg);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int StgCreateStorageEx([MarshalAs(UnmanagedType.LPWStr)] string pwcsName, int grfMode, STGFMT stgfmt, int grfAttrs, int pStgOptions, int reserved2, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] ref IPropertySetStorage ppObjectOpen);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int StgIsStorageFile([MarshalAs(UnmanagedType.LPWStr)] string Filename);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int StgOpenStorage([MarshalAs(UnmanagedType.LPWStr)] string Name, [MarshalAs(UnmanagedType.Interface)] IStorage Priority, int mode, int snbExclude, int reserved, out IStorage Stg);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int StgOpenStorageEx([MarshalAs(UnmanagedType.LPWStr)] string pwcsName, int grfMode, STGFMT stgfmt, int grfAttrs, int pStgOptions, int reserved2, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] ref IPropertySetStorage ppObjectOpen);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int WriteClassStg([MarshalAs(UnmanagedType.Interface)] IStorage pStg, ref Guid pclsid);
        [DllImport("ole32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern int WriteClassStm([MarshalAs(UnmanagedType.Interface)] IStream pStg, ref Guid pclsid);
    }

 

}
