using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using CommandServer;

namespace Server
{
    public partial class Form1 : Form
    {
        private BackgroundWorker bwListener;
        private List<ClientManager> clients;
        private Socket listenerSocket;
        private IPAddress serverIP;
        private int serverPort;

        public Form1()
        {
            InitializeComponent();

            clients = new List<ClientManager>();

            serverPort = 8000;
            serverIP = IPAddress.Any;
            // progDomain.serverIP = IPAddress.Parse(args[0]);


            bwListener = new BackgroundWorker();
            bwListener.WorkerSupportsCancellation = true;
            bwListener.DoWork += new DoWorkEventHandler(StartToListen);
            bwListener.RunWorkerAsync();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DisconnectServer();
            Application.Exit();
        }


        private void StartToListen(object sender, DoWorkEventArgs e)
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(new IPEndPoint(serverIP, serverPort));
            listenerSocket.Listen(200);
            while (true)
                CreateNewClientManager(listenerSocket.Accept());
        }

        private void CreateNewClientManager(Socket socket)
        {
            ClientManager newClientManager = new ClientManager(socket);
            newClientManager.CommandReceived += new CommandReceivedEventHandler(CommandReceived);
            newClientManager.Disconnected += new DisconnectedEventHandler(ClientDisconnected);
            CheckForAbnormalDC(newClientManager);
            clients.Add(newClientManager);
            UpdateConsole("Connected.", newClientManager.IP, newClientManager.Port);
        }

        private void CheckForAbnormalDC(ClientManager mngr)
        {
            if (RemoveClientManager(mngr.IP))
                UpdateConsole("Disconnected.", mngr.IP, mngr.Port);
        }

        private void ClientDisconnected(object sender, ClientEventArgs e)
        {
            if (RemoveClientManager(e.IP))
                UpdateConsole("Disconnected.", e.IP, e.Port);
        }

        private bool RemoveClientManager(IPAddress ip)
        {
            lock (this)
            {
                int index = IndexOfClient(ip);
                if (index != -1)
                {
                    string name = clients[index].ClientName;
                    clients.RemoveAt(index);

                    //Inform all clients that a client had been disconnected.
                    Command cmd = new Command(CommandType.ClientLogOffInform, IPAddress.Broadcast);
                    cmd.SenderName = name;
                    cmd.SenderIP = ip;
                    BroadCastCommand(cmd);
                    return true;
                }
                return false;
            }
        }

        private int IndexOfClient(IPAddress ip)
        {
            int index = -1;
            foreach (ClientManager cMngr in clients)
            {
                index++;
                if (cMngr.IP.Equals(ip))
                    return index;
            }
            return -1;
        }

        private void CommandReceived(object sender, CommandEventArgs e)
        {
            //When a client connects to the server sends a 'ClientLoginInform' command with a MetaData in this format :
            //"RemoteClientIP:RemoteClientName". With this information we should set the name of ClientManager and then
            //Send the command to all other remote clients.
            if (e.Command.CommandType == CommandType.ClientLoginInform)
                SetManagerName(e.Command.SenderIP, e.Command.MetaData);

            //If the client asked for existance of a name,answer to it's question.
            if (e.Command.CommandType == CommandType.IsNameExists)
            {
                bool isExixsts = IsNameExists(e.Command.SenderIP, e.Command.MetaData);
                SendExistanceCommand(e.Command.SenderIP, isExixsts);
                return;
            }
                //If the client asked for list of a logged in clients,replay to it's request.
            else if (e.Command.CommandType == CommandType.SendClientList)
            {
                SendClientListToNewClient(e.Command.SenderIP);
                return;
            }

            if (e.Command.Target.Equals(IPAddress.Broadcast))
                BroadCastCommand(e.Command);
            else
                SendCommandToTarget(e.Command);
        }

        private void SendExistanceCommand(IPAddress targetIP, bool isExists)
        {
            Command cmdExistance = new Command(CommandType.IsNameExists, targetIP, isExists.ToString());
            cmdExistance.SenderIP = serverIP;
            cmdExistance.SenderName = "Server";
            SendCommandToTarget(cmdExistance);
        }

        private void SendClientListToNewClient(IPAddress newClientIP)
        {
            foreach (ClientManager mngr in clients)
            {
                if (mngr.Connected && !mngr.IP.Equals(newClientIP))
                {
                    Command cmd = new Command(CommandType.SendClientList, newClientIP);
                    cmd.MetaData = mngr.IP.ToString() + ":" + mngr.ClientName;
                    cmd.SenderIP = serverIP;
                    cmd.SenderName = "Server";
                    SendCommandToTarget(cmd);
                }
            }
        }

        private string SetManagerName(IPAddress remoteClientIP, string nameString)
        {
            int index = IndexOfClient(remoteClientIP);
            if (index != -1)
            {
                string name = nameString.Split(new char[] {':'})[1];
                clients[index].ClientName = name;
                return name;
            }
            return "";
        }

        private bool IsNameExists(IPAddress remoteClientIP, string nameToFind)
        {
            foreach (ClientManager mngr in clients)
                if (mngr.ClientName == nameToFind && !mngr.IP.Equals(remoteClientIP))
                    return true;
            return false;
        }

        private void BroadCastCommand(Command cmd)
        {
            foreach (ClientManager mngr in clients)
                if (!mngr.IP.Equals(cmd.SenderIP))
                    mngr.SendCommand(cmd);
        }

        private void SendCommandToTarget(Command cmd)
        {
            foreach (ClientManager mngr in clients)
                if (mngr.IP.Equals(cmd.Target))
                {
                    mngr.SendCommand(cmd);
                    return;
                }
        }

        private void UpdateConsole(string status, IPAddress IP, int port)
        {
            Console.WriteLine("Client {0}{1}{2} has been {3} ( {4}|{5} )", IP.ToString(), ":", port.ToString(), status,
                              DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
        }

        public void DisconnectServer()
        {
            if (clients != null)
            {
                foreach (ClientManager mngr in clients)
                    mngr.Disconnect();

                bwListener.CancelAsync();
                bwListener.Dispose();
                listenerSocket.Close();
                GC.Collect();
            }
        }
    }
}