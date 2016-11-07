using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FileTools.Controls;
using FileTools.Core;

namespace FileTools.Commands
{
    class FindNameDuplicatesCommand : BaseCommand
    {
        private Dictionary<string, List<FileInfo>> _map = new Dictionary<string, List<FileInfo>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FindNameDuplicatesCommand"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public FindNameDuplicatesCommand(ILog log)
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
            using (var form = new FormFileSelector())
            {
                form.Text = "Select folders to search duplicates";
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                var files = form.GetFiles();
                log.AddLog("Start search duplicates");
                FindDuplicates(files);
                log.AddLog("Results:");
                PrintResult();
                log.AddLog("End search duplicates");
            }
        }

        /// <summary>
        /// Prints the result.
        /// </summary>
        private void PrintResult()
        {
            foreach (var pair in _map.OrderByDescending(e => e.Value.Count))
            {
                var files = pair.Value;
                if (files.Count == 0) return;
                log.AddLog(string.Format("\nFile: {0} finded {1}", pair.Key, files.Count));
                foreach (var file in files)
                {
                    log.AddLog(file.FullName);
                }
            }
        }

        private void FindDuplicates(FileInfo[] files)
        {
            foreach (var file in files)
            {
                var fileName = file.Name;
                if (_map.ContainsKey(fileName))
                {
                    _map[fileName].Add(file);
                }
                else
                {
                    _map.Add(fileName, new List<FileInfo> { file });
                }
            }

        }
    }
}
