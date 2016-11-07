using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using CommandServer;
using CommandType = CommandServer.CommandType;

namespace Client
{
    public partial class Form1 : Form
    {
        private CommandClient client;
        private IPAddress address;
        public Form1()
        {
            InitializeComponent();
            address = IPAddress.Parse("127.0.0.1");
            client = new CommandClient(address, 8000, "Hi");
            client.ConnectToServer();
            client.CommandSent += new CommandSentEventHandler(client_CommandSent);
            client.CommandReceived += new CommandReceivedEventHandler(client_CommandReceived);
            client.SendCommand(new Command(CommandType.FreeCommand, IPAddress.Broadcast, client.IP + ":" + client.NetworkName));
            client.SendCommand(new Command(CommandType.SendClientList, client.ServerIP));


        }

        void client_CommandSent(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.SendCommand(new Command(CommandType.Message, address, "sndhsdnjh"));
        }

        private void client_CommandReceived(object sender, CommandEventArgs e)
        {
            //switch (e.Command.CommandType)
            //{
            //    case (CommandType.Message):
            //        if (e.Command.Target.Equals(IPAddress.Broadcast))
            //            txtMessages.Text += e.Command.SenderName + ": " + e.Command.MetaData + Environment.NewLine;
            //        else if (!IsPrivateWindowOpened(e.Command.SenderName))
            //        {
            //            OpenPrivateWindow(e.Command.SenderIP, e.Command.SenderName, e.Command.MetaData);
            //            ShareUtils.PlaySound(ShareUtils.SoundType.NewMessageWithPow);
            //        }
            //        break;

            //    case (CommandType.FreeCommand):
            //        string[] newInfo = e.Command.MetaData.Split(new char[] { ':' });
            //        AddToList(newInfo[0], newInfo[1]);
            //        ShareUtils.PlaySound(ShareUtils.SoundType.NewClientEntered);
            //        break;
            //    case (CommandType.SendClientList):
            //        string[] clientInfo = e.Command.MetaData.Split(new char[] { ':' });
            //        AddToList(clientInfo[0], clientInfo[1]);
            //        break;
            //    case (CommandType.ClientLogOffInform):
            //        RemoveFromList(e.Command.SenderName);
            //        break;
            //}
        }
    }
}