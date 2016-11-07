using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;



namespace Savchin.Net
{
    /// <summary>
    /// FtpSocketClient
    /// </summary>
    public class FtpSocketClient
    {


        #region Properties

        private static readonly Encoding ASCII = Encoding.ASCII;
        private static int BUFFER_SIZE = 512;
        private readonly Byte[] buffer = new Byte[BUFFER_SIZE];
        private bool binMode=false;
        private int bytes;
        private Socket clientSocket;
        private bool loggedin;

        private string message;
        private string password = "anonymous@anonymous.net";


        private string remotePath = ".";
        private string result;
        private int resultCode;





        private bool verboseDebugging;
        /// <summary>
        /// Display all communications to the debug log
        /// </summary>
        public bool VerboseDebugging
        {
            get { return verboseDebugging; }
            set { verboseDebugging = value; }
        }

        private int port = 21;
        /// <summary>
        /// Remote server port. Typically TCP 21
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private int timeoutSeconds = 10;
        /// <summary>
        /// Timeout waiting for a response from server, in seconds.
        /// </summary>
        public int Timeout
        {
            get { return timeoutSeconds; }
            set { timeoutSeconds = value; }
        }

        private string server = "localhost";
        /// <summary>
        /// Gets and Sets the name of the FTP server.
        /// </summary>
        /// <returns></returns>
        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        /// <summary>
        /// Gets and Sets the port number.
        /// </summary>
        /// <returns></returns>
        public int RemotePort
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// GetS and Sets the remote directory.
        /// </summary>
        public string RemotePath
        {
            get { return remotePath; }
            set { remotePath = value; }
        }

