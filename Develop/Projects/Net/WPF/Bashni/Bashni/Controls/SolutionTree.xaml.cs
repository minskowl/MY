using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Bashni.Game;

namespace Bashni.Controls
{
    /// <summary>
    /// Interaction logic for SolutionTree.xaml
    /// </summary>
    public partial class SolutionTree
    {
        public SolutionTree()
        {
            InitializeComponent();
        }

        private void TreeItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var fe = e.OriginalSource as FrameworkElement;
            if (fe == null) return;
            var keyHolder = fe.DataContext as StepView;
            if (keyHolder == null) return;

            keyHolder.IsExpanded = !keyHolder.IsExpanded;
            e.Handled = true;
        }

    }

    public class ViewStepConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2) return Binding.DoNothing;
            var step = values[0] as Step;
            var collection = values[1] as StepsCollection;
            if (step == null || collection == null) return Binding.DoNothing;

            var result = collection.Expand(step);

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new []
                     {
                        value==null?  Binding.DoNothing:((StepView)value).Key,
                         Binding.DoNothing
                     };
        }
    }
}
