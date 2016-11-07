using System.IO;
using System.Windows.Forms;
using Savchin.Comparer;
using Savchin.Controls.Common.Comparer;
using Savchin.Core;
using Savchin.Data.Schema;
using SchemaEditor.Controls;
using SchemaEditor.Core;

namespace SchemaEditor.Commands.Edit
{
    class ReverseEngeniringCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter)
        {
            var browser = AppCore.FormMain.SchemaBrowser;

            DatabaseSchema destination = browser.SelectedSchema;

            if (destination == null)
            {
                if (browser.Schemas.Count == 0)
                    return;

                destination = browser.Schemas[0];
            }

            var builder = DatabaseSchema.CreateBuilder(destination.ProviderType);
            DatabaseSchema source = builder.BuildDatabaseSchema(destination.ConnectionString);

            AppCore.FormMain.ExceptionViewer.ShowExceptions(builder.Errors);


            var form = new CompareConsole();
            form.View.ShowCompareResult(ObjectResult.Compare(source, destination));
            form.Show(AppCore.FormMain.DockPanel);


        }
    }
}
