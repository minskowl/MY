using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Savchin.Bubbles.Controls;
using Savchin.Bubbles.Core;
using Bubbles.Braker;

namespace Savchin.Bubbles
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowMain"/> class.
        /// </summary>
        public WindowMain()
        {
            InitializeComponent();

            boxField.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(boxField_PropertyChanged);


            CreateShiftMenu();
        }

        private void CreateShiftMenu()
        {
            var strategies = Enum.GetValues(typeof(ShiftStrategy));
            var activeStrategy = App.Current.Settings.Strategy;

            foreach (ShiftStrategy strategy in strategies)
            {
                var item = new MenuItem
                               {
                                   Header = strategy,
                                   IsCheckable = true,
                                   IsChecked = strategy == activeStrategy
                               };
                item.Click += MenuShift_OnClick;
                menuShift.Items.Add(item);
            }
        }

        void boxField_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FieldControl.SelectedScoreProperty.Name)
            {
                labelSelectedScore.Text = boxField.SelectedScore.ToString();
            }
            else if (e.PropertyName == FieldControl.ScoreProperty.Name)
            {
                labelScore.Text = boxField.Score.ToString();
            }
        }

        private void boxField_FieldChanged(object sender, RoutedEventArgs e)
        {
            panelColorStatisics.Compute(boxField);
        }

        #region Menu Items
        private void MenuItemGameNew_OnClick(object sender, RoutedEventArgs e)
        {
            boxField.NewGame();
        }

        private void menuItemShiftAgain_Click(object sender, RoutedEventArgs e)
        {
            boxField.DoShift();
        }

        private void MenuItemShowNulls_OnClick(object sender, RoutedEventArgs e)
        {
            boxField.ShowNulls();
        }

        private void MenuItemClearLabels_OnClick(object sender, RoutedEventArgs e)
        {
            boxField.ClearLabels();
        }

        private void menuItemIsValid_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(boxField.IsValid());
        }


        /// <summary>
        /// Handles the Click event of the menuItemAbout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void menuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            using (var window = new AboutBox())
            {
                window.ShowDialog();
            }
        }
        /// <summary>
        /// Handles the OnClick event of the MenuItemStatistics control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MenuItemStatistics_OnClick(object sender, RoutedEventArgs e)
        {
            new WindowGamesStatistics { Owner = this }.ShowDialog();
        }
        private void MenuShift_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Game would be restarted.", Settings.AppName, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                return;
            var item = sender as MenuItem;

            if (item == null)
                return;
            item.IsChecked = true;

            labelStrategy.Text = item.Header.ToString();
            boxField.ActiveStrategy = (ShiftStrategy)Enum.Parse(typeof(ShiftStrategy), labelStrategy.Text);
        }
        #endregion

        #region Commands
        private void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = boxField.CanUndo;
        }

        private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            boxField.Undo();
        }

        /// <summary>
        /// Handles the CanExecute event of the RedoCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void RedoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = boxField.CanRedo;
        }

        /// <summary>
        /// Handles the Executed event of the RedoCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void RedoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (boxField.CanRedo)
                boxField.Redo();
        }
        #endregion


    }
}
