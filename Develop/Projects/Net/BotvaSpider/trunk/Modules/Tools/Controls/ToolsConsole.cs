using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Core;
using BotvaSpider.Tools.Commands;
using BotvaSpider.Tools.Core;
using Savchin.Forms.Core.Commands;

namespace BotvaSpider.Consoles
{
    public partial class ToolsConsole : ControllerConsole, IObjectViewer
    {
        public ToolsConsole()
        {
            InitializeComponent();

            clearStaffListToolStripMenuItem.BindCommand(new ClearStaffListCommand(this,this));
            staffListAddToolStripMenuItem.BindCommand(new AddStaffListCommand(this));
        }


        public void Clear()
        {
         
        }

        public void Display(object obj)
        {
   
        }

        public void ShowStatus(string status)
        {
            if (labelStatus.InvokeRequired)
            {
                labelStatus.Invoke((TextDelegate)ShowStatus, new[] { status });
                return;
            }
            labelStatus.Text = status;
            Application.DoEvents();
        }
    }
}
