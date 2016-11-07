using System.Reflection;
using System.Windows.Forms;
using BlueStacks.hyperDroid.Frontend;
using Castle.Core;
using CastleBot.Bot;
using Console = BlueStacks.hyperDroid.Frontend.Console;

namespace CastleController.Bot
{
    public class GameController : IGameController
    {

        private readonly ILog _log;
        private readonly Console _mainForm;
        private readonly MethodInfo _handleMouseDown;
        private readonly MethodInfo _handleMouseUp;
        private readonly MethodInfo _handleMouseMove;
        private readonly OpenSensor _openSensor;
        public GameController(Console mainForm, ILog log)
        {
            _mainForm = mainForm;
            _log = log;
            var type = _mainForm.GetType();
            //var sensorField=type.GetField("mOpenSensor", BindingFlags.Instance | BindingFlags.NonPublic);
            //Log(_handleMouseDown.Name);
            //_openSensor = sensorField.GetValue(_mainForm) as OpenSensor;
            //Log(_openSensor.SetControlHandler());

            _handleMouseDown = type.GetMethod("HandleMouseDown", BindingFlags.Instance | BindingFlags.NonPublic);
            //AddLog(_handleMouseDown.Name);
            _handleMouseUp = type.GetMethod("HandleMouseUp", BindingFlags.Instance | BindingFlags.NonPublic);
            //AddLog(_handleMouseUp.Name);
            _handleMouseMove = type.GetMethod("HandleMouseMove", BindingFlags.Instance | BindingFlags.NonPublic);
            //AddLog(_handleMouseMove.Name);
        }

        private void Log(string s)
        {
            _log.AddLog(s);
        }
        public void HandleMouseUp(MouseEvent evt)
        {
            Log("Mouse Up " + evt);
            _handleMouseUp.Invoke(_mainForm, new object[] { _mainForm, evt.ToArgs() });
        }

        public void HandleMouseDown(MouseEvent evt)
        {
            Log("Mouse Down " + evt);
            _handleMouseDown.Invoke(_mainForm, new object[] { _mainForm, evt.ToArgs() });
        }
        public void HandleMouseMove(MouseEvent evt)
        {
            Log("Mouse Move " + evt);
            _handleMouseMove.Invoke(_mainForm, new object[] { _mainForm, evt.ToArgs() });
        }
    }
}