        private string username = "anonymous";
        /// <summary>
        /// Gets and Sets the username.
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// Gets and Set the password.
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// If the value of mode is true, set binary mode for downloads, else, Ascii mode.
        /// </summary>
        public bool BinaryMode
        {
            get { return binMode; }
            set
            {
                if (binMode == value) return;

                if (value)
                    sendCommand("TYPE I");

                else
                    sendCommand("TYPE A");

                if (resultCode != 200) throw new FtpException(result.Substring(4));
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default contructor
        /// </summary>
        public FtpSocketClient()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public FtpSocketClient(string server, string username, string password)
        {
            this.server = server;
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="port"></param>
        public FtpSocketClient(string server, string username, string password, int timeoutSeconds, int port)
        {
            this.server = server;
            this.username = username;
            this.password = password;
            this.timeoutSeconds = timeoutSeconds;
            this.port = port;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Login to the remote server.
        /// </summary>
        public void Login()
        {
            if (loggedin) Close();

            Debug.WriteLine("Opening connection to " + server, "FtpSocketClient");

            IPAddress addr = null;
            IPEndPoint ep = null;

            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                addr = Dns.GetHostEntry(server).AddressList[0];
                ep = new IPEndPoint(addr, port);
                clientSocket.Connect(ep);
            }
            catch (Exception ex)
            {
                // doubtfull
                if (clientSocket != null && clientSocket.Connected) clientSocket.Close();

                throw new FtpException("Couldn't connect to remote server", ex);
            }

            readResponse();

            if (resultCode != 220)
            {
                Close();
                throw new FtpException(result.Substring(4));
            }

            sendCommand("USER " + username);

            if (!(resultCode == 331 || resultCode == 230))
            {
                cleanup();
                throw new FtpException(result.Substring(4));
            }

            if (resultCode != 230)
            {
                sendCommand("PASS " + password);

                if (!(resultCode == 230 || resultCode == 202))
                {
                    cleanup();
                    throw new FtpException(result.Substring(4));
                }
            }

            loggedin = true;

            Debug.WriteLine("Connected to " + server, "FtpSocketClient");

            ChangeDir(remotePath);
        }

        /// <summary>
        /// Close the FTP connection.
        /// </summary>
        public void Close()
        {
            Debug.WriteLine("Closing connection to " + server, "FtpSocketClient");

            if (clientSocket != null)
            {
                sendCommand("QUIT");
            }

            cleanup();
        }

        /// <summary>
        /// Return a string array containing the remote directory's file list.
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList()
        {
            return GetFileList("*.*");
        }

        /// <summary>
        /// Return a string array containing the remote directory's file list.
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public string[] GetFileList(string mask)
        {
            if (!loggedin) Login();

            Socket cSocket = createDataSocket();

            sendCommand("NLST " + mask);

            if (!(resultCode == 150 || resultCode == 125)) throw new FtpException(result.Substring(4));

            message = "";

            DateTime timeout = DateTime.Now.AddSeconds(timeoutSeconds);

            while (timeout > DateTime.Now)
            {
                int bytes = cSocket.Receive(buffer, buffer.Length, 0);
                message += ASCII.GetString(buffer, 0, bytes);

                if (bytes < buffer.Length) break;
            }

            string[] msg = message.Replace("\r", "").Split('\n');

            cSocket.Close();

            if (message.IndexOf("No such file or directory") != -1)
                msg = new string[] { };

            readResponse();

            if (resultCode != 226)
                msg = new string[] { };
            //	throw new FtpException(result.Substring(4));

            return msg;
        }

        /// <summary>
        /// Return the size of a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public long GetFileSize(string fileName)
        {
            if (!loggedin) Login();

            sendCommand("SIZE " + fileName);

            if (resultCode == 213)
                return long.Parse(result.Substring(4));

            throw new FtpException(result.Substring(4));
        }


        /// <summary>
        /// Download a file to the Assembly's local directory,
        /// keeping the same file name.
        /// </summary>
        /// <param name="remFileName"></param>
        public void Download(string remFileName)
        {
            Download(remFileName, "", false);
        }

        /// <summary>
        /// Download a remote file to the Assembly's local directory,
        /// keeping the same file name, and set the resume flag.
        /// </summary>
        /// <param name="remFileName"></param>
        /// <param name="resume"></param>
        public void Download(string remFileName, Boolean resume)
        {
            Download(remFileName, "", resume);
        }

        /// <summary>
        /// Download a remote file to a local file name which can include
        /// a path. The local file name will be created or overwritten,
        /// but the path must exist.
        /// </summary>
        /// <param name="remFileName"></param>
        /// <param name="locFileName"></param>
        public void Download(string remFileName, string locFileName)
        {
            Download(remFileName, locFileName, false);
        }

        /// <summary>
        /// Download a remote file to a local file name which can include
        /// a path, and set the resume flag. The local file name will be
        /// created or overwritten, but the path must exist.
        /// </summary>
        /// <param name="remFileName"></param>
        /// <param name="locFileName"></param>
        /// <param name="resume"></param>
        public void Download(string remFileName, string locFileName, Boolean resume)
        {
            if (!loggedin) Login();

            BinaryMode = true;

            Debug.WriteLine("Downloading file " + remFileName + " from " + server + "/" + remotePath, "FtpSocketClient");

            if (locFileName.Equals(""))
            {
                locFileName = remFileName;
            }

            FileStream output = null;

            if (!File.Exists(locFileName))
                output = File.Create(locFileName);

            else
                output = new FileStream(locFileName, FileMode.Open);

            Socket cSocket = createDataSocket();

            long offset = 0;

            if (resume)
            {
                offset = output.Length;

                if (offset > 0)
                {
                    sendCommand("REST " + offset);
                    if (resultCode != 350)
                    {
                        //Server dosnt support resuming
                        offset = 0;
                        Debug.WriteLine("Resuming not supported:" + result.Substring(4), "FtpSocketClient");
                    }
                    else
                    {
                        Debug.WriteLine("Resuming at offset " + offset, "FtpSocketClient");
                        output.Seek(offset, SeekOrigin.Begin);
                    }
                }
            }

            sendCommand("RETR " + remFileName);

            if (resultCode != 150 && resultCode != 125)
            {
                throw new FtpException(result.Substring(4));
            }

            DateTime timeout = DateTime.Now.AddSeconds(timeoutSeconds);

            while (timeout > DateTime.Now)
            {
                bytes = cSocket.Receive(buffer, buffer.Length, 0);
                output.Write(buffer, 0, bytes);

                if (bytes <= 0)
                {
                    break;
                }
            }

            output.Close();

            if (cSocket.Connected) cSocket.Close();

            readResponse();

            if (resultCode != 226 && resultCode != 250)
                throw new FtpException(result.Substring(4));
        }


        /// <summary>
        /// Upload a file.
        /// </summary>
        /// <param name="fileName"></param>
        public void Upload(string fileName)
        {
            Upload(fileName, false);
        }


        /// <summary>
        /// Upload a file and set the resume flag.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="resume"></param>
        public void Upload(string fileName, bool resume)
        {
            if (!loggedin) Login();

            Socket cSocket = null;
            long offset = 0;

            if (resume)
            {
                try
                {
                    BinaryMode = true;

                    offset = GetFileSize(Path.GetFileName(fileName));
                }
                catch (Exception)
                {
                    // file not exist
                    offset = 0;
                }
            }

            // open stream to read file
            var input = new FileStream(fileName, FileMode.Open);

            if (resume && input.Length < offset)
            {
                // different file size
                Debug.WriteLine("Overwriting " + fileName, "FtpSocketClient");
                offset = 0;
            }
            else if (resume && input.Length == offset)
            {
                // file done
                input.Close();
                Debug.WriteLine("Skipping completed " + fileName + " - turn resume off to not detect.", "FtpSocketClient");
                return;
            }

            // dont create untill we know that we need it
            cSocket = createDataSocket();

            if (offset > 0)
            {
                sendCommand("REST " + offset);
                if (resultCode != 350)
                {
                    Debug.WriteLine("Resuming not supported", "FtpSocketClient");
                    offset = 0;
                }
            }

            sendCommand("STOR " + Path.GetFileName(fileName));

            if (resultCode != 125 && resultCode != 150) throw new FtpException(result.Substring(4));

            if (offset != 0)
            {
                Debug.WriteLine("Resuming at offset " + offset, "FtpSocketClient");

                input.Seek(offset, SeekOrigin.Begin);
            }

            Debug.WriteLine("Uploading file " + fileName + " to " + remotePath, "FtpSocketClient");

            while ((bytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                cSocket.Send(buffer, bytes, 0);
            }

            input.Close();

            if (cSocket.Connected)
            {
                cSocket.Close();
            }

            readResponse();

            if (resultCode != 226 && resultCode != 250) throw new FtpException(result.Substring(4));
        }

        /// <summary>
        /// Upload a directory and its file contents
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recurse">Whether to recurse sub directories</param>
        public void UploadDirectory(string path, bool recurse)
        {
            UploadDirectory(path, recurse, "*.*");
        }

        /// <summary>
        /// Upload a directory and its file contents
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recurse">Whether to recurse sub directories</param>
        /// <param name="mask">Only upload files of the given mask - everything is '*.*'</param>
        public void UploadDirectory(string path, bool recurse, string mask)
        {
            string[] dirs = path.Replace("/", @"\").Split('\\');
            string rootDir = dirs[dirs.Length - 1];

            // make the root dir if it doed not exist
            if (GetFileList(rootDir).Length < 1) MakeDir(rootDir);

            ChangeDir(rootDir);

            foreach (string file in Directory.GetFiles(path, mask))
            {
                Upload(file, true);
            }
            if (recurse)
            {
                foreach (string directory in Directory.GetDirectories(path))
                {
                    UploadDirectory(directory, recurse, mask);
                }
            }

            ChangeDir("..");
        }

        /// <summary>
        /// Delete a file from the remote FTP server.
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteFile(string fileName)
        {
            if (!loggedin) Login();

            sendCommand("DELE " + fileName);

            if (resultCode != 250) throw new FtpException(result.Substring(4));

            Debug.WriteLine("Deleted file " + fileName, "FtpSocketClient");
        }

        /// <summary>
        /// Rename a file on the remote FTP server.
        /// </summary>
        /// <param name="oldFileName"></param>
        /// <param name="newFileName"></param>
        /// <param name="overwrite">setting to false will throw exception if it exists</param>
        public void RenameFile(string oldFileName, string newFileName, bool overwrite)
        {
            if (!loggedin) Login();

            sendCommand("RNFR " + oldFileName);

            if (resultCode != 350) throw new FtpException(result.Substring(4));

            if (!overwrite && GetFileList(newFileName).Length > 0) throw new FtpException("File already exists");

            sendCommand("RNTO " + newFileName);

            if (resultCode != 250) throw new FtpException(result.Substring(4));

            Debug.WriteLine("Renamed file " + oldFileName + " to " + newFileName, "FtpSocketClient");
        }

        /// <summary>
        /// Create a directory on the remote FTP server.
        /// </summary>
        /// <param name="dirName"></param>
        public void MakeDir(string dirName)
        {
            if (!loggedin) Login();

            sendCommand("MKD " + dirName);

            if (resultCode != 250 && resultCode != 257) throw new FtpException(result.Substring(4));

            Debug.WriteLine("Created directory " + dirName, "FtpSocketClient");
        }

        /// <summary>
        /// Delete a directory on the remote FTP server.
        /// </summary>
        /// <param name="dirName"></param>
        public void RemoveDir(string dirName)
        {
            if (!loggedin) Login();

            sendCommand("RMD " + dirName);

            if (resultCode != 250) throw new FtpException(result.Substring(4));

            Debug.WriteLine("Removed directory " + dirName, "FtpSocketClient");
        }

        /// <summary>
        /// Change the current working directory on the remote FTP server.
        /// </summary>
        /// <param name="dirName"></param>
        public void ChangeDir(string dirName)
        {
            if (dirName == null || dirName.Equals(".") || dirName.Length == 0)
            {
                return;
            }

            if (!loggedin) Login();

            sendCommand("CWD " + dirName);

            if (resultCode != 250) throw new FtpException(result.Substring(4));

            sendCommand("PWD");

            if (resultCode != 257) throw new FtpException(result.Substring(4));

            // gonna have to do better than this....
            remotePath = message.Split('"')[1];

            Debug.WriteLine("Current directory is " + remotePath, "FtpSocketClient");
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void readResponse()
        {
            message = "";
            result = readLine();

            if (result.Length > 3)
                resultCode = int.Parse(result.Substring(0, 3));
            else
                result = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string readLine()
        {
            while (true)
            {
                bytes = clientSocket.Receive(buffer, buffer.Length, 0);
                message += ASCII.GetString(buffer, 0, bytes);

                if (bytes < buffer.Length)
                {
                    break;
                }
            }

            string[] msg = message.Split('\n');

            if (message.Length > 2)
                message = msg[msg.Length - 2];

            else
                message = msg[0];


            if (message.Length > 4 && !message.Substring(3, 1).Equals(" ")) return readLine();

            if (verboseDebugging)
            {
                for (int i = 0; i < msg.Length - 1; i++)
                {
                    Debug.Write(msg[i], "FtpSocketClient");
                }
            }

            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        private void sendCommand(String command)
        {
            if (verboseDebugging) Debug.WriteLine(command, "FtpSocketClient");

            Byte[] cmdBytes = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
            clientSocket.Send(cmdBytes, cmdBytes.Length, 0);
            readResponse();
        }

        /// <summary>
        /// when doing data transfers, we need to open another socket for it.
        /// </summary>
        /// <returns>Connected socket</returns>
        private Socket createDataSocket()
        {
            sendCommand("PASV");

            if (resultCode != 227) throw new FtpException(result.Substring(4));

            int index1 = result.IndexOf('(');
            int index2 = result.IndexOf(')');

            string ipData = result.Substring(index1 + 1, index2 - index1 - 1);

            var parts = new int[6];

            int len = ipData.Length;
            int partCount = 0;
            string buf = "";

            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = char.Parse(ipData.Substring(i, 1));

                if (char.IsDigit(ch))
                    buf += ch;

                else if (ch != ',')
                    throw new FtpException("Malformed PASV result: " + result);

                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = int.Parse(buf);
                        buf = "";
                    }
                    catch (Exception ex)
                    {
                        throw new FtpException("Malformed PASV result (not supported?): " + result, ex);
                    }
                }
            }

            string ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];

            int port = (parts[4] << 8) + parts[5];

            Socket socket = null;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var ep = new IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[0], port);
                socket.Connect(ep);
            }
            catch (Exception ex)
            {
                // doubtfull....
                if (socket != null && socket.Connected) socket.Close();

                throw new FtpException("Can't connect to remote server", ex);
            }

            return socket;
        }

        /// <summary>
        /// Always release those sockets.
        /// </summary>
        private void cleanup()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
            }
            loggedin = false;
        }

