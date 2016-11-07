using System.Collections.Generic;
using CodeRocket.Commands.Edit;
using CodeRocket.Commands.Project;
using Savchin.Forms.Core.Commands;


namespace CodeRocket.Commands
{
    public class CommandsDictionary : Dictionary<string, ICommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsDictionary"/> class.
        /// </summary>
        public CommandsDictionary() : base()
        {
            Add("ProjectOpen", new ProjectOpenCommad());
            Add("ProjectSave", new ProjectSaveCommand());
            Add("FileSave", new File.SaveCommand());

            Add("ProjectSaveAs", new ProjectSaveAsCommand());
            Add("ProjectClose", new ProjectCloseCommand());

            Add("Select All", new SelectAllCommand());
            Add("Copy", new CopyCommand());
            Add("Paste", new PasteCommand());

            Add("GeneratgeToOutput", new Generate.GenerateToOutputCommand());
            Add("GenerateToSolution", new Generate.GenerateToSolutionCommand());
            Add("GenerateToSolutionDir", new Generate.GenerateToSolutionDirCommand());

            Add("CloseAllWindows", new CloseAllWindowsCommand());
            Add("HelpAbout", new AboutCommand());
        }
    }
}
