using Savchin.Core;
using Savchin.Data.Schema;
using SchemaEditor.Core;

namespace SchemaEditor.Commands.File
{
    class SaveSchemaCommand : Command
    {

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter)
        {
            DatabaseSchema schema = AppCore.FormMain.SchemaBrowser.SelectedSchema;
            if (schema != null)
            {
                schema.Save();
            }
            else
            {
                foreach (DatabaseSchema databaseSchema in AppCore.FormMain.SchemaBrowser.Schemas)
                {
                    databaseSchema.Save();
                }
            }
        }
    }
}
