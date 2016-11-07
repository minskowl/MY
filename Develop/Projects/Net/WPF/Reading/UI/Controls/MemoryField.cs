using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Reading.Core;
using Savchin.Collection.Generic;
using Savchin.Wpf.Imaging;

namespace Reading.Controls
{
    public class MemoryField : Grid
    {
        private DispatcherTimer _timer = new DispatcherTimer();


        public MemoryField()
        {
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += new EventHandler(_timer_Tick);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            ValidateOpened();
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

            base.OnPropertyChanged(e);

            if (e.Property == DataContextProperty)
            {
                var field = DataContext as int?[,];
                RowDefinitions.Clear();
                ColumnDefinitions.Clear();
                Children.Clear();

                if (field != null)
                    BuildField(field);
            }
        }

        private void BuildField(int?[,] field)
        {
            var rows = field.GetLength(0);
            var columns = field.GetLength(1);
            CreateGrid(columns, rows);

            var card = ResourceProvider.GetPath("card.png").ToImageSource();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    var num = field[i, j].Value;
                    var url = ResourceProvider.GetAnimalsFile(num.ToString());
                    var image = new MemoryCard
                                    {
                                        Source = card,
                                        FrontImage = url.ToImageSource(),
                                        OpositeImage = card,
                                        Number = num
                                    };
                    image.MouseLeftButtonDown += image_MouseLeftButtonDown;
                    SetRow(image, i);
                    SetColumn(image, j);
                    Children.Add(image);
                }
        }

        void image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var opened = GetOpened();
            if (opened.Length > 1) return;

            var card = (MemoryCard)sender;
            card.IsOpen = !card.IsOpen;

            _timer.Start();
        }

        private void CreateGrid(int columns, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < columns; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }
            ColumnDefinitions.Add(new ColumnDefinition());
        }
        private void ValidateOpened()
        {
            var opened = GetOpened();
            if (opened.Length > 1)
            {
                if (opened[0].Number == opened[1].Number)
                {


                    opened[0].MouseLeftButtonDown -= image_MouseLeftButtonDown;
                    opened[1].MouseLeftButtonDown -= image_MouseLeftButtonDown;
                    opened[0].Visibility = Visibility.Hidden;
                    opened[1].Visibility = Visibility.Hidden;
                }
                else
                {
                    opened.Foreach(e => e.IsOpen = false);
                }
            }
        }

        private MemoryCard[] GetOpened()
        {
            return Children.OfType<MemoryCard>().Where(c => c.IsOpen && c.Visibility == Visibility.Visible).ToArray();
        }
    }

    public class MemoryCard : Image
    {
        public ImageSource FrontImage { get; set; }
        public ImageSource OpositeImage { get; set; }
        public int Number { get; set; }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(MemoryCard), new UIPropertyMetadata(false, OnIsOpenChanged));

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (MemoryCard)d;
            c.Source = c.IsOpen ? c.FrontImage : c.OpositeImage;
        }





    }
}
