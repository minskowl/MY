using System;
using System.Windows.Forms;
using Savchin.Controls.Common;
using Savchin.Core;
using SchemaEditor.Core;

namespace SchemaEditor.Commands.File
{
    public class OpenSchemaCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter)
        {
            try
            {
                if (AppCore.OpenFileDialog.ShowDialog(AppCore.FormMain) != DialogResult.OK)
                {
                    return;
                }

                AppCore.FormMain.SchemaBrowser.ShowSchema(AppCore.OpenFileDialog.FileName);
            }
            catch (Exception ex)
            {
                ExceptionForm.ShowException("Open file", "Error open file", ex);
            }
        }
    }
}
