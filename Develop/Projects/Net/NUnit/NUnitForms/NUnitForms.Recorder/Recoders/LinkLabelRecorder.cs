

using System;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.Recorder
{
    public class LinkLabelRecorder : ControlRecorder
    {
        public LinkLabelRecorder(Listener listener) : base(listener)
        {
        }

        public override Type RecorderType
        {
            get { return typeof (LinkLabel); }
        }

        public override Type TesterType
        {
            get { return typeof (LinkLabelTester); }
        }

        public void LinkClicked(object sender, LinkLabelLinkClickedEventArgs args)
        {
            Listener.FireEvent(TesterType, sender, "Click");
        }
    }
}