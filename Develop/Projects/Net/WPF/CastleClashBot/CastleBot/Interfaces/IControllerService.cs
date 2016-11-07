using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Castle.Core;
using ChristianMoser.WpfInspector.Services;

namespace CastleBot.Bot
{
    [ServiceContract]
    public interface IControllerService
    {
        [OperationContract]
        void MouseUp(MouseEvent e);

        [OperationContract]
        void MouseDown(MouseEvent e);

        [OperationContract]
        void MouseMove(MouseEvent e);

    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ControllerService : IControllerService
    {
        public const string ControllerServiceAddress = "net.pipe://localhost/ControllerService";
        private readonly IGameController _controller;

        public ControllerService(IGameController controller)
        {
            _controller = controller;
        }

        public void MouseUp(MouseEvent e)
        {
            _controller.HandleMouseUp(e);
        }

        public void MouseDown(MouseEvent e)
        {
            _controller.HandleMouseDown(e);
        }

        public void MouseMove(MouseEvent e)
        {
            _controller.HandleMouseMove(e);
        }
    }
}
