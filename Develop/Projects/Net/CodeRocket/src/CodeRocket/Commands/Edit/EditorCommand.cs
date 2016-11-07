using CodeRocket.Common;
using Savchin.CodeGeneration.Common;
using Savchin.Core;
using Savchin.Forms.Core.Commands;

namespace CodeRocket.Commands.Edit
{
    abstract class EditorCommand : Command
    {
        /// <summary>
        /// Gets the acive editor.
        /// </summary>
        /// <value>The acive editor.</value>
        protected ICodeEditor AciveEditor
        {
            get { return AppCore.Current.FileTabs.AciveEditor; }
        }

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanExecute(object parameter, object target)
        {
            Enabled = AciveEditor != null;
            return base.CanExecute(parameter, target);
        }
    }
}