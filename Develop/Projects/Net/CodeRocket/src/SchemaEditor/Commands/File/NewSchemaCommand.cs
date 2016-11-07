using System;
using System.Windows.Forms;
using Savchin.Controls.Common;
using Savchin.Core;
using Savchin.Data.Schema;
using SchemaEditor.Core;

namespace SchemaEditor.Commands.File
{
    class NewSchemaCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter)
        {

            using (var f = new FormConnectionString())
            {
                if (f.ShowDialog(AppCore.FormMain) != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    var builder = DatabaseSchema.CreateBuilder(f.ProviderType);
                    var schema = builder.BuildDatabaseSchema(f.ConnectionString);

                    AppCore.FormMain.SchemaBrowser.ShowSchema(schema);
                }
                catch (Exception ex)
                {
                    ExceptionForm.ShowException("Open file", "Error open file", ex);
                }
            }

        }
    }
}