        /// <summary>
        /// Destuctor
        /// </summary>
        ~FtpSocketClient()
        {
            cleanup();
        }

        #region Async methods (auto generated)


        private delegate void ChangeDirCallback(String dirName);
        private delegate void CloseCallback();
        private delegate void DeleteFileCallback(String fileName);
        private delegate void DownloadCallback(String remFileName);
        private delegate void DownloadFileNameFileNameCallback(String remFileName, String locFileName);
        private delegate void DownloadFileNameFileNameResumeCallback(String remFileName, String locFileName, Boolean resume);
        private delegate void DownloadFileNameResumeCallback(String remFileName, Boolean resume);
        private delegate String[] GetFileListCallback();
        private delegate String[] GetFileListMaskCallback(String mask);
        private delegate Int64 GetFileSizeCallback(String fileName);
        private delegate void LoginCallback();
        private delegate void MakeDirCallback(String dirName);
        private delegate void RemoveDirCallback(String dirName);
        private delegate void RenameFileCallback(String oldFileName, String newFileName, Boolean overwrite);
        private delegate void UploadCallback(String fileName);
        private delegate void UploadDirectoryCallback(String path, Boolean recurse);
        private delegate void UploadDirectoryPathRecurseMaskCallback(String path, Boolean recurse, String mask);
        private delegate void UploadFileNameResumeCallback(String fileName, Boolean resume);


