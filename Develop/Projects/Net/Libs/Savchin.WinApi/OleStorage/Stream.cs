using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Savchin.WinApi.OleStorage
{
    [ComVisible(false)]
    public sealed class Stream : System.IO.Stream
    {
        // Fields
        private bool m_IsMemoryBased;
        private IStream m_IStreamObj;
        private Storage m_ParentStorage;

        // Methods
        public Stream(bool deleteOnRelease)
        {
            this.m_IsMemoryBased = false;
            this.m_IsMemoryBased = true;
            IntPtr global = new IntPtr(0);
            int Res = API.CreateStreamOnHGlobal(global, deleteOnRelease, out this.m_IStreamObj);
            if (Res != 0)
            {
                Marshal.ThrowExceptionForHR(Res);
            }
        }

        public Stream(string filename)
            : this(filename, Modes.ShareExclusive | Modes.AccessReadWrite)
        {
        }

        internal Stream(IStream stream, Storage parent)
        {
            this.m_IsMemoryBased = false;
            this.m_IStreamObj = stream;
            this.m_ParentStorage = parent;
        }

        public Stream(bool deleteOnRelease, IntPtr globalMemoryHandle)
        {
            IStream NewStream;
            this.m_IsMemoryBased = false;
            this.m_IsMemoryBased = true;
            int Res = API.CreateStreamOnHGlobal(globalMemoryHandle, deleteOnRelease, out NewStream);
            if (Res != 0)
            {
                Marshal.ThrowExceptionForHR(Res);
            }
        }

        public Stream(string filename, Modes flags)
        {
            this.m_IsMemoryBased = false;
            int Res = API.SHCreateStreamOnFile(ref filename, (int)flags, ref this.m_IStreamObj);
            if (Res != 0)
            {
                Marshal.ThrowExceptionForHR(Res);
            }
        }

        public Stream Clone()
        {
            return new Stream(this.m_IStreamObj.Clone(), this.m_ParentStorage);
        }

        public override void Close()
        {
            if (this.m_IStreamObj != null)
            {
                Marshal.ReleaseComObject(this.m_IStreamObj);
                this.m_IStreamObj = null;
                this.m_ParentStorage = null;
                GC.SuppressFinalize(this);
            }
        }

        public long CopyTo(Stream stream)
        {
            return CopyTo(stream, long.MaxValue);
        }

        public long CopyTo(Stream stream, long length)
        {
            long Read;
            long Written;
            this.m_IStreamObj.CopyTo(this.IStreamObj, length, out Read, out Written);
            return Written;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Close();
        }


        public override void Flush()
        {
            this.Flush(CommitFlags.Default);
        }

        public void Flush(CommitFlags flags)
        {
            this.m_IStreamObj.Commit((int)flags);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int Read;
            IntPtr Data = Marshal.AllocCoTaskMem(count);
            try
            {
                Read = this.m_IStreamObj.Read(Data, count);
                Marshal.Copy(Data, buffer, offset, Read);
            }
            finally
            {
                Marshal.FreeCoTaskMem(Data);
            }
            return Read;
        }

        public void Revert()
        {
            this.m_IStreamObj.Revert();
        }

        public long Seek(long newPos)
        {
            return this.m_IStreamObj.Seek(newPos, 1);
        }

        public override long Seek(long newPos, SeekOrigin origin)
        {
            return this.m_IStreamObj.Seek(newPos, (int)origin);
        }

        public override void SetLength(long value)
        {
            this.m_IStreamObj.SetSize(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            IntPtr Data = Marshal.AllocCoTaskMem(count);
            try
            {
                Marshal.Copy(buffer, offset, Data, count);
                this.m_IStreamObj.Write(Data, count);
            }
            finally
            {
                Marshal.FreeCoTaskMem(Data);
            }
        }

        // Properties
        public override bool CanRead
        {
            get
            {
                var Mode = Stat.Mode;

                if ((Mode & Modes.AccessReadWrite) == Modes.AccessReadWrite || (Mode & Modes.AccessWrite) == 0) 
                {
                    return  true;
                }
                return false;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                var mode = Stat.Mode;

                if ((mode & Modes.AccessReadWrite) == Modes.AccessReadWrite || (mode & Modes.AccessWrite) == Modes.AccessWrite)
                {
                    return true;
                }
                return false;
            }
        }

        public IntPtr GlobalMemoryHandle
        {
            get
            {
                IntPtr GlobalMemoryHandle =new IntPtr();
                if (!this.m_IsMemoryBased)
                {
                    throw new InvalidOperationException("The stream is not memory based.");
                }
                int Res = API.GetHGlobalFromStream(this.m_IStreamObj, ref GlobalMemoryHandle);
                if (Res != 0)
                {
                    Marshal.ThrowExceptionForHR(Res);
                }
                return GlobalMemoryHandle;
            }
        }

        public Guid Guid
        {
            get
            {
                Guid Guid= System.Guid.NewGuid();
                API.ReadClassStm(this.m_IStreamObj, ref Guid);
                return Guid;
            }
            set
            {
                API.WriteClassStm(this.m_IStreamObj, ref value);
            }
        }

        internal IStream IStreamObj
        {
            get
            {
                return this.m_IStreamObj;
            }
        }

        public override long Length
        {
            get
            {
                return this.Stat.Size;
            }
        }

        public Storage Parent
        {
            get
            {
                return this.m_ParentStorage;
            }
        }

        public override long Position
        {
            get
            {
                return this.m_IStreamObj.Seek(0L, 1);
            }
            set
            {
                this.m_IStreamObj.Seek(value, 0);
            }
        }

        public StatStg this[StatStg.Flags flags]
        {
            get
            {
                StatStg Stat;
                this.m_IStreamObj.Stat(out Stat, flags);
                return Stat;
            }
        }

        public StatStg Stat
        {
            get
            {
                StatStg Stat;
                this.m_IStreamObj.Stat(out Stat, StatStg.Flags.Default);
                return Stat;
            }
        }
    }





}
