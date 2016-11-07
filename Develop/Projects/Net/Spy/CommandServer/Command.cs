using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CommandServer
{
    /// <summary>
    /// The command class.
    /// </summary>
    public class Command
    {
        private CommandType cmdType;
        /// <summary>
        /// The type of command to send.If you wanna use the Message command type,create a Message class instead of command.
        /// </summary>
        public CommandType CommandType
        {
            get { return cmdType; }
            set { cmdType = value; }
        }

        private IPAddress senderIP;
        /// <summary>
        /// [Gets/Sets] The IP address of command sender.
        /// </summary>
        public IPAddress SenderIP
        {
            get { return senderIP; }
            set { senderIP = value; }
        }

        private string senderName;
        /// <summary>
        /// [Gets/Sets] The name of command sender.
        /// </summary>
        public string SenderName
        {
            get { return senderName; }
            set { senderName = value; }
        }

        private IPAddress target;
        /// <summary>
        /// [Gets/Sets] The targer machine that will receive the command.Set this property to IPAddress.Broadcast if you want send the command to all connected clients.
        /// </summary>
        public IPAddress Target
        {
            get { return target; }
            set { target = value; }
        }
        private string commandBody;
        /// <summary>
        /// The body of the command.This string is different in various commands.
        /// <para>Message : The text of the message.</para>
        /// <para>ClientLoginInform,SendClientList : "RemoteClientIP:RemoteClientName".</para>
        /// <para>***WithTimer : The interval of timer in miliseconds..The default value is 60000 equal to 1 min.</para>
        /// <para>IsNameExists : 'True' or 'False'</para>
        /// <para>Otherwise pass the "" or null.</para>
        /// </summary>
        public string MetaData
        {
            get { return commandBody; }
            set { commandBody = value; }
        }
        /// <summary>
        /// Creates an instance of command object to send over the network.
        /// </summary>
        /// <param name="type">The type of command.If you wanna use the Message command type,create a Message class instead of command.</param>
        /// <param name="targetMachine">The targer machine that will receive the command.Set this property to IPAddress.Broadcast if you want send the command to all connected clients.</param>
        /// <param name="metaData">
        /// The body of the command.This string is different in various commands.
        /// <para>Message : The text of the message.</para>
        /// <para>ClientLoginInform,SendClientList : "RemoteClientIP:RemoteClientName".</para>
        /// <para>***WithTimer : The interval of timer in miliseconds..The default value is 60000 equal to 1 min.</para>
        /// <para>IsNameExists : 'True' or 'False'</para>
        /// <para>Otherwise pass the "" or null or use the next overriden constructor.</para>
        /// </param>
        public Command(CommandType type, IPAddress targetMachine, string metaData)
        {
            this.cmdType = type;
            this.target = targetMachine;
            this.commandBody = metaData;
        }

        /// <summary>
        /// Creates an instance of command object to send over the network.
        /// </summary>
        /// <param name="type">The type of command.If you wanna use the Message command type,create a Message class instead of command.</param>
        /// <param name="targetMachine">The targer machine that will receive the command.Set this property to IPAddress.Broadcast if you want send the command to all connected clients.</param>
        public Command(CommandType type, IPAddress targetMachine)
        {
            this.cmdType = type;
            this.target = targetMachine;
            this.commandBody = "";
        }
    }
}
