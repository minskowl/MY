using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Savchin.Forms.RichText.Editor.Commands
{
    /// <summary>
    /// NewFileCommand
    /// </summary>
   public  class NewFileCommand : EditorCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewFileCommand"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public NewFileCommand(EditorControl control) : base(control)
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
            try
            {
                if (rtbDoc.Modified)
                {
                    var answer = MessageBox.Show("Save current document before creating new document?",
                        "Unsaved Document",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        rtbDoc.SaveCommand.Execute(target);
                    }
           
                }

                rtbDoc.CurrentFileName = null;
             
                rtbDoc.Modified = false;
                rtbDoc.Clear();
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        #endregion
    }
}
