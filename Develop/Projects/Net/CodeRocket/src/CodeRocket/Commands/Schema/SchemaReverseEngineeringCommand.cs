using System.Windows.Forms;
using CodeRocket.Common;
using CodeRocket.Controls;
using Savchin.Comparer;
using Savchin.Forms.Core.Commands;
using Savchin.Data.Schema;

namespace CodeRocket.Commands.Schema
{
    class SchemaReverseEngineeringCommand : Command
    {
        private readonly Configuration _configuration= new Configuration{CompareMode=CompareMode.Marked, TrackDifference = true};
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            var destionationSchema = AppCore.Current.Form.SchemaBrowser.SelectedSchema;
            if (destionationSchema == null)
            {
                MessageBox.Show("Select schema. Please.", "Error");
                return;
            }
            var builder = new OleDbSchemaBuilder();
            var sourceSchema = builder.BuildDatabaseSchema(destionationSchema.ConnectionString);

            AppCore.Current.Form.ErrorViewer.ShowErrors(builder.Errors);

            var dummyDoc = new CompareConsole();

            dummyDoc.ShowResults(_configuration.Compare(typeof(DatabaseSchema), sourceSchema, destionationSchema) as ObjectResult);
            dummyDoc.Show(AppCore.Current.Form.DockPanel);
 
        }
    }
}
