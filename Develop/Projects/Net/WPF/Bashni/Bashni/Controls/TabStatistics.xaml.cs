using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bashni.Game;
using Bashni.Views;

namespace Bashni.Controls
{
    /// <summary>
    /// Interaction logic for TabStatistics.xaml
    /// </summary>
    public partial class TabStatistics
    {
        public TabStatistics()
        {
            InitializeComponent();

        }

        public void Show(Step root)
        {
            ((CollectionViewSource) Resources["cvs"]).Source = root.Steps.ToArray();
        }
    }
  
}

