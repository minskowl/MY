using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TVSeriesTracker.Models;

namespace TVSeriesTracker.Controls
{
    public class PageEx : Page
    {
        public PageEx()
        {
            Loaded += OnLoaded;
        }

        protected virtual void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var model = DataContext as ModelBase;
            if (model != null)
                model.OnLoaded();
        }
    }
}
