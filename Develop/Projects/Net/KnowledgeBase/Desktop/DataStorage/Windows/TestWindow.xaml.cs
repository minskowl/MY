using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KnowledgeBase.BussinesLayer.Core;

namespace KnowledgeBase.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

      
            Print("Empty", Environment.GetEnvironmentVariables());
            Print("Machine", Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine));
            Print("Process", Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process));
            Print("User", Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User));
        }

        private void Print(string title, IDictionary variables)
        {
            boxOut.AppendText( Environment.NewLine+"!!!!!!!!!!!!!! " + title + Environment.NewLine);

            foreach (var key in variables.Keys)
            {
                var value=variables[key];
                boxOut.AppendText(key + " = " + value + Environment.NewLine);
            }
        }
    }
}
