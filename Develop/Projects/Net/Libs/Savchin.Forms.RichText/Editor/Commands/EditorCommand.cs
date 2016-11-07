using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savchin.Forms.Core.Commands;

namespace Savchin.Forms.RichText.Editor.Commands
{
    public abstract class EditorCommand : Command
    {
        protected EditorControl rtbDoc;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorCommand"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        protected EditorCommand(EditorControl control)
        {
            rtbDoc = control;
        }


    }
}
