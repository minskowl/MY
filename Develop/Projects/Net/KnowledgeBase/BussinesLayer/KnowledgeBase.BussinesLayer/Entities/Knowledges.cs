using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Core;
using KnowledgeBase.DAL;
using Savchin.Validation;

namespace KnowledgeBase.BussinesLayer
{
    /// <summary>
    /// 
    /// </summary>
    public enum KnowledgeType
    {
        /// <summary>
        /// 
        /// </summary>
        Info = 1,
        /// <summary>
        /// 
        /// </summary>
        Error = 2,
        /// <summary>
        /// 
        /// </summary>
        Solution = 3,
        /// <summary>
        /// 
        /// </summary>
        Resource = 4
    }

    /// <summary>
    /// KnowledgeStatus
    /// </summary>
    public enum KnowledgeStatus : byte
    {
        /// <summary>
        /// New
        /// </summary>
        New = 0,
        /// <summary>
        /// Active
        /// </summary>
        Active = 1,
        /// <summary>
        ///Hide
        /// </summary>
        Hide = 2,
        /// <summary>
        /// Deleted
        /// </summary>
        Deleted = 3
    }

    /// <summary>
    /// Base Knowledge
    /// </summary>
    [Serializable]
    public class Knowledge : EntityBase<KnowledgeValue>
    {
        #region Properties

        private User creator;
        private IList<int> kewordsAssociations;
        private User modificator;
        private Category _category;

