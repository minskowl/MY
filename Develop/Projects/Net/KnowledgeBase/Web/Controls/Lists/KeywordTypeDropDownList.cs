using System;
using System.Collections.Generic;
using System.Text;
using KnowledgeBase.BussinesLayer;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    public class KeywordTypeDropDownList : DropDownListEx
    {

        /// <summary>
        /// Gets or sets the type of the selected knowledge.
        /// </summary>
        /// <value>The type of the selected knowledge.</value>
        public KeywordType SelectedKeywordType
        {
            get { return (KeywordType) SelectedIntValue; }
            set { SelectedIntValue = (int) value; }
        }

        /// <summary>
        /// Handles the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Items.Clear();
            if (!DesignMode)
                AddItems(typeof (KeywordType));

        }
    }
}
