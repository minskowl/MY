using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.SiteCore;
using Savchin.Core;
using Savchin.Web.Core;
using Savchin.Web.UI;
using Telerik.Web.UI;

[assembly: WebResource(KnowledgeBase.Controls.EmbeddedResources.JsKeywordListControl, KnowledgeBase.Controls.EmbeddedResources.JavaScript, PerformSubstitution = true)]


namespace KnowledgeBase.Controls
{
    internal static partial class EmbeddedResources
    {
        internal const string JsKeywordListControl = namespaceName + "KeywordListControl.js";
        //KnowledgeBase.Controls.KeywordListControl.js


    }
    public class KeywordListControl : CompositeControl
    {
        private readonly Panel panel = new Panel();
        private readonly RadComboBox combo = new RadComboBox();
        private readonly ButtonEx addButton = new ButtonEx();

        private readonly List<Variable> items = new List<Variable>();
        private readonly List<string> newKeywords = new List<string>();

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [allow custom text].
        /// </summary>
        /// <value><c>true</c> if [allow custom text]; otherwise, <c>false</c>.</value>
        public bool AllowCustomText
        {
            get { return combo.AllowCustomText; }
            set { combo.AllowCustomText = value; }
        }
        /// <summary>
        /// Gets the keywords ID.
        /// </summary>
        /// <value>The keywords ID.</value>
        public IList<int> KeywordsID
        {
            get
            {
                var result = new List<int>();
                int id;
                foreach (Variable item in items)
                {
                    id = int.Parse(item.Value);
                    if (id > 0)
                        result.Add(id);
                }
                return result;
            }
            set
            {
                FillItems(value);
            }
        }



        /// <summary>
        /// Gets the name of the JS object.
        /// </summary>
        /// <value>The name of the JS object.</value>
        public string JSObjectName
        {
            get { return ClientID + "Obj"; }
        }

        public List<string> NewKeywords
        {
            get { return newKeywords; }
        }

        #endregion



        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            combo.ID = "combo";
            combo.ShowToggleImage = true;
            combo.ShowMoreResultsBox = true;
            combo.EnableLoadOnDemand = true;
            combo.MarkFirstMatch = true;
            combo.EnableVirtualScrolling = true;
            combo.ItemsRequested += new RadComboBoxItemsRequestedEventHandler(_combo_ItemsRequested);
            panel.Controls.Add(combo);

            addButton.ID = "addButton";
            addButton.Mode = ButtonEx.ButtonType.Link;
            addButton.ImageUrl = ImagePathProvider.AddImage;
            addButton.CausesValidation = false;
            addButton.UseSubmitBehavior = false;

            panel.Controls.Add(addButton);

            panel.ID = "panel";

            Controls.Add(panel);

        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
                return;

            FillItems(ExtractValues());
            int newId = -1;
            foreach (string s in NewKeywords)
            {
                items.Add(new Variable(s, newId.ToString()));
                newId--;
            }
        }

        private List<int> ExtractValues()
        {
            string controlId = UniqueID + "$item";

            var ids = new List<int>();
            foreach (string key in Page.Request.Form.AllKeys)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith(controlId))
                {
                    string value = Page.Request.Form[key];
                    int id;
                    if (int.TryParse(value, out id))
                    {
                        ids.Add(id);
                    }
                    else if (!string.IsNullOrEmpty(value))
                    {
                        NewKeywords.Add(value);
                    }
                }
            }
            return ids;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            addButton.OnClientClick = JSObjectName + ".AddItem();";

            if (DesignMode)
                return;

            Page.ClientScript.RegisterClientScriptResource(typeof(KeywordListControl), EmbeddedResources.JsKeywordListControl);

            string initscript = String.Format(@"
var {0} = new KeywordListControl('{0}','{1}','{2}','{3}','{4}');
",
                                  JSObjectName,
                                  ClientID,
                           Savchin.Web.Core.TypeSerializer<List<Variable>>.ToJsonString(items),
                                  AppSettings.ApplicationImagesUrl,
                                  combo.ClientID);

            Page.ClientScript.RegisterStartupScript(GetType(),
                                                    "KeywordListControl" + ClientID,
                                                    initscript,
                                                    true);
        }

        private void FillItems(IList<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                items.Clear();
                return;
            }
            var keywords = KbContext.CurrentKb.ManagerKeyword.GetByListID(ids);

            foreach (var keyword in keywords)
            {
                items.Add(new Variable(keyword.Name, keyword.KeywordID.ToString()));
            }
        }

        /// <summary>
        /// Handles the ItemsRequested event of the _combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs"/> instance containing the event data.</param>
        protected void _combo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<Keyword> words = KbContext.CurrentKb.ManagerKeyword.FindByName(e.Text);



            try
            {
                int itemsPerRequest = 10;
                int itemOffset = e.NumberOfItems;
                int endOffset = itemOffset + itemsPerRequest;
                if (endOffset > words.Count)
                {
                    endOffset = words.Count;
                }
                if (endOffset == words.Count)
                {
                    e.EndOfItems = true;
                }
                else
                {
                    e.EndOfItems = false;
                }
                for (int i = itemOffset; i < endOffset; i++)
                {
                    Keyword word = words[i];
                    combo.Items.Add(new RadComboBoxItem(word.Name, word.KeywordID.ToString()));
                }

                if (words.Count > 0)
                {
                    e.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), words.Count.ToString());
                }
                else
                {
                    e.Message = "No matches";
                }
            }
            catch
            {
                e.Message = "No matches";
            }

        }


    }
}
