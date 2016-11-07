using System.Windows.Forms;
using CodeRocket.Common;
using Savchin.Core;
using Savchin.Data.Schema;
using Savchin.Forms.Core.Commands;

namespace CodeRocket.Commands.Schema
{
    class SchemaSaveCommand : Command
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            DatabaseSchema schema = AppCore.Current.Form.SchemaBrowser.SelectedSchema;
            if (schema == null)
            {
                MessageBox.Show("Select schema. Please.", "Error");
                return;
            }

            schema.Save();
        }
    }
}
