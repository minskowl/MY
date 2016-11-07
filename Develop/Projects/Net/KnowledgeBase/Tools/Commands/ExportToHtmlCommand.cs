using Savchin.Core;
using KnowledgeBase.KbTools.Controls;
using KnowledgeBase.KbTools.Core;
using Savchin.Forms.Core.Commands;

namespace KnowledgeBase.KbTools.Commands
{
    /// <summary>
    /// ExportToHtmlCommand
    /// </summary>
    class ExportToHtmlCommand : Command
    {
        public override void Execute(object parameter, object target)
        {
            AppCore.MainForm.ShowControl(new ExportToHtmlControl());
        }
    }
}
