
using System;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.Recorder.Recoders
{
    public class MenuItemRecorder : ControlRecorder
    {
        public MenuItemRecorder(Listener listener) : base(listener)
        {
        }

        public override Type RecorderType
        {
            get { return typeof (MenuItem); }
        }

        public override Type TesterType
        {
            get { return typeof (MenuItemTester); }
        }

        public void Click(object sender, EventArgs args)
        {
            Listener.FireEvent(TesterType, sender, "Click");
        }
    }
}