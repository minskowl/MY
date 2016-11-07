using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Savchin.Wpf.Drawing;

namespace Savchin.Wpf.Controls.Core
{
    public class ListBoxSystemColor : ListBox
    {
        private MenuItem sortingItem;
        public enum SortingType
        {
            ByName,
            ByColor
        }
        #region Properties



        public SortingType Sorting
        {
            get { return (SortingType)GetValue(SortingProperty); }
            set { SetValue(SortingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sorting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SortingProperty =
            DependencyProperty.Register("Sorting", typeof(SortingType), typeof(ListBoxSystemColor),
            new UIPropertyMetadata(SortingType.ByName, SortingPropertyChanged));

        private static void SortingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListBoxSystemColor;
            if (control == null)
                return;
            control.FillColors();
        }


        public bool IsSelectable
        {
            get { return (bool)GetValue(IsSelectableProperty); }
            set { SetValue(IsSelectableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelectable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectableProperty =
            DependencyProperty.Register("IsSelectable", typeof(bool), typeof(ListBoxSystemColor),
            new UIPropertyMetadata(false, IsSelectablePropertyChanged));

        private static void IsSelectablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListBoxSystemColor;
            if (control == null)
                return;
            control.FillColors();
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBoxSystemColor"/> class.
        /// </summary>
        public ListBoxSystemColor()
        {
            Loaded += new RoutedEventHandler(SystemColorsControl_Loaded);
            this.ContextMenu = CreateMenu();
        }

        /// <summary>
        /// Handles the Loaded event of the SystemColorsControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void SystemColorsControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillColors();
        }

        private ContextMenu CreateMenu()
        {
            var result = new ContextMenu();
            sortingItem = new MenuItem { Header = "Sorting" };

            var sortingByNameItem = new MenuItem
                                        {
                                            Header = "By Name",
                                            IsCheckable = true,
                                            IsChecked = true
                                        };
            sortingByNameItem.Click += new RoutedEventHandler(sortingByNameItem_Click);
            sortingItem.Items.Add(sortingByNameItem);

            var sortingByColorItem = new MenuItem
                                         {
                                             Header = "By Color",
                                             IsCheckable = true
                                         };
            sortingByColorItem.Click += new RoutedEventHandler(sortingByColorItem_Click);
            sortingItem.Items.Add(sortingByColorItem);

            result.Items.Add(sortingItem);
            return result;
        }

        void sortingByColorItem_Click(object sender, RoutedEventArgs e)
        {
            Sorting = SortingType.ByColor;

        }

        void sortingByNameItem_Click(object sender, RoutedEventArgs e)
        {
            Sorting = SortingType.ByName;
        }

        private void FillColors()
        {
            if (ActualWidth == 0)
                return;

            Items.Clear();

            var isSelectable = IsSelectable;
            var width = ActualWidth - 25;
            foreach (var systemColor in GetSystemColors())
            {
                var fore = new SolidColorBrush(GetInversColor(systemColor.Brush.Color));

                if (isSelectable)
                    Items.Add(new TextBox
                                  {
                                      Text = systemColor.Name,
                                      Background = systemColor.Brush,
                                      Foreground = fore,
                                      MinHeight = 25,
                                      Width = width,
                                      VerticalAlignment = VerticalAlignment.Center,
                                      HorizontalAlignment = HorizontalAlignment.Stretch,
                                      IsReadOnly = true,
                                      BorderThickness = new Thickness(0)
                                  });
                else
                    Items.Add(new TextBlock
                    {
                        Text = systemColor.Name,
                        Background = systemColor.Brush,
                        Foreground = fore,
                        MinHeight = 25,
                        Width = width,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                    });

            }
        }

        private List<SystemBrush> GetSystemColors()
        {
            var type = typeof(SystemColors);
            var properties = type.GetProperties();
            var result = new List<SystemBrush>();
            foreach (var property in properties)
            {
                if (!property.CanRead)
                    continue;
                var value = property.GetValue(null, null);
                if (value is SolidColorBrush)
                    result.Add(new SystemBrush
                                   {
                                       Name = property.Name,
                                       Brush = (SolidColorBrush)value
                                   });
            }

            if(Sorting==SortingType.ByColor)
            {
                result.Sort(delegate(SystemBrush x,SystemBrush y)
                                {
                                    var xsum = GetColorNumber(x.Brush.Color);
                                    var ysum = GetColorNumber(y.Brush.Color);
                                    if (xsum==ysum)
                                        return 0;
                                    return xsum < ysum ? -1 : 1;
                                }
                    );
            }
            return result;
        }
        /// <summary>
        /// Gets the color number.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        private static int GetColorNumber(Color c)
        {
            return c.A + c.R + c.G + c.B;
        }

        private Color GetInversColor(Color color)
        {
            HSBColor hsbColor = ConverterColor.ToHSBColor(color);
            //return (hsbColor.B > 127) ? Colors.White : Colors.Black;
            var newBlack = (hsbColor.B > 127) ? hsbColor.B - 125 : hsbColor.B + 125;
            var newSaturation = (hsbColor.S > 127) ? hsbColor.S - 125 : hsbColor.S + 125;
            return HSBColor.FromHSB(hsbColor.A, hsbColor.H, newSaturation, newBlack);

        }
        private class SystemBrush
        {
            public SolidColorBrush Brush { get; set; }
            public String Name { get; set; }
        }
    }
}
