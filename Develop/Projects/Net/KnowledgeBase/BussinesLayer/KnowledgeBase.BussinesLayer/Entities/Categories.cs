

/******************************************
 * Auto-generated by CodeRocket
 * 24.05.2008 18:23:30
 ******************************************/
using System;
using System.Data;
using System.Xml.Serialization;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Core;
using KnowledgeBase.DAL;
using Savchin.Validation;

namespace KnowledgeBase.BussinesLayer
{

    /// <summary>
    /// Base Category
    /// </summary>
    [Serializable]
    public class Category : EntityBase<CategoryValue>
    {


        #region Properties



        /// <summary>
        /// Gets or sets the CategoryID.
        /// </summary>
        /// <value>The CategoryID.</value>   
        [XmlElement(ElementName = "CategoryID")]
        public System.Int32 CategoryID
        {
            get { return ObjectValue.CategoryID; }
            set { ObjectValue.CategoryID = value; }
        }

        /// <summary>
        /// Gets or sets the CreationDate.
        /// </summary>
        /// <value>The CreationDate.</value>   
        [XmlElement(ElementName = "CreationDate")]
        public System.DateTime CreationDate
        {
            get { return ObjectValue.CreationDate; }
            set { ObjectValue.CreationDate = value; }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>   
        [XmlElement(ElementName = "Name")]
        [RequiredFieldValidation()]
        public System.String Name
        {
            get { return ObjectValue.Name; }
            set { ObjectValue.Name = value; }
        }

        /// <summary>
        /// Gets or sets the ParentCategoryID.
        /// </summary>
        /// <value>The ParentCategoryID.</value>   
        [XmlElement(ElementName = "ParentCategoryID")]
        public System.Int32? ParentCategoryID
        {
            get { return ObjectValue.ParentCategoryID; }
            set { ObjectValue.ParentCategoryID = value; }
        }
        private Category parentCategory;
        /// <summary>
        /// Gets the parent category.
        /// </summary>
        /// <value>The parent category.</value>
        [XmlIgnore()]
        public Category ParentCategory
        {
            get
            {
                if (parentCategory == null && Identifier.IsValid(ParentCategoryID))
                {
                    parentCategory = Context.ManagerCategory.GetByID(ParentCategoryID.Value);
                }
                return parentCategory;
            }
        }

        #region Security
        /// <summary>
        /// Gets a value indicating whether this instance can edit.
        /// </summary>
        /// <value><c>true</c> if this instance can edit; otherwise, <c>false</c>.</value>
        public bool CanEdit
        {
            get { return Permission.CanEditCategory(); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can view.
        /// </summary>
        /// <value><c>true</c> if this instance can view; otherwise, <c>false</c>.</value>
        public bool CanView
        {
            get { return Permission.CanViewCategory(); }
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


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category()
            : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Category(CategoryValue value)
            : base(value)
        {

        }







    }
}
