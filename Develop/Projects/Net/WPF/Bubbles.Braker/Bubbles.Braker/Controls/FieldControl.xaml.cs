using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Bubbles.Braker;
using Savchin.Bubbles.Core;

namespace Savchin.Bubbles.Controls
{



    /// <summary>
    /// Interaction logic for Field.xaml
    /// </summary>
    public partial class FieldControl : UserControl, INotifyPropertyChanged
    {
        private const int bubleSize = 22;
        private const int margin = 0;


        private readonly StrategiesCollection strategies = new StrategiesCollection();
        private IShiftStrategy[] shiftStrategy;
        private BubbleField bubbles;

        #region Properties
        /// <summary>
        /// Gets the bubbles.
        /// </summary>
        /// <value>The bubbles.</value>
        public BubbleField Bubbles
        {
            get { return bubbles; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance can redo.
        /// </summary>
        /// <value><c>true</c> if this instance can redo; otherwise, <c>false</c>.</value>
        public bool CanRedo
        {
            get { return bubbles.CanRedo; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can undo.
        /// </summary>
        /// <value><c>true</c> if this instance can undo; otherwise, <c>false</c>.</value>
        public bool CanUndo
        {
            get { return bubbles.CanUndo; }
        }

        #region ActiveStrategy
        /// <summary>
        /// Gets or sets the active strategy.
        /// </summary>
        /// <value>The active strategy.</value>
        public ShiftStrategy ActiveStrategy
        {
            get { return (ShiftStrategy)GetValue(ActiveStrategyProperty); }
            set { SetValue(ActiveStrategyProperty, value); }
        }

        public static readonly DependencyProperty ActiveStrategyProperty =
           DependencyProperty.Register("ActiveStrategy", typeof(ShiftStrategy), typeof(FieldControl),
           new UIPropertyMetadata(ShiftStrategy.Standart, OnActiveStrategyPropertyChanged));

        private static void OnActiveStrategyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d as FieldControl;
            if (b == null) return;

            var strategy = (ShiftStrategy)e.NewValue;
            App.Current.Settings.Strategy = strategy;
            b.shiftStrategy = b.strategies[strategy];
            b.NewGame();
        }
        #endregion

        #region Score
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>The score.</value>
        public int Score
        {
            get { return (int)GetValue(ScoreProperty); }
        }

        public static readonly DependencyProperty ScoreProperty =
            DependencyProperty.Register("Score", typeof(int), typeof(FieldControl), new UIPropertyMetadata(0, OnScoreChanged));

        private static void OnScoreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d as FieldControl;
            if (b == null) return;

            b.OnPropertyChanged(new PropertyChangedEventArgs("Score"));
        }
        #endregion

        #region SelectedScore
        public static readonly DependencyProperty SelectedScoreProperty =
    DependencyProperty.Register("SelectedScore", typeof(int), typeof(FieldControl), new UIPropertyMetadata(0, OnSelectedScoreChanged));
        /// <summary>
        /// Gets or sets the selected score.
        /// </summary>
        /// <value>The selected score.</value>
        public int SelectedScore
        {
            get { return (int)GetValue(SelectedScoreProperty); }
        }


        private static void OnSelectedScoreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d as FieldControl;
            if (b == null) return;

            b.OnPropertyChanged(new PropertyChangedEventArgs("SelectedScore"));
        }
        #endregion

        #region Size

        public static readonly DependencyProperty SizeProperty =
    DependencyProperty.Register("Size", typeof(int), typeof(FieldControl), new UIPropertyMetadata(0, OnSizePropertyChanged));
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fieldControl = d as FieldControl;
            if (fieldControl == null) return;
            var size = (int)e.NewValue;
            var unitSize = size * bubleSize + margin;

            fieldControl.layout.Width = unitSize;
            fieldControl.layout.Height = unitSize;
            fieldControl.MinHeight = unitSize;
            fieldControl.MinWidth = unitSize;
            fieldControl.bubbles = new BubbleField(size, fieldControl);
        }
        #endregion

        #region Row

