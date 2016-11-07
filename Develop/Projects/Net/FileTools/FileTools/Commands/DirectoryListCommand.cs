using System.Windows.Forms;
using FileTools.Controls;
using FileTools.Core;

namespace FileTools.Commands
{
    public class DirectoryListCommand : BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryListCommand"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public DirectoryListCommand(ILog log)
            : base(log)
        { }


        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter, object target)
        {
            using (var form = new FormFileSelector())
            {
                form.Text = "Print file list";
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                foreach (var file in form.GetFiles())
                {
                    AddLog(file.FullName);
                }
            }
        }
    }
}
