using System;
using System.IO;
using System.Net;
using Savchin.IO;

namespace Savchin.Net
{
    public class FtpClient
    {
        private const int bufferSize = 2048;

        #region Properties
        private string server;
        private string user;
        private string password;
        private string localdir;
        private string remotedir;
        private int port = 21;

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public string User
        {
            get { return user; }
            set { user = value; }
        }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private InternetSettings connectionSettings = new InternetSettings();
        /// <summary>
        /// Gets or sets the connection settings.
        /// </summary>
        /// <value>The connection settings.</value>
        public InternetSettings ConnectionSettings
        {
            get { return connectionSettings = new InternetSettings(); }
            set { connectionSettings = value; }
        }

        /// <summary>
        /// Gets or sets the localdir.
        /// </summary>
        /// <value>The localdir.</value>
        public string Localdir
        {
            get { return localdir; }
            set { localdir = value; }
        }

        /// <summary>
        /// Gets or sets the remotedir.
        /// </summary>
        /// <value>The remotedir.</value>
        public string Remotedir
        {
            get { return remotedir; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    remotedir = value;
                }
                else
                {
                    if (value.EndsWith(@"\") || value.EndsWith(@"/"))
                        remotedir = value;
                    else
                        remotedir = value + @"\";
                }

            }
        }

        #endregion

        #region Interface

        /// <summary>
        /// Downloads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Download(string fileName)
        {

            try
            {

                Uri url = GetUrl(fileName);
                Console.WriteLine("Download url: " + url);

                FtpWebRequest request = CreateRequest(url);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                var downloadResponse = request.GetResponse();

                using (var responseStream = downloadResponse.GetResponseStream())
                using (var fileStream = File.Create(localdir + fileName))
                {
                    StreamPipe.Transfer(responseStream, fileStream, bufferSize);
                }

            }
            catch (Exception ex)
            {
                throw new FtpException("Error download file", ex);
            }


        }

        /// <summary>
        /// Uploads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Upload(string fileName)
        {
        tryAgain:

            FtpWebResponse response = null;

            try
            {

                Uri url = GetUrl(fileName);
                Console.WriteLine("Upload url: " + url);

                FtpWebRequest request = CreateRequest(url);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(user, password);


                using (var requestStream = request.GetRequestStream())
                using (var fileStream = File.Open(fileName, FileMode.Open))
                {
                    StreamPipe.Transfer(fileStream, requestStream, bufferSize);
                    response = (FtpWebResponse)request.GetResponse();

                    Console.WriteLine(response.StatusDescription);
                    Console.WriteLine("Upload complete.");
                }



            }
            catch (UriFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (WebException ex)
            {
                string status = ((FtpWebResponse)ex.Response).StatusDescription;
                if (status.Contains("550"))
                {
                    if (MakeDirectorySafe())
                        goto tryAgain;
                }
                else
                {
                    PrintWebException(ex);
                }

            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
        /// <summary>
        /// Deletes the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Delete(string fileName)
        {

            FtpWebResponse response = null;
            try
            {
                Uri url = GetUrl(fileName);
                Console.WriteLine("Delete url: " + url);

                FtpWebRequest request = CreateRequest(url);
                request.Method = WebRequestMethods.Ftp.DeleteFile;


                response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Delete complete.");
            }
            catch (Exception ex)
            {
                throw new FtpException("Error download file", ex);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
        /// <summary>
        /// Makes the directory.
        /// </summary>
        public void MakeDirectory()
        {
            try
            {
                Uri url = GetUrl();
                Console.WriteLine("Make Directory url: " + url);

                FtpWebRequest request = CreateRequest(url);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.GetResponse();


            }
            catch (Exception ex)
            {
                throw new FtpException("Error download file", ex);
            }
        }
        /// <summary>
        /// Lists the specified list URL.
        /// </summary>
        public void List()
        {
            StreamReader reader = null;
            try
            {
                Uri url = GetUrl();
                Console.WriteLine("List url: " + url);


                FtpWebRequest request = CreateRequest(url);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                FtpWebResponse listResponse = (FtpWebResponse)request.GetResponse();
                reader = new StreamReader(listResponse.GetResponseStream());
                Console.WriteLine(reader.ReadToEnd());
                Console.WriteLine("List complete.");
            }
            catch (Exception ex)
            {
                throw new FtpException("Error download file", ex);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        #endregion

        private bool MakeDirectorySafe()
        {
            try
            {
                MakeDirectory();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void PrintWebException(WebException ex)
        {
            Console.WriteLine(((FtpWebResponse)ex.Response).StatusDescription + "::" +
                              ex.ToString());
        }

        private Uri GetUrl(string fileName)
        {
            return new UriBuilder("ftp", server, port, remotedir + Path.GetFileName(fileName)).Uri;
        }

        private Uri GetUrl()
        {
            return new UriBuilder("ftp", server, port, remotedir).Uri;
        }

        private FtpWebRequest CreateRequest(Uri url)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            if (connectionSettings != null)
                connectionSettings.Initialize(request);
            else
                request.Credentials = new NetworkCredential(user, password);
            return request;
        }

    }
}
