using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;


namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// A default template for both, normal and edit mode of a OdcTreeNode.
    /// </summary>
    internal class DefaultTemplate : IBindableTemplate
    {
        public bool EditMode { get; private set; }

        public DefaultTemplate(bool editMode)
            : base()
        {
            this.EditMode = editMode;
        }

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            OdcTreeNodeContainer item = (OdcTreeNodeContainer)container;
            OdcTreeNode node = item.Node;

            if (!string.IsNullOrEmpty(node.ImageUrl))
            {
                System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                image.ImageUrl = node.ImageUrl;
                image.Style.Add(HtmlTextWriterStyle.PaddingRight, "4px");
                image.CssClass = "cnt";
                //image.Style.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
                image.EnableViewState = false;
                container.Controls.Add(image);
            }

            if (node.ShowCheckBox.HasValue && node.ShowCheckBox.Value == true)
            {
                WebControl cb = new WebControl(HtmlTextWriterTag.Input);
                //cb.Style.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
                cb.CssClass = "cnt";
                cb.Attributes.Add("type", "checkbox");
                cb.EnableViewState = false;
                cb.Attributes.Add("event", "check");
                //                    cb.Height = 9;
                if (node.IsChecked) cb.Attributes.Add("checked", "checked");
                container.Controls.Add(cb);
            }

            if (EditMode)
            {
                TextBox tb = new TextBox();
                tb.BorderStyle = BorderStyle.None;
                tb.EnableViewState = false;
                tb.Text = node.Text;
                tb.Attributes.Add("id", "edit");
                container.Controls.Add(tb);
            }
            else
            {
                Literal literal = new Literal();

                literal.EnableViewState = false;
                literal.Mode = LiteralMode.Encode;
                literal.Text = node.Text;
                container.Controls.Add(literal);
            }
        }


        #endregion

        #region IBindableTemplate Members

        /// <summary>
        /// When implemented by a class, retrieves a set of name/value pairs for values bound using two-way ASP.NET data-binding syntax within the templated content.
        /// </summary>
        /// <param name="container">The <see cref="T:System.Web.UI.Control"/> from which to extract name/value pairs, which are passed by the data-bound control to an associated data source control in two-way data-binding scenarios.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Specialized.IOrderedDictionary"/> of name/value pairs. The name represents the name of a control within templated content, and the value is the current value of a property value bound using two-way ASP.NET data-binding syntax.
        /// </returns>
        public System.Collections.Specialized.IOrderedDictionary ExtractValues(Control container)
        {
            OrderedDictionary d = new OrderedDictionary();
            return d;
        }

        #endregion
    }
}