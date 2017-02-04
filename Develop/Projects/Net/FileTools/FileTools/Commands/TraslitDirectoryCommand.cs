using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FileTools.Controls;
using FileTools.Core;
using Savchin.Text;


namespace FileTools.Commands
{
    public class TraslitDirectoryCommand : BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraslitDirectoryCommand"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public TraslitDirectoryCommand(ILog log): base(log)
        {
        }
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            AddLog("Start translit");
            FileInfo[] files;
            using (var form = new FormFileSelector())
            {
                form.Text = "Print file list";
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                files = form.GetFiles();

            }
            var dirs = new List<DirectoryInfo>();
            foreach (var file in files)
            {
                TranslitFile(file);
                if (!dirs.Contains(file.Directory))
                    dirs.Add(file.Directory);
            }
            foreach (var dir in dirs)
            {
                TranslitDir(dir);
            }
            AddLog("End translit");
        }


        private void TranslitDir(DirectoryInfo dir)
        {
            if (StringUtil.HasRussianChars(dir.Name))
            {
                string newName = StringUtil.Traslit(dir.Name);
                AddLog("Rename  dir ::" + dir.Name);
                AddLog("to :: " + newName);
                Directory.Move(dir.FullName, Path.Combine(Path.GetDirectoryName(dir.FullName), newName));
            }
        }

        private void TranslitFile(FileInfo file)
        {


            if (StringUtil.HasRussianChars(file.Name))
            {
                string newName = StringUtil.Traslit(file.Name);
                AddLog("Rename file ::" + file.Name);
                AddLog("to :: " + newName);
                File.Move(file.FullName, Path.Combine(Path.GetDirectoryName(file.FullName), newName));
            }
            //MP3 tags= new MP3();
            //tags.fileComplete = file.FullName;
            //Mp3Tool.ReadMP3Tag(ref tags);

            // regex.Matches(file.)



            Application.DoEvents();
        }
    }
}
