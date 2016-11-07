using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Playback.Core
{
    class Settings
    {
        public static readonly Dictionary<int, SolidColorBrush> AvailableColors = new Dictionary<int, SolidColorBrush> 
                                 {
                                     {0, new SolidColorBrush(Colors.Silver)},
                                      {1, new SolidColorBrush(Colors.Red)},
                                     {2, new SolidColorBrush(Colors.Green)},
                                     {3, new SolidColorBrush(Colors.Blue)},
                                     {4, new SolidColorBrush(Colors.Yellow)},
                                     {5, new SolidColorBrush(Colors.Violet)},
                                 };
    }
}
