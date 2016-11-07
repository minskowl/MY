using System;
using System.Windows.Forms;
using Savchin.Controls.Common;
using Savchin.Core;
using Savchin.Data.Schema;
using SchemaEditor.Core;

namespace SchemaEditor.Commands.File
{
    class SaveAsSchemaCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter)
        {
            DatabaseSchema schema = AppCore.FormMain.SchemaBrowser.SelectedSchema;
            if (schema == null)
            {
                return;
            }

            if (AppCore.SaveFileDialog.ShowDialog(AppCore.FormMain) != DialogResult.OK)
            {
                return;
            }
            try
            {
                schema.Save(AppCore.SaveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                ExceptionForm.ShowException("Open file", "Error open file", ex);
            }
        }
    }
}
