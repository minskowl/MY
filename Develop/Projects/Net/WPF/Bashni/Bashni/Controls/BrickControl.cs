using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Bashni.Game;

namespace Bashni.Controls
{
    public class BrickControl : Label
    {
        public static List<SolidColorBrush> Colors { get; set; }

        public void Init(Brick brick)
        {
            Background =Colors[brick.Color];
            Content = brick.Width == 1 ? string.Empty : (brick.Width - 1).ToString();
            Width = brick.Width  * 11 + 2;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;

            Margin = new Thickness(1);

        }
    }
}
