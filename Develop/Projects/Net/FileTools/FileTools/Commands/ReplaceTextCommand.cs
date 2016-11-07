using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FileTools.Controls;
using FileTools.Core;

namespace FileTools.Commands
{
    public class ReplaceTextCommand : BaseCommand
    {
 



        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTextCommand"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public ReplaceTextCommand(ILog log)
            : base(log)
        {

        }


        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter, object target)
        {
            FileInfo[] files;
            string pattern;
            string replaceText;
            using (var fbd = new FormReplaceInFiles())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                files = fbd.SelectedFiles;
                pattern = fbd.SearchText;
                replaceText= fbd.ReplaceText;
            }


            Regex regex = new Regex(pattern);
            AddLog("Start replace");
            foreach (var file in files)
            {
                AddLog("Replace in file: " + file.FullName);
                string content = File.ReadAllText(file.FullName);
                File.WriteAllText(file.FullName, regex.Replace( content, replaceText));
            }
            AddLog("End replace");
        }


      


    }
}
