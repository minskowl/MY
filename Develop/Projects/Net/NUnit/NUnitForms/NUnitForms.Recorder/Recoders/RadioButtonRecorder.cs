
using System;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.Recorder.Recoders
{
    public class RadioButtonRecorder : ButtonRecorder
    {
        public RadioButtonRecorder(Listener listener) : base(listener)
        {
        }

        public override Type RecorderType
        {
            get { return typeof (RadioButton); }
        }

        public override Type TesterType
        {
            get { return typeof (RadioButtonTester); }
        }
    }
}