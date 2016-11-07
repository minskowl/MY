using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using CastleBot.Bot;
using CastleController.Bot;
using Console = BlueStacks.hyperDroid.Frontend.Console;

namespace CastleBot
{
    public partial class ControllerForm : Form, ILog
    {

        private GameController _controller;
        private ServiceHost _serviceHost;
        public ControllerForm()
        {
            InitializeComponent();
            //BlueStacks.hyperDroid.Common.Logger.InitLog(AddLog);

            AddLog("Injected");


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                _controller = new GameController(Application.OpenForms.OfType<Console>().FirstOrDefault(), this);
                _serviceHost = new ServiceHost(new ControllerService(_controller));
                _serviceHost.AddServiceEndpoint(typeof(IControllerService), new NetNamedPipeBinding(), ControllerService.ControllerServiceAddress);
                _serviceHost.Open();
                AddLog("Service started");
            }
            catch (Exception ex)
            {
                AddLog(ex.ToString());
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            _serviceHost.Close();
            AddLog("Service stoped");
        }


        public void AddLog(string text)
        {
            textBox1.AppendText(Environment.NewLine + text);
        }


    }
}