        /// <summary>
        /// Begins the login.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginLogin(AsyncCallback callback)
        {
            LoginCallback ftpCallback = Login;
            return ftpCallback.BeginInvoke(callback, null);
        }

        /// <summary>
        /// Begins the close.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginClose(AsyncCallback callback)
        {
            CloseCallback ftpCallback = Close;
            return ftpCallback.BeginInvoke(callback, null);
        }

        /// <summary>
        /// Begins the get file list.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginGetFileList(AsyncCallback callback)
        {
            GetFileListCallback ftpCallback = GetFileList;
            return ftpCallback.BeginInvoke(callback, null);
        }

        /// <summary>
        /// Begins the get file list.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginGetFileList(String mask, AsyncCallback callback)
        {
            GetFileListMaskCallback ftpCallback = GetFileList;
            return ftpCallback.BeginInvoke(mask, callback, null);
        }

        /// <summary>
        /// Begins the size of the get file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginGetFileSize(String fileName, AsyncCallback callback)
        {
            GetFileSizeCallback ftpCallback = GetFileSize;
            return ftpCallback.BeginInvoke(fileName, callback, null);
        }

        public IAsyncResult BeginDownload(String remFileName, AsyncCallback callback)
        {
            DownloadCallback ftpCallback = Download;
            return ftpCallback.BeginInvoke(remFileName, callback, null);
        }

