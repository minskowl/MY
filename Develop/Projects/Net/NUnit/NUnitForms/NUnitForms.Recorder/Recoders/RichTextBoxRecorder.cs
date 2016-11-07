
using System;
using System.Collections;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.Recorder.Recoders
{
    public class RichTextBoxRecorder : ControlRecorder
    {
        private Hashtable table = new Hashtable();

        public RichTextBoxRecorder(Listener listener)
            : base(listener)
        {
        }

        public override Type RecorderType
        {
            get { return typeof (RichTextBox); }
        }

        public override Type TesterType
        {
            get { return typeof (RichTextBoxTester); }
        }

        public void TextChanged(object sender, EventArgs e)
        {
            if (HasFocus(sender))
            {
                Listener.FireEvent(TesterType, sender, new EventAction("Enter", ((RichTextBox) sender).Text));
            }
        }

        private bool HasFocus(object sender)
        {
            return (true.Equals(table[sender]));
        }

        public void Enter(object sender, EventArgs e)
        {
            table[sender] = true;
        }

        public void Leave(object sender, EventArgs e)
        {
            table[sender] = false;
        }
    }
}