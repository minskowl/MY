using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace NAnt.Savchin.Tasks.FTP
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
            Stream responseStream = null;
            FileStream fileStream = null;
            StreamReader reader = null;
            try
            {

                Uri url = GetUrl(fileName);
                Console.WriteLine("Download url: " + url);

                FtpWebRequest request = CreateRequest(url);
                request.Method = WebRequestMethods.Ftp.DownloadFile;


                FtpWebResponse downloadResponse = (FtpWebResponse)request.GetResponse();
                responseStream = downloadResponse.GetResponseStream();


                fileStream = File.Create(localdir + fileName);
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                while (true)
                {

                    bytesRead = responseStream.Read(buffer, 0, bufferSize);
                    if (bytesRead == 0)
                        break;
                    fileStream.Write(buffer, 0, bytesRead);
                }




            }
            catch (UriFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (WebException ex)
            {
                PrintWebException(ex);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (responseStream != null)
                    responseStream.Close();
                if (fileStream != null)
                    fileStream.Close();
            }


        }

        /// <summary>
        /// Uploads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Upload(string fileName)
        {
        tryAgain:
            Stream requestStream = null;
            FileStream fileStream = null;
            FtpWebResponse response = null;

            try
            {

                Uri url = GetUrl(fileName);
                Console.WriteLine("Upload url: " + url);

                FtpWebRequest request = CreateRequest(url);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(user, password);


                requestStream = request.GetRequestStream();
                fileStream = File.Open(fileName, FileMode.Open);

                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                while (true)
                {
                    bytesRead = fileStream.Read(buffer, 0, bufferSize);
                    if (bytesRead == 0)
                        break;
                    requestStream.Write(buffer, 0, bytesRead);
                }

                // The request stream must be closed before getting 
                // the response.
                requestStream.Close();

                response = (FtpWebResponse)request.GetResponse();

                Console.WriteLine(response.StatusDescription);
                Console.WriteLine("Upload complete.");
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
                    if (MakeDirectory())
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
                if (fileStream != null)
                    fileStream.Close();
                if (requestStream != null)
                    requestStream.Close();
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
                PrintWebException(ex);
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
        public bool MakeDirectory()
        {
            try
            {
                Uri url = GetUrl();
                Console.WriteLine("Make Directory url: " + url);

                FtpWebRequest request = CreateRequest(url);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.GetResponse();
                return true;

            }
            catch (UriFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
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
            catch (UriFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        } 
        #endregion

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
            request.Credentials = new NetworkCredential(user, password);
            return request;
        }

    }
}