        public static readonly DependencyProperty RowProperty =
    DependencyProperty.RegisterAttached("Row", typeof(int), typeof(FieldControl),
    new FrameworkPropertyMetadata(-1, new PropertyChangedCallback(OnRowAttachedPropertyChanged)));
        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static int GetRow(UIElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            return (int)element.GetValue(RowProperty);
        }

        /// <summary>
        /// Sets the row.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="row">The row.</param>
        public static void SetRow(UIElement element, int row)
        {
            if (element == null) throw new ArgumentNullException("element");
            element.SetValue(RowProperty, row);
        }
        private static void OnRowAttachedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d as Bubble;
            if (b == null) return;




            var row = (int)e.NewValue;

            Canvas.SetTop(b, row * bubleSize + margin);

            var column = GetColumn(b);
            if (column < 0)
                return;

            FieldControl f = getField(d);
            if (f == null)
                return;


            if (f.bubbles[row, column] != null)
                throw new OperationCanceledException("Try to move on not empty cell");


            f.bubbles[row, column] = b;
            var oldRow = (int)e.OldValue;
            if (oldRow > -1)
                f.bubbles[oldRow, column] = null;

        }
        #endregion

        #region Column
        public static readonly DependencyProperty ColumnProperty =
    DependencyProperty.RegisterAttached("Column", typeof(int), typeof(FieldControl),
    new FrameworkPropertyMetadata(-1, new PropertyChangedCallback(OnColumnAttachedPropertyChanged)));
        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static int GetColumn(UIElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            return (int)element.GetValue(ColumnProperty);
        }

        /// <summary>
        /// Sets the column.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="column">The column.</param>
        public static void SetColumn(UIElement element, int column)
        {
            if (element == null) throw new ArgumentNullException("element");
            element.SetValue(ColumnProperty, column);
        }

        private static void OnColumnAttachedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d as Bubble;
            if (b == null) return;



            var column = (int)e.NewValue;

            Canvas.SetLeft(b, column * bubleSize + margin);

            var row = GetRow(b);
            if (row < 0)
                return;

            FieldControl f = getField(d);
            if (f == null) return;

            if (f.bubbles[row, column] != null)
                throw new OperationCanceledException("Try to move on not empty cell");
            f.bubbles[row, column] = b;

