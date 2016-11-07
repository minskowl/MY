using System;
using System.Text;

namespace Savchin.Web.UI
{
	/// <summary>
	/// Summary description for CheckedListBoxDesigner.
	/// </summary>
	public class CheckedListBoxDesigner : System.Web.UI.Design.WebControls.ListControlDesigner
	{

		
		public override string GetDesignTimeHtml() {
			String baseResult = base.GetDesignTimeHtml();
			return baseResult + GetDesignTimeScript();
		}

		protected virtual String GetDesignTimeScript() {
			StringBuilder script = new StringBuilder();
			script.Append("\n<script>\nalert('foo');\n</script>");
			return script.ToString();
		}
	}
}
