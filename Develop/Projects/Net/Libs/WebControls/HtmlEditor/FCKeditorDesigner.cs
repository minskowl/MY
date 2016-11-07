

using System ;
using System.Globalization ;

namespace Savchin.Web.UI
{
	public class FCKeditorDesigner : System.Web.UI.Design.ControlDesigner
	{
		public FCKeditorDesigner()
		{
		}

		public override string GetDesignTimeHtml() 
		{
            HtmlEditorControl control = (HtmlEditorControl)Component;
			return String.Format( CultureInfo.InvariantCulture,
				"<div><table width=\"{0}\" height=\"{1}\" bgcolor=\"#f5f5f5\" bordercolor=\"#c7c7c7\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\"><tr><td valign=\"middle\" align=\"center\">FCKeditor V2 - <b>{2}</b></td></tr></table></div>",
					control.Width,
					control.Height,
					control.ID ) ;
		}
	}
}
