namespace Savchin.WinApi.Windows.FileDialogs
{
    public class SaveFileDialogWatcher : FileDialogWatcher
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFileDialogWatcher"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public SaveFileDialogWatcher(string title)
            : base(title)
        {
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void SaveFile(string file)
        {
            SetFileName(file);
        }

      
    }
}