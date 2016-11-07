using Savchin.Core;
using SchemaEditor.Core;

namespace SchemaEditor.Commands.File
{
    class CloseSchemaCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter)
        {
            AppCore.FormMain.SchemaBrowser.CloseAllSchemas();
        }
    }
}
