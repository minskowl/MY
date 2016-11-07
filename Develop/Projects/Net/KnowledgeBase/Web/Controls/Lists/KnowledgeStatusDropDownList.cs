using System;
using System.Collections.Generic;
using System.Text;
using KnowledgeBase.BussinesLayer;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class KnowledgeStatusDropDownList : DropDownListEx
    {

        /// <summary>
        /// Gets or sets the selected knowledge status.
        /// </summary>
        /// <value>The selected knowledge status.</value>
        public KnowledgeStatus SelectedKnowledgeStatus
        {
            get { return (KnowledgeStatus)SelectedShortValue; }
            set { SelectedShortValue = (byte)value; }
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
                AddItems(typeof(KnowledgeStatus));

        }
    }
}
