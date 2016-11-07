using System.Windows.Forms;
using FileTools.Controls;
using FileTools.Core;
using Savchin.IO;

namespace FileTools.Commands
{
    public class FileStatisticsCommand : BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryListCommand"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public FileStatisticsCommand(ILog log)
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
                form.Text = "Print file statistics";
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                var files = form.GetFiles();
                var cnt = 0;
                
                foreach (var file in files)
                {
                    var fileCnt = FileHelper.GetLinesCount(file.FullName);
                    AddLog(fileCnt +" lines" + file.FullName );
                    cnt += fileCnt;
                }
                AddLog(string.Format("Find files {0} lines {1}", files.Length, cnt));
            }
        }
    }
}
