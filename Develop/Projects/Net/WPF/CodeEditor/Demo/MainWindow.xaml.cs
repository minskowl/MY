using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CSharpBinding;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop.Project;
using Savchin.CodeEditor.CSharpBinding;
using Savchin.CodeEditor.Core;
using Savchin.CodeEditor.GUI;
using Savchin.CodeEditor.Services.Parse;
using Savchin.CodeEditor.Services.Project;
using Savchin.CodeEditor.Services.Project.Items;
using Savchin.Project;
using Savchin.Utils;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {


        public MainWindow()
        {

            try
            {
                WorkbenchSingleton.InitializeWorkbench(this);  
         
                InitializeComponent();

                editor.Text = Properties.Resources.Test;

                //ProjectService.LoadSolution(@"C:\Users\admin\Documents\SharpDevelop Projects\test\test.sln");
                //var s = ProjectService.OpenSolution;

            
            }
            catch (Exception ex)
            {

            }
        }



    }
}
