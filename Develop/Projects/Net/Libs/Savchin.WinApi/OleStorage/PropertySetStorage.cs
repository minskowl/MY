using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Savchin.WinApi.OleStorage
{
    internal enum STGFMT
    {
        STGFMT_ANY = 4,
        STGFMT_DOCFILE = 5,
        STGFMT_FILE = 3,
        STGFMT_STORAGE = 0
    }





    [ComVisible(false)]
    public class PropertySetStorage : MarshalByRefObject, IDisposable
    {
        // Fields
        public static Guid FMTID_DocSummaryInformation = new Guid("{D5CDD502-2E9C-101B-9397-08002B2CF9AE}");
        public static Guid FMTID_InternetSite = new Guid("{000214A1-0000-0000-C000-000000000046}");
        public static Guid FMTID_Intshcut = new Guid("{000214A0-0000-0000-C000-000000000046}");
        public static Guid FMTID_SummaryInformation = new Guid("{F29F85E0-4FF9-1068-AB91-08002B27B3D9}");
        public static Guid FMTID_UserProperties = new Guid("{D5CDD505-2E9C-101B-9397-08002B2CF9AE}");
        private bool m_IgnoreClose;
        private IPropertySetStorage m_IPropSetObj;

        // Methods
        internal PropertySetStorage(IStorage storage)
        {
            this.m_IPropSetObj = (IPropertySetStorage)storage;
            this.m_IgnoreClose = true;
        }

        public PropertySetStorage(string filename)
        {
            if (Functions.IsAtLeastW2K())
            {
                STGFMT Fmt;
                Guid IID_IPropertySetStorage = new Guid("0000013A-0000-0000-C000-000000000046");
                if (API.StgIsStorageFile(filename) == 0)
                {
                    Fmt = STGFMT.STGFMT_DOCFILE;
                }
                else
                {
                    Fmt = STGFMT.STGFMT_FILE;
                }
                int Res = API.StgCreateStorageEx(filename, 0x12, Fmt, 0, 0, 0, ref IID_IPropertySetStorage, ref this.m_IPropSetObj);
                if (Res == -2147286960)
                {
                    Res = API.StgOpenStorageEx(filename, 0x12, Fmt, 0, 0, 0, ref IID_IPropertySetStorage, ref this.m_IPropSetObj);
                    if (Res != 0)
                    {
                        Marshal.ThrowExceptionForHR(Res);
                    }
                }
                else
                {
                    Marshal.ThrowExceptionForHR(Res);
                }
            }
            else
            {
                Storage Stg = new Storage(filename);
                this.m_IPropSetObj = (IPropertySetStorage)Stg.IStorageObj;
            }
        }

        public void Close()
        {
            if (!this.m_IgnoreClose && (this.m_IPropSetObj != null))
            {
                Marshal.ReleaseComObject(this.m_IPropSetObj);
                this.m_IPropSetObj = null;
                GC.SuppressFinalize(this);
            }
        }

        public PropertyStorage Create(Guid formatID)
        {

            return this.Create(formatID, Flags.Default, Modes.ShareExclusive | Modes.AccessReadWrite, Guid.Empty);
        }

        public PropertyStorage Create(Guid formatID, Flags flags)
        {

            return this.Create(formatID, flags, Modes.ShareExclusive | Modes.AccessReadWrite, Guid.Empty);
        }

        public PropertyStorage Create(Guid formatID, Modes mode)
        {

            return this.Create(formatID, Flags.Default, mode, Guid.Empty);
        }

        public PropertyStorage Create(Guid formatID, Flags flags, Modes mode)
        {
            return this.Create(formatID, flags, mode, Guid.Empty);
        }

        public PropertyStorage Create(Guid formatID, Flags flags, Modes mode, Guid classID)
        {
            return new PropertyStorage(this.m_IPropSetObj.Create(ref formatID, classID, (int)flags, mode), this);
        }

        public StatPropSetStgCollection Elements()
        {
            return new StatPropSetStgCollection(this.m_IPropSetObj);
        }



        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            this.Close();
        }
        /// <summary>
        /// Opens the or create.
        /// </summary>
        /// <param name="formatID">The format ID.</param>
        /// <returns></returns>
        public PropertyStorage OpenOrCreate(Guid formatID)
        {
            try
            {
                return Open(formatID);
            }
            catch
            {
                return Create(formatID);
            }
        }
        /// <summary>
        /// Opens the specified format ID.
        /// </summary>
        /// <param name="formatID">The format ID.</param>
        /// <returns></returns>
        public PropertyStorage Open(Guid formatID)
        {
            return this.Open(formatID, Modes.ShareExclusive | Modes.AccessReadWrite);
        }

        public PropertyStorage Open(Guid formatID, Modes mode)
        {
            return new PropertyStorage(this.m_IPropSetObj.Open(ref formatID, mode), this);
        }

        public void Remove(StatPropSetStg stat)
        {
            this.Remove(stat.FmtID);
        }

        public void Remove(Guid formatID)
        {
            this.m_IPropSetObj.Delete(ref formatID);
        }

        // Nested Types
        [Flags]
        public enum Flags
        {
            ANSI = 2,
            Default = 0,
            NonSimple = 1,
            Unbuffered = 4
        }

        [ComVisible(false)]
        public sealed class StatPropSetStgCollection : ReadOnlyCollectionBase
        {
            // Methods
            internal StatPropSetStgCollection(IPropertySetStorage propertySetStorage)
            {
                StatPropSetStg Stat;
                int fetched;
                IEnumSTATPROPSETSTG oEnum = propertySetStorage.Enum();
            Label_0024:
                fetched = 0;
                if (oEnum.Next(1, out Stat, out fetched) == 0)
                {
                    base.InnerList.Add(Stat);
                    goto Label_0024;
                }
                Marshal.ReleaseComObject(oEnum);
            }

            // Properties
            public StatPropSetStg this[Guid formatID]
            {
                get
                {
                    StatPropSetStg Stat = new StatPropSetStg();
                    Stat.FmtID = formatID;
                    int Idx = base.InnerList.IndexOf(Stat);
                    if (Idx < 0)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return (StatPropSetStg)base.InnerList[Idx];
                }
            }

            public StatPropSetStg this[int index]
            {
                get
                {
                    return (StatPropSetStg)base.InnerList[index];
                }
            }
        }
    }


    internal sealed class Functions
    {
        // Methods
        public static bool IsAtLeastW2K()
        {
            OperatingSystem v = Environment.OSVersion;
            if (v.Platform != PlatformID.Win32NT)
            {
                return false;
            }
            if (v.Version.Major < 5)
            {
                return false;
            }
            v = null;
            return true;
        }
    }



}