        #region Security
        /// <summary>
        /// Gets a value indicating whether this instance can edit.
        /// </summary>
        /// <value><c>true</c> if this instance can edit; otherwise, <c>false</c>.</value>
        public bool CanEdit
        {
            get
            {
                return Permission.CanEditKnowledge(KnowledgeStatus, CreatorID);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can view.
        /// </summary>
        /// <value><c>true</c> if this instance can view; otherwise, <c>false</c>.</value>
        public bool CanView
        {
            get { return Permission.CanViewKnowledge(KnowledgeStatus); }
        }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <value>The permission.</value>
        public Permission Permission
        {
            get
            {
                return (Identifier.IsValid(CategoryID))
                           ? Context.GetPermissionsForCategory(CategoryID)
                           : Permission.None;
            }
        } 
        #endregion
        #region Refrences
        /// <summary>
        /// Gets the kewords associations.
        /// </summary>
        /// <value>The kewords associations.</value>
        [XmlIgnore]
        public Category Category
        {
            get
            {
                if (_category == null && Identifier.IsValid(CategoryID))
                    _category = Context.ManagerCategory.GetByID(CategoryID);

                return _category;
            }
        }

        /// <summary>
        /// Gets the creator.
        /// </summary>
        /// <value>The creator.</value>
        [XmlIgnore]
        public User Creator
        {
            get
            {
                if (creator == null && Identifier.IsValid(CreatorID))
                {
                    creator = Context.ManagerUser.GetByID(CreatorID);
                }
                return creator;
            }
        }

        /// <summary>
        /// Gets the modificator.
        /// </summary>
        /// <value>The modificator.</value>
        [XmlIgnore]
        public User Modificator
        {
            get
            {
                if (modificator == null && Identifier.IsValid(ModificatorID))
                {
                    modificator = Context.ManagerUser.GetByID(ModificatorID.Value);
                }
                return modificator;
            }
        } 
        #endregion

        /// <summary>
        /// Gets or sets the CreationDate.
        /// </summary>
        /// <value>The CreationDate.</value>   
        [XmlElement(ElementName = "CreationDate")]
        public DateTime CreationDate
        {
            get { return ObjectValue.CreationDate; }
            set { ObjectValue.CreationDate = value; }
        }

        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        /// <value>The category ID.</value>
        [XmlElement(ElementName = "CategoryID")]
        [RangeValidationAttribute(MinValue = 1, Message = "Category is required.")]
        public Int32 CategoryID
        {
            get { return ObjectValue.CategoryID; }
            set { ObjectValue.CategoryID = value; }
        }

        /// <summary>
        /// Gets or sets the CreatorID.
        /// </summary>
        /// <value>The CreatorID.</value>   
        [XmlElement(ElementName = "CreatorID")]
        public Int32 CreatorID
        {
            get { return ObjectValue.CreatorID; }
            set { ObjectValue.CreatorID = value; }
        }

        /// <summary>
        /// Gets or sets the KnowledgeID.
        /// </summary>
        /// <value>The KnowledgeID.</value>   
        [XmlElement(ElementName = "KnowledgeID")]
        public Int32 KnowledgeID
        {
            get { return ObjectValue.KnowledgeID; }
            set { ObjectValue.KnowledgeID = value; }
        }

        /// <summary>
        /// Gets or sets the KnowledgeTypeID.
        /// </summary>
        /// <value>The KnowledgeTypeID.</value>   
        [XmlElement(ElementName = "KnowledgeType")]
        [RangeValidationAttribute(MinValue= KnowledgeType.Info, MaxValue = KnowledgeType.Resource, Message = "Type is required.")]
        public KnowledgeType KnowledgeType
        {
            get { return (KnowledgeType) ObjectValue.KnowledgeTypeID; }
            set { ObjectValue.KnowledgeTypeID = (short) value; }
        }

        /// <summary>
        /// Gets or sets the knowledge status.
        /// </summary>
        /// <value>The knowledge status.</value>
        public KnowledgeStatus KnowledgeStatus
        {
            get { return (KnowledgeStatus) ObjectValue.KnowledgeStatusID; }
            set { ObjectValue.KnowledgeStatusID = (byte) value; }
        }

        /// <summary>
        /// Gets or sets the ModificationDate.
        /// </summary>
        /// <value>The ModificationDate.</value>   
        [XmlElement(ElementName = "ModificationDate")]
        public DateTime? ModificationDate
        {
            get { return ObjectValue.ModificationDate; }
            set { ObjectValue.ModificationDate = value; }
        }

        /// <summary>
        /// Gets or sets the ModificatorID.
        /// </summary>
        /// <value>The ModificatorID.</value>   
        [XmlElement(ElementName = "ModificatorID")]
        public Int32? ModificatorID
        {
            get { return ObjectValue.ModificatorID; }
            set { ObjectValue.ModificatorID = value; }
        }

        /// <summary>
        /// Gets or sets the Summary.
        /// </summary>
        /// <value>The Summary.</value>   
        [XmlElement(ElementName = "Summary")]
        [RequiredFieldValidation]
        public String Summary
        {
            get { return ObjectValue.Summary; }
            set { ObjectValue.Summary = value; }
        }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        /// <value>The Title.</value>   
        [XmlElement(ElementName = "Title")]
        [RequiredFieldValidation]
        public String Title
        {
            get { return ObjectValue.Title; }
            set { ObjectValue.Title = value; }
        }

        /// <summary>
        /// Gets the kewords associations.
        /// </summary>
        /// <value>The kewords associations.</value>
        [XmlIgnore]
        public IList<int> KewordsAssociations
        {
            get
            {
                if (kewordsAssociations == null && Identifier.IsValid(KnowledgeID))
                    kewordsAssociations = Context.ManagerKnowledge.GetKeywordsAssociations(KnowledgeID);

                return kewordsAssociations;
            }
        }

        /// <summary>
        /// Gets the public ID.
        /// </summary>
        /// <value>The public ID.</value>
        public Guid PublicID
        {
            get { return ObjectValue.PublicID; }
        }
        /// <summary>
        /// Gets the public id string.
        /// </summary>
        /// <value>The public id string.</value>
        public string PublicIdString
        {
            get { return ObjectValue.PublicID.ToString().Replace("-", string.Empty); }
        }
        #endregion



        /// <summary>
        /// Gets the public URL.
        /// </summary>
        /// <param name="baseurl">The baseurl.</param>
        /// <returns></returns>
        public string GetPublicUrl(string baseurl)
        {
            return baseurl +
                ((baseurl.EndsWith("/")) ? "KnowledgeInfo.aspx?ID=" : "/KnowledgeInfo.aspx?ID=") +PublicIdString;
        }


    }
}