            var oldColumn = (int)e.OldValue;
            if (oldColumn > -1)
                f.bubbles[row, oldColumn] = null;
        }
        #endregion

        #endregion

        #region Events

        #region FieldChanged

        public static readonly RoutedEvent FieldChangedEvent =
            EventManager.RegisterRoutedEvent("FieldChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FieldControl));

        public event RoutedEventHandler FieldChanged
        {
            add { AddHandler(FieldChangedEvent, value); }
            remove { RemoveHandler(FieldChangedEvent, value); }
        }

        /// <summary>
        /// Called when [field changed].
        /// </summary>
        internal void OnFieldChanged()
        {
            OnFieldChanged(new RoutedEventArgs(FieldChangedEvent, this));
        }

        /// <summary>
        /// Raises the <see cref="E:FieldChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnFieldChanged(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldControl"/> class.
        /// </summary>
        public FieldControl()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(FieldControl_Loaded);

        }

        void FieldControl_Loaded(object sender, RoutedEventArgs e)
        {
            ActiveStrategy = App.Current.Settings.Strategy;
            if (shiftStrategy == null)
            {
                shiftStrategy = strategies[App.Current.Settings.Strategy];
                NewGame();
            }
        }


        #region InterFace

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public int GetCount(BubbleColor color)
        {
            var result = 0;
            foreach (var bubble in bubbles)
            {
                if (bubble != null && bubble.Color == color)
                    result++;
            }
            return result;
        }

        /// <summary>
        /// Does the shift.
        /// </summary>
        public void DoShift()
        {
            foreach (var strategy in shiftStrategy)
            {
                strategy.Do(bubbles);
            }
        }

        /// <summary>
        /// News the game.
        /// </summary>
        public void NewGame()
        {
            SetScore(0);

            layout.Children.Clear();
            bubbles.NewGame();
            OnFieldChanged();

            IsEnabled = true;
        }
        /// <summary>
        /// Redoes this instance.
        /// </summary>
        public void Redo()
        {
            layout.Children.Clear();
            SetScore(bubbles.Redo());
            SetSelectedScore(0);
            OnFieldChanged();
        }
        /// <summary>
        /// Undoes this instance.
        /// </summary>
        public void Undo()
        {
            layout.Children.Clear();
            SetScore(bubbles.Undo());
            SetSelectedScore(0);
            OnFieldChanged();
        }

        /// <summary>
        /// Adds the bubble.
        /// </summary>
        /// <param name="bubble">The bubble.</param>
        public void AddBubble(Bubble bubble)
        {
            bubble.Width = bubble.MinWidth = bubble.MinHeight = bubble.Height = bubleSize;
            bubble.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(bubble_MouseLeftButtonDown);

            layout.Children.Add(bubble);

        }

        public void ShowNulls()
        {
            for (int column = 0; column < bubbles.Size; column++)
            {
                for (int row = 0; row < bubbles.Size; row++)
                {
                    var block = new TextBlock();
                    if (bubbles[row, column] == null)
                    {

                        block.Text = "X";
                    }
                    else
                    {
                        block.Text = "o";
                    }
                    layout.Children.Add(block);
                    Canvas.SetTop(block, row * bubleSize + margin);
                    Canvas.SetLeft(block, column * bubleSize + bubleSize / 3);

                }
            }
        }
        public void ClearLabels()
        {
            List<TextBlock> todelete = new List<TextBlock>();
            foreach (var o in layout.Children)
            {
                if (o is TextBlock)
                    todelete.Add((TextBlock)o);
            }
            foreach (var block in todelete)
            {
                layout.Children.Remove(block);
            }
        }
        #endregion

        void bubble_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                TryKillBubbles((Bubble)sender);
            }
            else
            {
                MakeSelection((Bubble)sender);
            }

        }

        private void TryKillBubbles(Bubble bubble)
        {
            if (bubble.Status != BubbleStatus.Selected)
                return;



            StartKillSelected();

            SetScore(Score + SelectedScore);
            SetSelectedScore(0);

            ClearKilled();

            DoShift();

            bubbles.MakeSnapShot(Score);

            if (bubbles.IsEndGame)
            {
                App.Current.Statistics.AddScore(new GameScore
                                                    {
                                                        Shift = ActiveStrategy, 
                                                        Score = Score,
                                                        FieldSize = Size
                                                    });
                IsEnabled = false;
                MessageBox.Show("End game");
            }

            OnFieldChanged();
        }

        private void StartKillSelected()
        {
            foreach (Bubble bubble in bubbles)
            {
                if (bubble.Status == BubbleStatus.Selected)
                    bubble.KillPlay();
            }
        }

        private void ClearKilled()
        {
            for (var row = 0; row < bubbles.Size; row++)
            {
                for (var column = 0; column < bubbles.Size; column++)
                {
                    var bubble = bubbles[row, column];
                    if (bubble == null || bubble.Status != BubbleStatus.Killed)
                        continue;
                    layout.Children.Remove(bubble);
                    bubbles[row, column] = null;
                }
            }
        }


        private void MakeSelection(Bubble b)
        {
            if (b.Status != BubbleStatus.Normal)
                return;

            var column = GetColumn(b);
            var row = GetRow(b);
            var color = b.Color;


            if (!bubbles.CanSelect(row, column))
                return;

            bubbles.SelectBubble(row, column, color);

            SetSelectedScore(bubbles.SelectedScore);
        }


        public string IsValid()
        {
            return bubbles.IsValid();
        }


        private void SetScore(int value)
        {
            SetValue(ScoreProperty, value);
        }
        private void SetSelectedScore(int value)
        {
            SetValue(SelectedScoreProperty, value);
        }

        #region Property Changed Handlers

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, e);
        }







        private static FieldControl getField(DependencyObject d)
        {
            return (FieldControl)((FrameworkElement)((FrameworkElement)((FrameworkElement)VisualTreeHelper.GetParent(d)).Parent).Parent).Parent;
        }

        #endregion

        private static object IsIntValueNotNegative(DependencyObject o, object value)
        {
            return (((int)value) >= 0) ? value : 0;
        }



    }
}
