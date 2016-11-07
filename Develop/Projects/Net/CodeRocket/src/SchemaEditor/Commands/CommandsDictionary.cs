using System.Collections.Generic;
using Savchin.Core;
using SchemaEditor.Commands.Edit;
using SchemaEditor.Commands.File;

namespace SchemaEditor.Commands
{
    internal class CommandsDictionary : Dictionary<string, ICommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsDictionary"/> class.
        /// </summary>
        public CommandsDictionary()
            : base()
        {
            Add("New", new NewSchemaCommand());
            Add("Open", new OpenSchemaCommand());
            Add("Close", new CloseSchemaCommand());
            Add("Save", new SaveSchemaCommand());
            Add("SaveAs", new SaveAsSchemaCommand());
            Add("Exit", new ExitCommand());

            Add("Reverse Engeniring", new ReverseEngeniringCommand());

 
        }
    }
}
