using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Savchin.WinApi.OleStorage
{
    public enum PRPSPEC
    {
        PRSPEC_INVALID = -1,
        PRSPEC_LPWSTR = 0,
        PRSPEC_PROPID = 1
    }


    [ComVisible(false)]
    public sealed class PropertyStorage : MarshalByRefObject, IDisposable
    {
        // Fields
        private IPropertyStorage m_IPropStgObj;
        private PropertySetStorage m_Parent;
        private bool m_UseUnicode;

        // Methods
        internal PropertyStorage(IPropertyStorage propertyStorage, PropertySetStorage parent)
        {
            this.m_IPropStgObj = propertyStorage;
            this.m_Parent = parent;
            this.m_UseUnicode = (this.Stat.Flags & PropertySetStorage.Flags.ANSI) == PropertySetStorage.Flags.Default;
        }

        public void Clear()
        {
            StatPropStg StatProp;

            IEnumSTATPROPSTG oEnum = this.m_IPropStgObj.Enum();
        Label_003E:
            var fetched = 0;
            if (oEnum.Next(1, out StatProp, out fetched) == 0)
            {
                if (StatProp.PropName == null)
                {
                    this.Remove(StatProp.PropID);
                }
                else
                {
                    this.Remove(StatProp.PropName);
                }
                goto Label_003E;
            }
            Marshal.ReleaseComObject(oEnum);
        }

        public void Close()
        {
            if (this.m_IPropStgObj != null)
            {
                Marshal.ReleaseComObject(this.m_IPropStgObj);
                this.m_IPropStgObj = null;
                GC.SuppressFinalize(this);
            }
        }

        public void Flush()
        {
            this.m_IPropStgObj.Commit(CommitFlags.Default);
        }

        public void Flush(CommitFlags flags)
        {
            this.m_IPropStgObj.Commit(flags);
        }

        public void Remove(int propertyID)
        {
            PROPSPEC PROP;
            PROP.ulKind = PRPSPEC.PRSPEC_PROPID;
            PROP.Name_Or_ID = new IntPtr(propertyID);
            this.m_IPropStgObj.DeleteMultiple(1, ref PROP);
        }

        public void Remove(string propertyName)
        {
            if (propertyName[0].ToString() == "#")
            {
                this.Remove(Convert.ToInt32(propertyName.Substring(1)));
            }
            else
            {
                PROPSPEC PROP;
                PROP.ulKind = PRPSPEC.PRSPEC_LPWSTR;
                PROP.Name_Or_ID = Marshal.StringToCoTaskMemUni(propertyName);
                this.m_IPropStgObj.DeleteMultiple(1, ref PROP);
                Marshal.FreeCoTaskMem(PROP.Name_Or_ID);
            }
        }

        public void Revert()
        {
            this.m_IPropStgObj.Revert();
        }

        // Properties
        public StatPropStgCollection Elements
        {
            get
            {
                return new StatPropStgCollection(this.m_IPropStgObj);
            }
        }
        private object _lock= new object();

        public object this[int propertyID]
        {
            get
            {
                PROPSPEC PROP;
                PROPVARIANT value;
                PROP.ulKind = PRPSPEC.PRSPEC_PROPID;
                PROP.Name_Or_ID = new IntPtr(propertyID);
                this.m_IPropStgObj.ReadMultiple(1, ref PROP, out value);
                return value.ToObject(_lock);
            }
            set
            {
                PROPSPEC PROP;
                PROP.ulKind = PRPSPEC.PRSPEC_PROPID;
                PROP.Name_Or_ID = new IntPtr(propertyID);
                PROPVARIANT VariantValue = new PROPVARIANT();
                VariantValue.Init(RuntimeHelpers.GetObjectValue(value));
                int Res = this.m_IPropStgObj.WriteMultiple(1, ref PROP, ref VariantValue, 2);
                VariantValue.Clear();
                if (Res != 0)
                {
                    Marshal.ThrowExceptionForHR(Res);
                }
            }
        }

        public object this[string propertyName]
        {
            get
            {
                PROPSPEC PROP;
                PROPVARIANT value;
                if (propertyName[0].ToString() == "#")
                {
                    return this[Convert.ToInt32(propertyName.Substring(1))];
                }
                PROP.ulKind = PRPSPEC.PRSPEC_LPWSTR;
                PROP.Name_Or_ID = Marshal.StringToCoTaskMemUni(propertyName);
                this.m_IPropStgObj.ReadMultiple(1, ref PROP, out value);
                Marshal.FreeCoTaskMem(PROP.Name_Or_ID);
                return value.ToObject(_lock);
            }
            set
            {
                if (propertyName[0].ToString() == "#")
                {
                    this[Convert.ToInt32(propertyName.Substring(1))] = RuntimeHelpers.GetObjectValue(value);
                }
                else
                {
                    PROPSPEC PROP;
                    PROP.ulKind = PRPSPEC.PRSPEC_LPWSTR;
                    PROP.Name_Or_ID = Marshal.StringToCoTaskMemUni(propertyName);
                    PROPVARIANT Var = new PROPVARIANT();
                    Var.Init(RuntimeHelpers.GetObjectValue(value));
                    int Res = this.m_IPropStgObj.WriteMultiple(1, ref PROP, ref Var, 2);
                    Marshal.FreeCoTaskMem(PROP.Name_Or_ID);
                    Var.Clear();
                    if (Res != 0)
                    {
                        Marshal.ThrowExceptionForHR(Res);
                    }
                }
            }
        }

        public object this[StatPropStg stat]
        {
            get
            {
                if (stat.PropName == null)
                {
                    return this[stat.PropID];
                }
                return this[stat.PropName];
            }
            set
            {
                if (stat.PropName == null)
                {
                    this[stat.PropID] = RuntimeHelpers.GetObjectValue(value);
                }
                else
                {
                    this[stat.Name] = RuntimeHelpers.GetObjectValue(value);
                }
            }
        }

        public StatPropSetStg Stat
        {
            get
            {
                return this.m_IPropStgObj.Stat();
            }
        }

        // Nested Types
        public enum DocSummaryProperty
        {
            ByteCount = 4,
            Category = 2,
            Company = 15,
            DocParts = 13,
            HeadingPair = 12,
            HiddenCount = 9,
            LineCount = 5,
            LinksDirty = 0x10,
            Manager = 14,
            MMClipCount = 10,
            NoteCount = 8,
            ParCount = 6,
            PresFormat = 3,
            Scale = 11,
            SlideCount = 7
        }

        public enum GlobalProperty
        {
            Behavior = -2147483645,
            CodePage = 1,
            Dictionary = 0,
            Locale = -2147483648,
            ModifyTime = -2147483647,
            Security = -2147483646
        }

        [ComVisible(false)]
        public sealed class StatPropStgCollection : ReadOnlyCollectionBase
        {
            // Methods
            internal StatPropStgCollection(IPropertyStorage propertyStorage)
            {
                StatPropStg Stat;

                IEnumSTATPROPSTG oEnum = propertyStorage.Enum();
            Label_0024:
                var fetched = 0;
                if (oEnum.Next(1, out Stat, out fetched) == 0)
                {
                    base.InnerList.Add(Stat);
                    goto Label_0024;
                }
                Marshal.ReleaseComObject(oEnum);
                oEnum = null;
            }

            // Properties
            public StatPropStg this[string name]
            {
                get
                {
                    StatPropStg statProp= new StatPropStg {PropName = name};
                    int Idx = base.InnerList.IndexOf(statProp);
                    if (Idx < 0)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return (StatPropStg)base.InnerList[Idx];
                }
            }

            public StatPropStg this[int index]
            {
                get
                {
                    return (StatPropStg)base.InnerList[index];
                }
            }
        }

        public enum SummaryProperty
        {
            ApplicationName = 0x12,
            Author = 4,
            CharCount = 0x10,
            Comments = 6,
            CreateDTM = 12,
            DocSecurity = 0x13,
            EditTime = 10,
            Keywords = 5,
            LastAuthor = 8,
            LastPrinted = 11,
            LastSaveDTM = 13,
            PageCount = 14,
            RevisionNumber = 9,
            Subject = 3,
            Template = 7,
            Thumbnail = 0x11,
            Title = 2,
            WordCount = 15
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}