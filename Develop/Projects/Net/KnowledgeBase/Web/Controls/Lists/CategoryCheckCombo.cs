using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using Savchin.Text;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    public class CategoryCheckBoxCombo : CheckBoxCombo
    {
        private List<Category> categories;

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (DesignMode)
                return;

            categories = KbContext.CurrentKb.ManagerCategory.GetAll();
            Category[] cat = categories.ToArray();
            foreach (Category category in cat)
            {
                if (category.ParentCategoryID == null)
                {
                    ListBox.AddItem(category.Name, category.CategoryID.ToString());
                    categories.Remove(category);
                    PrintCategory(category.CategoryID, 1);
                }
            }
        }

        private void PrintCategory(int id, int level)
        {
            Category[] cat = categories.ToArray();
            foreach (Category category in cat)
            {
                if (category.ParentCategoryID == id)
                {
                    ListBox.AddItem(StringUtil.Clone("|", level) + category.Name, category.CategoryID.ToString());
                    categories.Remove(category);
                    PrintCategory(category.CategoryID, level + 1);
                }
            }
        }
    }
}
