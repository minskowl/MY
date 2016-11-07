using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RunnerBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Trace("OnMouseDown", e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
      
            Trace("OnMouseUp",e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Trace("OnMouseMove", e);
        }

      
        private void Trace(string ev,MouseEventArgs e)
        {
            Debug.WriteLine(string.Format("{5} {6} {0} C:{1} X:{2} Y{3} D: {4}", 
                e.Button, e.Clicks, e.X, e.Y, e.Delta, DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"),ev));
        }
    }
}
