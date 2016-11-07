using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FileTools.Controls;
using FileTools.Core;
using Savchin.Text;

namespace FileTools.Commands
{
    public class RenameFilesCommand : BaseCommand
    {




        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTextCommand"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public RenameFilesCommand(ILog log)
            : base(log)
        {

        }


        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            FileInfo[] files;
            string pattern;
            string replaceText;
            bool useRegExp;

            using (var fbd = new FormReplaceInFiles())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                files = fbd.SelectedFiles;
                pattern = fbd.SearchText;
                replaceText = fbd.ReplaceText;
                useRegExp = fbd.UseRegExp;
            }


            AddLog("Start rename");
            foreach (var file in files)
            {
                AddLog("Rename file: " + file.FullName);
                var newName = StringUtil.Replace(file.Name, pattern, replaceText, useRegExp, null);
                try
                {
                    if (newName != file.Name)
                        File.Move(file.FullName, Path.Combine(file.DirectoryName, newName));
                }
                catch (Exception ex)
                {
                    AddLog("Error rename " + ex);
                }
            }
            AddLog("End rename");
        }





    }
}
