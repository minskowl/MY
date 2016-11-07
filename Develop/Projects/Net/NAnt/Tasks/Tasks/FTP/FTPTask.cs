using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;


namespace NAnt.Savchin.Tasks.FTP
{
    [TaskName("ftp")]
    public class FTPTask : Task
    {
        FtpClient ftpClient = new FtpClient();
        #region Property
        private string server;
        private string user;
        private string password;
        private string mode;
        private string localdir;
        private string remotedir;
        private FileSet files;
        private GetFiles getFiles;
        private int port = 21;

        /// <summary>
        /// The port number to connect to.  Default is 21.
        /// </summary>
        [TaskAttribute("port", Required = false)]
        [Int32Validator(1, 65535)]
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        [TaskAttribute("server")]
        [StringValidator(AllowEmpty = false)]
        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        [TaskAttribute("mode")]
        [StringValidator(AllowEmpty = false)]
        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        /// <summary>
        /// Gets or sets the localdir.
        /// </summary>
        /// <value>The localdir.</value>
        [TaskAttribute("localdir")]
        [StringValidator(AllowEmpty = false)]
        public string Localdir
        {
            get { return localdir; }
            set { localdir = value; }
        }
        /// <summary>
        /// Gets or sets the remotedir.
        /// </summary>
        /// <value>The remotedir.</value>
        [TaskAttribute("remotedir")]
        [StringValidator(AllowEmpty = false)]
        public string Remotedir
        {
            get { return remotedir; }
            set { remotedir = value.Replace('/', '\\').Trim('\\'); }
        }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        [TaskAttribute("user")]
        [StringValidator(AllowEmpty = true)]
        public string User
        {
            get { return user; }
            set { user = value; }
        }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [TaskAttribute("password")]
        [StringValidator(AllowEmpty = true)]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        [BuildElement("fileset")]
        public FileSet Files
        {
            get { return files; }
            set { files = value; }
        }


        /// <summary>
        /// Gets or sets the get files.
        /// </summary>
        /// <value>The get files.</value>
        [BuildElement("getfiles")]
        public GetFiles GetFiles
        {
            set { getFiles = value; }
            get { return getFiles; }
        }
        #endregion

        protected override void ExecuteTask()
        {
            LogInfoMessage();

            ftpClient.Server = server;
            ftpClient.User = user;
            ftpClient.Password = password;
            ftpClient.Port = port;
            ftpClient.Localdir = localdir;
            ftpClient.Remotedir = remotedir;
            switch (Mode.ToLower())
            {
                case "get":
                    LoadFiles();
                    break;
                case "put":
                    UploadFiles();
                    break;
                case "delete":
                    DeleteFiles();
                    break;
                case "list":
                    ftpClient.List();
                    break;
                default:
                    Log(Level.Error, "Incorrect mode: " + Mode);
                    break;
            }


        }
        private void UploadFiles()
        {
            if (files == null)
                return;

            Log(Level.Verbose, "remotedir: " + remotedir);
            foreach (string filename in files.FileNames)
            {
                Log(Level.Info, "processing file: " + filename);
                string relativePath = Path.GetDirectoryName(filename).Substring(files.BaseDirectory.FullName.Length);
                ftpClient.Remotedir = remotedir + relativePath;
                Log(Level.Verbose, "ftpClient.Remotedir: " + ftpClient.Remotedir);
                ftpClient.Upload(filename);
            }
        }
        private void DeleteFiles()
        {
            if (files == null)
                return;
            foreach (string filename in files.FileNames)
            {
                Log(Level.Info, "processing file: " + filename);
                string relativePath = Path.GetDirectoryName(filename).Substring(files.BaseDirectory.FullName.Length);
                ftpClient.Remotedir = remotedir + relativePath;
                ftpClient.Delete(filename);
            }
        }
        private void LoadFiles()
        {
            if (getFiles == null)
                return;
            foreach (GetFile filename in getFiles.Files)
            {
                Log(Level.Info, "processing file: " + filename.FileName);
                ftpClient.Download(filename.FileName);
            }
        }

        private void LogInfoMessage()
        {
            StringBuilder result = new StringBuilder();
            int fileCount = 0;
            if (files != null)
                fileCount = files.FileNames.Count;
            else if (getFiles != null)
                fileCount = getFiles.Files.Length;
            result.AppendFormat("FtpTask v {0} processing {1} file(s) for  {2} on server {3}",
                                Assembly.GetExecutingAssembly().GetName().Version,
                                fileCount,
                                mode,
                                server);
            Log(Level.Info, result.ToString());
        }
    }
}
