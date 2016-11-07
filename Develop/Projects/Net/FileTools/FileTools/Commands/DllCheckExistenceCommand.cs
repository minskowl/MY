using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FileTools.Controls;
using FileTools.Core;
using Savchin.WinApi;

namespace FileTools.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DllCheckExistenceCommand : BaseCommand
    {
        private Regex _regex = new Regex("\\S*");
        /// <summary>
        /// Initializes a new instance of the <see cref="DllCheckExistenceCommand"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public DllCheckExistenceCommand(ILog log)
            : base(log)
        {
        }

        #region Overrides of Command

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {

            string dllList;
            using (var form = new FormText { Text = "Enter searched DLLs" })
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                dllList = form.EditText;
            }
            if (string.IsNullOrEmpty(dllList))
            {
                AddLog("List is empty");
                return;
            }
            AddLog("Start Dll Check Existence Command");
            using (var reader = new StringReader(dllList))
            {
                string line;
                do
                {
                    line = reader.ReadLine();
                    CheckExistence(line);
                } while (line != null);

            }
            AddLog("End Dll Check Existence Command");

        }

        #endregion
        private void CheckExistence(string text)
        {
            if (string.IsNullOrEmpty(text) || text.StartsWith("\\\\") || text.StartsWith("//"))
                return;
            
            var matches = _regex.Matches(text);

            foreach (Match match in matches)
            {
                var file = match.Value;
                try
                {
                    var ptr = Kernel32.LoadLibrary(file);
                    if (ptr != IntPtr.Zero)
                    {
                        Kernel32.FreeLibrary(ptr);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    AddLog("Error load: " + file);
                    AddLog(ex.ToString());
                }
            }
            AddLog("Not found: " + text);
        }
    }
}
