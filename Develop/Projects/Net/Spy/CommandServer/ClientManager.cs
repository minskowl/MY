using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CommandServer
{
    /// The class that contains some methods and properties to manage the remote clients.
    /// </summary>
    public class ClientManager
    {
        private readonly BackgroundWorker bwReceiver;
        private string clientName;
        private readonly NetworkStream networkStream;
        private readonly Socket socket;
        private readonly Semaphore semaphor = new Semaphore(1, 1);

        #region Constructor

        /// <summary>
        /// Creates an instance of ClientManager class to comunicate with remote clients.
        /// </summary>
        /// <param name="clientSocket">The socket of ClientManager.</param>
        public ClientManager(Socket clientSocket)
        {
            socket = clientSocket;
            networkStream = new NetworkStream(socket);
            bwReceiver = new BackgroundWorker();
            bwReceiver.DoWork += new DoWorkEventHandler(StartReceive);
            bwReceiver.RunWorkerAsync();
        }

        #endregion

        #region Private Methods


        private void StartReceive(object sender, DoWorkEventArgs e)
        {
            while (socket.Connected)
            {
                //Read the command's Type.
                byte[] buffer = new byte[4];
                int readBytes = networkStream.Read(buffer, 0, 4);
                if (readBytes == 0)
                    break;
                CommandType cmdType = (CommandType)(BitConverter.ToInt32(buffer, 0));

                //Read the command's target size.
                string cmdTarget = "";
                buffer = new byte[4];
                readBytes = networkStream.Read(buffer, 0, 4);
                if (readBytes == 0)
                    break;
                int ipSize = BitConverter.ToInt32(buffer, 0);

                //Read the command's target.
                buffer = new byte[ipSize];
                readBytes = networkStream.Read(buffer, 0, ipSize);
                if (readBytes == 0)
                    break;
                cmdTarget = Encoding.ASCII.GetString(buffer);

                //Read the command's MetaData size.
                string cmdMetaData = "";
                buffer = new byte[4];
                readBytes = networkStream.Read(buffer, 0, 4);
                if (readBytes == 0)
                    break;
                int metaDataSize = BitConverter.ToInt32(buffer, 0);

                //Read the command's Meta data.
                buffer = new byte[metaDataSize];
                readBytes = networkStream.Read(buffer, 0, metaDataSize);
                if (readBytes == 0)
                    break;
                cmdMetaData = Encoding.Unicode.GetString(buffer);

                Command cmd = new Command(cmdType, IPAddress.Parse(cmdTarget), cmdMetaData);
                cmd.SenderIP = IP;
                if (cmd.CommandType == CommandType.ClientLoginInform)
                    cmd.SenderName = cmd.MetaData.Split(new char[] { ':' })[1];
                else
                    cmd.SenderName = ClientName;
                OnCommandReceived(new CommandEventArgs(cmd));
            }
            OnDisconnected(new ClientEventArgs(socket));
            Disconnect();
        }

        private void bwSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null && ((bool)e.Result))
                OnCommandSent(new EventArgs());
            else
                OnCommandFailed(new EventArgs());

            ((BackgroundWorker)sender).Dispose();
            GC.Collect();
        }

        private void bwSender_DoWork(object sender, DoWorkEventArgs e)
        {
            Command cmd = (Command)e.Argument;
            e.Result = SendCommandToClient(cmd);
        }

        //This Semaphor is to protect the critical section from concurrent access of sender threads.
        private bool SendCommandToClient(Command cmd)
        {
            try
            {
                semaphor.WaitOne();
                //Type
                byte[] buffer = BitConverter.GetBytes((int)cmd.CommandType);
                networkStream.Write(buffer, 0, 4);
                networkStream.Flush();

                //Sender IP
                byte[] senderIPBuffer = Encoding.ASCII.GetBytes(cmd.SenderIP.ToString());
                buffer = BitConverter.GetBytes(senderIPBuffer.Length);
                networkStream.Write(buffer, 0, 4);
                networkStream.Flush();
                networkStream.Write(senderIPBuffer, 0, senderIPBuffer.Length);
                networkStream.Flush();

                //Sender Name
                byte[] senderNameBuffer = Encoding.Unicode.GetBytes(cmd.SenderName.ToString());
                buffer =  BitConverter.GetBytes(senderNameBuffer.Length);
                networkStream.Write(buffer, 0, 4);
                networkStream.Flush();
                networkStream.Write(senderNameBuffer, 0, senderNameBuffer.Length);
                networkStream.Flush();

                //Target
                byte[] ipBuffer = Encoding.ASCII.GetBytes(cmd.Target.ToString());
                buffer = BitConverter.GetBytes(ipBuffer.Length);
                networkStream.Write(buffer, 0, 4);
                networkStream.Flush();
                networkStream.Write(ipBuffer, 0, ipBuffer.Length);
                networkStream.Flush();

                //Meta Data.
                if (cmd.MetaData == null || cmd.MetaData == "")
                    cmd.MetaData = "\n";

                byte[] metaBuffer = Encoding.Unicode.GetBytes(cmd.MetaData);
                buffer = BitConverter.GetBytes(metaBuffer.Length);
                networkStream.Write(buffer, 0, 4);
                networkStream.Flush();
                networkStream.Write(metaBuffer, 0, metaBuffer.Length);
                networkStream.Flush();

                semaphor.Release();
                return true;
            }
            catch
            {
                semaphor.Release();
                return false;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sends a command to the remote client if the connection is alive.
        /// </summary>
        /// <param name="cmd">The command to send.</param>
        public void SendCommand(Command cmd)
        {
            if (socket != null && socket.Connected)
            {
                BackgroundWorker bwSender = new BackgroundWorker();
                bwSender.DoWork += new DoWorkEventHandler(bwSender_DoWork);
                bwSender.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwSender_RunWorkerCompleted);
                bwSender.RunWorkerAsync(cmd);
            }
            else
                OnCommandFailed(new EventArgs());
        }


        /// <summary>
        /// Disconnect the current client manager from the remote client and returns true if the client had been disconnected from the server.
        /// </summary>
        /// <returns>True if the remote client had been disconnected from the server,otherwise false.</returns>
        public bool Disconnect()
        {
            if (socket != null && socket.Connected)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return true;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a command received from a remote client.
        /// </summary>
        public event CommandReceivedEventHandler CommandReceived;

        /// <summary>
        /// Occurs when a command received from a remote client.
        /// </summary>
        /// <param name="e">Received command.</param>
        protected virtual void OnCommandReceived(CommandEventArgs e)
        {
            if (CommandReceived != null)
                CommandReceived(this, e);
        }

        /// <summary>
        /// Occurs when a command had been sent to the remote client successfully.
        /// </summary>
        public event CommandSentEventHandler CommandSent;

        /// <summary>
        /// Occurs when a command had been sent to the remote client successfully.
        /// </summary>
        /// <param name="e">The sent command.</param>
        protected virtual void OnCommandSent(EventArgs e)
        {
            if (CommandSent != null)
                CommandSent(this, e);
        }

        /// <summary>
        /// Occurs when a command sending action had been failed.This is because disconnection or sending exception.
        /// </summary>
        public event CommandSendingFailedEventHandler CommandFailed;

        /// <summary>
        /// Occurs when a command sending action had been failed.This is because disconnection or sending exception.
        /// </summary>
        /// <param name="e">The sent command.</param>
        protected virtual void OnCommandFailed(EventArgs e)
        {
            if (CommandFailed != null)
                CommandFailed(this, e);
        }

        /// <summary>
        /// Occurs when a client disconnected from this server.
        /// </summary>
        public event DisconnectedEventHandler Disconnected;

        /// <summary>
        /// Occurs when a client disconnected from this server.
        /// </summary>
        /// <param name="e">Client information.</param>
        protected virtual void OnDisconnected(ClientEventArgs e)
        {
            if (Disconnected != null)
                Disconnected(this, e);
        }

        #endregion

        /// <summary>
        /// Gets the IP address of connected remote client.This is 'IPAddress.None' if the client is not connected.
        /// </summary>
        public IPAddress IP
        {
            get
            {
                if (socket != null)
                    return ((IPEndPoint)socket.RemoteEndPoint).Address;
                else
                    return IPAddress.None;
            }
        }

        /// <summary>
        /// Gets the port number of connected remote client.This is -1 if the client is not connected.
        /// </summary>
        public int Port
        {
            get
            {
                if (socket != null)
                    return ((IPEndPoint)socket.RemoteEndPoint).Port;
                else
                    return -1;
            }
        }

        /// <summary>
        /// [Gets] The value that specifies the remote client is connected to this server or not.
        /// </summary>
        public bool Connected
        {
            get
            {
                if (socket != null)
                    return socket.Connected;
                else
                    return false;
            }
        }

        /// <summary>
        /// The name of remote client.
        /// </summary>
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }
    }
}