        public IAsyncResult BeginDownload(String remFileName, Boolean resume, AsyncCallback callback)
        {
            DownloadFileNameResumeCallback ftpCallback = Download;
            return ftpCallback.BeginInvoke(remFileName, resume, callback, null);
        }

        public IAsyncResult BeginDownload(String remFileName, String locFileName, AsyncCallback callback)
        {
            DownloadFileNameFileNameCallback ftpCallback = Download;
            return ftpCallback.BeginInvoke(remFileName, locFileName, callback, null);
        }

        public IAsyncResult BeginDownload(String remFileName, String locFileName, Boolean resume, AsyncCallback callback)
        {
            DownloadFileNameFileNameResumeCallback ftpCallback = Download;
            return ftpCallback.BeginInvoke(remFileName, locFileName, resume, callback, null);
        }

        public IAsyncResult BeginUpload(String fileName, AsyncCallback callback)
        {
            UploadCallback ftpCallback = Upload;
            return ftpCallback.BeginInvoke(fileName, callback, null);
        }

        public IAsyncResult BeginUpload(String fileName, Boolean resume, AsyncCallback callback)
        {
            UploadFileNameResumeCallback ftpCallback = Upload;
            return ftpCallback.BeginInvoke(fileName, resume, callback, null);
        }

        public IAsyncResult BeginUploadDirectory(String path, Boolean recurse, AsyncCallback callback)
        {
            UploadDirectoryCallback ftpCallback = UploadDirectory;
            return ftpCallback.BeginInvoke(path, recurse, callback, null);
        }

        public IAsyncResult BeginUploadDirectory(String path, Boolean recurse, String mask, AsyncCallback callback)
        {
            UploadDirectoryPathRecurseMaskCallback ftpCallback = UploadDirectory;
            return ftpCallback.BeginInvoke(path, recurse, mask, callback, null);
        }

        public IAsyncResult BeginDeleteFile(String fileName, AsyncCallback callback)
        {
            DeleteFileCallback ftpCallback = DeleteFile;
            return ftpCallback.BeginInvoke(fileName, callback, null);
        }

        public IAsyncResult BeginRenameFile(String oldFileName, String newFileName, Boolean overwrite,
                                            AsyncCallback callback)
        {
            RenameFileCallback ftpCallback = RenameFile;
            return ftpCallback.BeginInvoke(oldFileName, newFileName, overwrite, callback, null);
        }

        /// <summary>
        /// Begins the make dir.
        /// </summary>
        /// <param name="dirName">Name of the dir.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginMakeDir(String dirName, AsyncCallback callback)
        {
            MakeDirCallback ftpCallback = MakeDir;
            return ftpCallback.BeginInvoke(dirName, callback, null);
        }

        /// <summary>
        /// Begins the remove dir.
        /// </summary>
        /// <param name="dirName">Name of the dir.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginRemoveDir(String dirName, AsyncCallback callback)
        {
            RemoveDirCallback ftpCallback = RemoveDir;
            return ftpCallback.BeginInvoke(dirName, callback, null);
        }

        /// <summary>
        /// Begins the change dir.
        /// </summary>
        /// <param name="dirName">Name of the dir.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public IAsyncResult BeginChangeDir(String dirName, AsyncCallback callback)
        {
            ChangeDirCallback ftpCallback = ChangeDir;
            return ftpCallback.BeginInvoke(dirName, callback, null);
        }



        #endregion
    }
}