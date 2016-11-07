using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Savchin.WinApi.OleStorage
{
[ComVisible(false)]
public sealed class Storage : MarshalByRefObject, IDisposable
{
    // Fields
    private IStorage m_IStorageObj;
    private Storage m_oParent;

    // Methods
    public Storage()
    {
        int Res = API.StgOpenStorage(null, null, 0x4001012, 0, 0, out m_IStorageObj);
        if (Res != 0)
        {
            Marshal.ThrowExceptionForHR(Res);
        }
    }

    public Storage(string filename) : this(filename, Modes.ShareExclusive | Modes.AccessReadWrite, false, false)
    {
    }

    internal Storage(IStorage storage, Storage parent)
    {
        this.m_IStorageObj = storage;
        this.m_oParent = parent;
    }

    public Storage(string filename, Modes mode) : this(filename, mode, false, false)
    {
    }

    public Storage(string filename, Modes mode, bool createPropSets, bool useAnsiProperties)
    {
        int Res = API.StgCreateDocfile(filename, (int) mode, 0, ref this.m_IStorageObj);
        if (Res == API.STG_E_FILEALREADYEXISTS)
        {
            Res = API.StgOpenStorage(filename, null, (int) mode, 0, 0, out m_IStorageObj);
            if (Res != 0)
            {
                Marshal.ThrowExceptionForHR(Res);
            }
        }
        else
        {
            Marshal.ThrowExceptionForHR(Res);
        }
        if (createPropSets)
        {
            this.CreatePropertySets(useAnsiProperties);
        }
    }

    public void Close()
    {
        if (this.m_IStorageObj != null)
        {
            Marshal.ReleaseComObject(this.m_IStorageObj);
            this.m_IStorageObj = null;
            this.m_oParent = null;
            GC.SuppressFinalize(this);
        }
    }

    public void CopyElement(string elementName, Storage destStorage)
    {
        this.m_IStorageObj.MoveElementTo(elementName, destStorage.IStorageObj, elementName, 1);
    }

    public void CopyElement(string elementName, Storage destStorage, string newName)
    {
        this.m_IStorageObj.MoveElementTo(elementName, destStorage.IStorageObj, newName, 1);
    }

    public void CreatePropertySets()
    {
        this.CreatePropertySets(false);
    }

    public void CreatePropertySets(bool useAnsi)
    {
        PropertySetStorage.Flags Flags;
        Guid g=System.Guid.Empty;
        IPropertySetStorage PropSetStg = (IPropertySetStorage) this.m_IStorageObj;
        if (useAnsi)
        {
            Flags = PropertySetStorage.Flags.ANSI;
        }
        else
        {
            Flags = PropertySetStorage.Flags.Default;
        }
        PropSetStg.Create(ref PropertySetStorage.FMTID_SummaryInformation, ref g, (int) Flags, Modes.Create | Modes.ShareExclusive | Modes.AccessReadWrite);
        PropSetStg.Create(ref PropertySetStorage.FMTID_DocSummaryInformation, ref g, (int) Flags, Modes.Create | Modes.ShareExclusive | Modes.AccessReadWrite);
        PropSetStg.Create(ref PropertySetStorage.FMTID_UserProperties, ref g, (int) Flags, Modes.Create | Modes.ShareExclusive | Modes.AccessReadWrite);
    }

    public Storage CreateStorage(string name)
    {
        return this.CreateStorage(name, Modes.ShareExclusive | Modes.AccessReadWrite);
    }

    public Storage CreateStorage(string name, Modes flags)
    {
        return new Storage(this.m_IStorageObj.CreateStorage(name, flags, 0, 0), this);
    }

    public Stream CreateStream(string name)
    {
        return this.CreateStream(name, Modes.ShareExclusive | Modes.AccessReadWrite);
    }

    public Stream CreateStream(string name, Modes flags)
    {
        return new Stream(this.m_IStorageObj.CreateStream(name, flags, 0, 0), this);
    }

    public void DestroyElement(string name)
    {
        this.m_IStorageObj.DestroyElement(name);
    }

    public StorageElementsCollection Elements()
    {
        return new StorageElementsCollection(this.m_IStorageObj);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <filterpriority>2</filterpriority>
    public void Dispose()
    {
        this.Close();
    }

    public void Flush()
    {
        this.m_IStorageObj.Commit(CommitFlags.Default);
    }

    public void Flush(CommitFlags flags)
    {
        this.m_IStorageObj.Commit(flags);
    }

    public static bool IsCompoundStorageFile(string filename)
    {
        return (API.StgIsStorageFile(filename) == 0);
    }

    public void MoveElement(string elementName, Storage destStorage)
    {
        this.m_IStorageObj.MoveElementTo(elementName, destStorage.IStorageObj, elementName, 0);
    }

    public void MoveElement(string elementName, Storage destStorage, string newName)
    {
        this.m_IStorageObj.MoveElementTo(elementName, destStorage.IStorageObj, newName, 0);
    }

    public Storage OpenStorage(string name)
    {
        return this.OpenStorage(name, Modes.ShareExclusive | Modes.AccessReadWrite);
    }

    public Storage OpenStorage(string name, Modes flags)
    {
        return new Storage(this.m_IStorageObj.OpenStorage(name, null, flags, 0, 0), this);
    }

    public Stream OpenStream(string name)
    {
        return this.OpenStream(name, Modes.ShareExclusive | Modes.AccessReadWrite);
    }

    public Stream OpenStream(string name, Modes flags)
    {
        return new Stream(this.m_IStorageObj.OpenStream(name, 0, flags, 0), this);
    }

    public void RenameElement(string oldName, string newName)
    {
        this.m_IStorageObj.RenameElement(oldName, newName);
    }

    public void Revert()
    {
        this.m_IStorageObj.Revert();
    }

    public void SwitchToFile(string filename)
    {
        ((IRootStorage) this.m_IStorageObj).SwitchToFile(filename);
    }

    // Properties
    public Guid Guid
    {
        get
        {
            Guid Guid=System.Guid.Empty;
            API.ReadClassStg(this.m_IStorageObj, ref Guid);
            return Guid;
        }
        set
        {
            API.WriteClassStg(this.m_IStorageObj, ref value);
        }
    }

    internal IStorage IStorageObj
    {
        get
        {
            return this.m_IStorageObj;
        }
    }

    public Storage Parent
    {
        get
        {
            return this.m_oParent;
        }
    }

    public PropertySetStorage PropertySetStorage
    {
        get
        {
            return new PropertySetStorage(this.m_IStorageObj);
        }
    }

    public StatStg Stat
    {
        get
        {
            StatStg Stat;
            this.m_IStorageObj.Stat(out Stat, StatStg.Flags.Default);
            return Stat;
        }
    }

    public StatStg this[StatStg.Flags flags]
    {
        get
        {
            StatStg Stat;
            this.m_IStorageObj.Stat(out Stat, flags);
            return Stat;
        }
    }

    // Nested Types

    public sealed class StorageElementsCollection : ReadOnlyCollectionBase
    {
        // Methods
        internal StorageElementsCollection(IStorage storage)
        {
            StatStg tSSTG;
           
            IEnumSTATSTG oEnm = storage.EnumElements(0, 0, 0);
        Label_0027:
           var fetched = 0;
        if (oEnm.Next(1, out tSSTG, out fetched) == 0)
            {
                base.InnerList.Add(tSSTG);
                goto Label_0027;
            }
            Marshal.ReleaseComObject(oEnm);
            oEnm = null;
        }

        // Properties
        public StatStg this[int index]
        {
            get
            {
                return (StatStg) base.InnerList[index];
            }
        }

        public StatStg this[string name]
        {
            get
            {
                StatStg Stat= new StatStg();
                Stat.m_Name = name;
                int Idx = base.InnerList.IndexOf(Stat);
                if (Idx < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                return (StatStg) base.InnerList[Idx];
            }
        }
    }
}

    [Flags]
    public enum Modes
    {
        AccessRead = 0,
        AccessReadWrite = 2,
        AccessWrite = 1,
        Convert = 0x20000,
        Create = 0x1000,
        DeleteOnRelease = 0x4000000,
        Direct_SWMR = 0x400000,
        FailIfThere = 0,
        ModeDirect = 0,
        ModeSimple = 0x8000000,
        ModeTransacted = 0x10000,
        NoScratch = 0x100000,
        NoSnapShot = 0x200000,
        Priority = 0x40000,
        ShareDenyNone = 0x40,
        ShareDenyRead = 0x30,
        ShareDenyWrite = 0x20,
        ShareExclusive = 0x10
    }

    [Flags]
    public enum CommitFlags
    {
        Consolidate = 8,
        DangeroslyCommitMerelyToDiskCache = 4,
        Default = 0,
        OnlyIfCurrent = 2,
        Overwrite = 1
    }


    [StructLayout(LayoutKind.Explicit), ComVisible(false)]
    public struct StatStg
    {
        [MarshalAs(UnmanagedType.LPWStr), FieldOffset(0)]
        internal string m_Name;
        [FieldOffset(4)]
        private ElementType m_Type;
        [FieldOffset(8)]
        private long m_Size;
        [FieldOffset(0x10)]
        private long m_mtime;
        [FieldOffset(0x18)]
        private long m_ctime;
        [FieldOffset(0x20)]
        private long m_atime;
        [FieldOffset(40)]
        private Modes m_Mode;
        [FieldOffset(0x2c)]
        private LockTypes m_LocksSupported;
        [FieldOffset(0x30)]
        private Guid m_Clsid;
        [FieldOffset(0x34)]
        private int m_StateBits;
        [FieldOffset(0x38)]
        private int m_Reserved;
        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }
        public ElementType Type
        {
            get
            {
                return this.m_Type;
            }
        }
        public long Size
        {
            get
            {
                return this.m_Size;
            }
        }
        public Modes Mode
        {
            get
            {
                return this.m_Mode;
            }
        }
        public LockTypes LocksSupported
        {
            get
            {
                return this.m_LocksSupported;
            }
        }
        public Guid ClassID
        {
            get
            {
                return this.m_Clsid;
            }
        }
        public DateTime CreationTime
        {
            get
            {
                return DateTime.FromFileTime(this.m_ctime);
            }
        }
        public DateTime LastModifiedTime
        {
            get
            {
                return DateTime.FromFileTime(this.m_mtime);
            }
        }
        public DateTime LastAccessTime
        {
            get
            {
                return DateTime.FromFileTime(this.m_atime);
            }
        }
        public override bool Equals(object objA)
        {
            return (objA.GetHashCode() == this.GetHashCode());
        }

        public override int GetHashCode()
        {
            return this.Name.ToUpper().GetHashCode();
        }
        // Nested Types
        public enum ElementType
        {
            LockBytes = 3,
            Property = 4,
            Storage = 1,
            Stream = 2
        }

        [Flags]
        public enum Flags
        {
            Default,
            NoName,
            NoOpen
        }

        [Flags]
        public enum LockTypes
        {
            Exclusive = 2,
            OnlyOnce = 4,
            Write = 1
        }
    }

 

}
