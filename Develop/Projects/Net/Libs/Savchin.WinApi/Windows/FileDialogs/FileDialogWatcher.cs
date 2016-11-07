namespace Savchin.WinApi.Windows.FileDialogs
{
    public class FileDialogWatcher
    {
        protected string name;
        private string fileName;
        /// <summary>
        /// Initializes a new instance of the <see cref="FileDialogWatcher"/> class.
        /// </summary>
        public FileDialogWatcher()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDialogWatcher"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public FileDialogWatcher(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Sets the name of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        protected void SetFileName(string fileName)
        {
            this.fileName = fileName;
            WindowWatcher.Instance.AddHandler(Handle);

        }



        private bool Handle(Window window)
        {
            if (!window.IsDialog )
                return false;
            if(name != null && name != window.Name)
                return false;

            FileDialog dialog = new FileDialog(window.Handle);
            dialog.SetFileName(fileName);
            WindowWatcher.Instance.RemoveHandler(Handle);
            return true;
        }
    }
}