#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KnowledgeBase.Controls
{
    public class HelpTopicButton : Label //TemplateControl
    {


        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write( " [<a href='#'>?</a>]");
        }
        //protected override void CreateChildControls()
        //{

            
        //    // Add Literal Control

        //    Controls.Add(new LiteralControl(Text + " ["));
            
        //    // Add Textbox
        //    HyperLink link= new HyperLink();
        //    link.Text = "?";
        //    link.Target = "#";
 
        //    Controls.Add(link);

        //    // Add Literal Control

        //    Controls.Add(new LiteralControl("]"));



        //}


    }
}
