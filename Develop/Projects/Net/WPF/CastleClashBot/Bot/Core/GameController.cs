using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Castle.Core;
using CastleBot.Bot;
using ChristianMoser.WpfInspector.Services;
using Savchin.WinApi;

namespace Bot.Core
{
    class GameController
    {
        private IntPtr _windowHandle;
        private RECT _windowRect;
        readonly ChannelFactory<IControllerService> _channelFactory = new ChannelFactory<IControllerService>(new NetNamedPipeBinding(), ControllerService.ControllerServiceAddress);
        private IControllerService _service;
     
        private IControllerService Service
        {
            get
            {
   
                return _channelFactory.CreateChannel();
            }
        }

        public void Init(IntPtr handle)
        {
            _windowHandle = handle;
            User32.GetClientRect(_windowHandle, out _windowRect);
      
        }

        public void Click(int x, int y)
        {
            var e = new MouseEvent(MouseButtons.Left, 1, x, _windowRect.Height - y, 0);
            Service.MouseDown(e);
            Thread.Sleep(500);
            Service.MouseUp(e);
        }

        public void MouseDown(MouseEvent e)
        {

            Service.MouseDown(e);
        }

        public void MouseMove(MouseEvent e)
        {
            Service.MouseMove(e);
        }

        public void MouseUp(MouseEvent e)
        {
            Service.MouseUp(e);
        }
    }
}
