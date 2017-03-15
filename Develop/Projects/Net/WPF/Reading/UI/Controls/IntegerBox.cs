using System.Windows;
using System.Windows.Controls;
using Savchin.Core;
using Savchin.Wpf.Core;

namespace Prodigy.Controls
{
    public class IntegerBox : Canvas
    {



        public int Digit
        {
            get { return (int)GetValue(DigitProperty); }
            set { SetValue(DigitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Digit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DigitProperty =
            DependencyProperty.Register("Digit", typeof(int), typeof(IntegerBox), new UIPropertyMetadata(0, OnDigitPropertyChanged));




        public DependencyObject Object
        {
            get { return (DependencyObject)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Object.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectProperty =
            DependencyProperty.Register("Object", typeof(DependencyObject), typeof(IntegerBox), new UIPropertyMetadata(null, OnObjectPropertyChanged));

        private static void OnObjectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((IntegerBox)d).Display();
        }

        private static void OnDigitPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((IntegerBox)d).Display();
        }

        private bool IsDigitDisplay
        {
            get { return Children.Count == 1 && Children[0] is Label; }
        }

        private void Display()
        {

            var o = Object;
            if (o == null)
            {
                if (IsDigitDisplay)
                {
                    ((Label)Children[0]).Content = Digit.ToString();
                }
                else
                {
                    this.Children.Clear();
                    Children.Add(new Label
                                     {
                                         Content = Digit.ToString(),
                                         Style = (Style)Application.Current.Resources["Syllable"]
                                     });
                }

            }
            else
            {
                this.Children.Clear();
                for (int i = 0; i < Digit; i++)
                {
                    var instance = (UIElement)o.Clone();
                    Children.Add(instance);
                }
                _field = null;
            }

        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
    
            if (Children.Count > 0)
            {
                if (IsDigitDisplay)
                {
                    var label = (Label)Children[0];
                    SetLeft(label, (arrangeSize.Width ) / 3);
                    SetTop(label, (arrangeSize.Height ) / 3);
                }
                else
                    SetPositions(arrangeSize);
            }

            return base.ArrangeOverride(arrangeSize); ;
        }


        private int _columnsCount;
        private int _rowsCount;
        private bool[,] _field;
        private const int CellSize = 30;
        private void SetPositions(Size size)
        {
            InitField(size.Width, size.Height);
            foreach (var child in Children)
            {
                SetPosition((UIElement)child);
            }
            _field = null;
        }
        private void InitField(double width, double height)
        {
            _columnsCount = (int)width / CellSize;
            _rowsCount = (int)height / CellSize;

            _field = new bool[_columnsCount, _rowsCount];
        }

        private void SetPosition(UIElement el)
        {
            var column = Randomizer.GetIntegerBetween(0, _columnsCount);
            var row = Randomizer.GetIntegerBetween(0, _rowsCount);

            while (_field[column, row])
            {
                column = Randomizer.GetIntegerBetween(0, _columnsCount);
                row = Randomizer.GetIntegerBetween(0, _rowsCount);
            }

            _field[column, row] = true;
            SetLeft(el, column * CellSize);
            SetTop(el, row * CellSize);

        }
    }
}
