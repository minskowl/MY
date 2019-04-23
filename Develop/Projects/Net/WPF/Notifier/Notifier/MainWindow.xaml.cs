using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Atlassian.Jira;

namespace Notifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

            var jira = Jira.CreateRestClient("https://jira.effective-soft.com", "ES\\dmitry.savchin", "ыфмф!11111");
            jira.Debug = true;
            // use LINQ syntax to retrieve issues
            var issues = from i in jira.Issues.Queryable
                where i.Assignee == "admin" && i.Priority == "Major"
                orderby i.Created
                select i;

            var result=issues.ToArray();


        }
    }
}
