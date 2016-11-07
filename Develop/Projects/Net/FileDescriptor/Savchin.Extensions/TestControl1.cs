using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Savchin.WinApi.Shell;

namespace Savchin.Extensions
{
    [Guid("99B8BCE4-5F61-4FE0-B71B-309CB762734E")]
    [ComVisible(true)]
    // [TargetExtension("*", false)]
    [TargetExtension("dll", true)]
    [RegistryKeyNameAttribute("Savchin Inc. Extensions")]
    public partial class TestControl1 : PropertySheetExtension
    {
        public TestControl1()
        {
            InitializeComponent();
        }
    }
}
