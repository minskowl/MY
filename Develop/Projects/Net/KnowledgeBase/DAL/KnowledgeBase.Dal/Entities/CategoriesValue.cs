/******************************************
 * Auto-generated by CodeRocket
 * 20.10.2008 23:15:35
 ******************************************/
using System;
using Savchin.Core;


namespace KnowledgeBase.DAL
{

    /// <summary>
    /// Category Value object
    /// </summary>
    [Serializable]
    public class CategoryValue : ICopiable
    {

        public System.Int32 CategoryID;
        public System.DateTime CreationDate;
        public System.String Name;
        public System.Int32? ParentCategoryID;




        /// <summary>
        /// Copies the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        public void Copy(object destination)
        {

            CategoryValue dest = destination as CategoryValue;
            if (dest == null)
                return;
            dest.CategoryID = CategoryID;
            dest.CreationDate = CreationDate;
            dest.Name = Name;
            dest.ParentCategoryID = ParentCategoryID;


        }







    }
}
