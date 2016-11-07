

using System;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.Recorder.Recoders
{
    /// <summary>
    /// A <see cref="ControlRecorder"/> class for <see cref="Form"/>s.
    /// </summary>
    public class FormRecorder : ControlRecorder
    {
        /// <summary>
        /// Constructes a new <see cref="FormRecorder"/> with the given listener.
        /// </summary>
        public FormRecorder(Listener listener) : base(listener)
        {
        }

        /// <summary>
        /// The type of control being recorded, <see cref="Form"/>.
        /// </summary>
        public override Type RecorderType
        {
            get { return typeof (Form); }
        }

        /// <summary>
        /// The tester type for this recorder, <see cref="FormTester"/>.
        /// </summary>
        public override Type TesterType
        {
            get { return typeof (FormTester); }
        }

        /// <summary>
        /// Fires the "Close" event for a form.
        /// </summary>
        public void Closed(object sender, EventArgs args)
        {
            Listener.FireEvent(TesterType, sender, "Close");
        }
    }
}