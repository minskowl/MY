using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Bashni.Game;

namespace Bashni.Controls
{
    public class FieldControl : Grid
    {
        public Step Step
        {
            get { return (Step)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(Step), typeof(FieldControl), new UIPropertyMetadata(null, OnStepChanged));

        private static void OnStepChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (FieldControl)d;
            c.Draw((Step)e.NewValue);

        }


        public void Draw(Step step)
        {
            ColumnDefinitions.Clear();
            RowDefinitions.Clear();
            Children.Clear();

            if (step == null) return;

            var field = step.Field;


            BuildGrid(field);

            for (int rowIndex = 0; rowIndex < field.Rows; rowIndex++)
            {
                AddTextBlock(rowIndex, 0, rowIndex);
                AddTextBlock(rowIndex, field.Columns + 1, rowIndex);

                for (int columnIndex = 0; columnIndex < field.Columns; columnIndex++)
                {
                    var brick = field[rowIndex, columnIndex];
                    if (brick != null)
                    {
                        var control = new BrickControl();

                        control.Init(brick);
                        SetColumn(control, columnIndex + 1);
                        SetRow(control, rowIndex);
                        Children.Add(control);
                    }
                }
            }
            var ground = new BrickControl();
            ground.Background = Brushes.Brown;
            SetColumn(ground, 0);
            SetColumnSpan(ground, field.Columns + 2);
            SetRow(ground, field.Rows);
            Children.Add(ground);

            for (int columnIndex = 0; columnIndex < field.Columns; columnIndex++)
            {
                AddTextBlock(field.Rows + 1, columnIndex + 1, columnIndex);
            }

        }
        private void AddTextBlock(int row, int column, object text)
        {
            var l = new TextBlock
                        {
                            Text = text.ToString(),
                            HorizontalAlignment = HorizontalAlignment.Center
                        };

            SetColumn(l, column);
            SetRow(l, row);
            Children.Add(l);
        }

        private void BuildGrid(Field field)
        {
            for (int rowIndex = 0; rowIndex < field.Rows; rowIndex++)
            {
                this.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25) });
            }
            this.RowDefinitions.Add(new RowDefinition());

            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
            for (int columnIndex = 0; columnIndex < field.Columns; columnIndex++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }

            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
        }
    }
}
