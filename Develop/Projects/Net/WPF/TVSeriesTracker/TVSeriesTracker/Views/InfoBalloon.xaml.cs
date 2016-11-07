using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Savchin.Text;
using Savchin.Wpf.Controls.NotifyIcon;

namespace TVSeriesTracker.Views
{
  /// <summary>
  /// Interaction logic for InfoBalloon.xaml
  /// </summary>
  public partial class InfoBalloon 
  {
    private bool isClosing = false;

    #region Title dependency property

    /// <summary>
    /// Description
    /// </summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title",
                                    typeof (string),
                                    typeof (InfoBalloon),
                                    new FrameworkPropertyMetadata(string.Empty));

    /// <summary>
    /// A property wrapper for the <see cref="BalloonTextProperty"/>
    /// dependency property:<br/>
    /// Description
    /// </summary>
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }



    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(InfoBalloon), new PropertyMetadata(String.Empty,OnTextChanged));

      private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
          var text = e.NewValue as string;
         var lines= text.CountLines();
          var c = d as InfoBalloon;

          c.Height = 70 + lines*16;
      }

      #endregion


    public InfoBalloon()
    {
      InitializeComponent();
      TaskbarIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
    }

    public void CloseBaloon()
    {
        //the tray icon assigned this attached property to simplify access
        TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
        taskbarIcon.CloseBalloon();
    }


    /// <summary>
    /// By subscribing to the <see cref="TaskbarIcon.BalloonClosingEvent"/>
    /// and setting the "Handled" property to true, we suppress the popup
    /// from being closed in order to display the fade-out animation.
    /// </summary>
    private void OnBalloonClosing(object sender, RoutedEventArgs e)
    {
      e.Handled = true;
      isClosing = true;
    }


    /// <summary>
    /// Resolves the <see cref="TaskbarIcon"/> that displayed
    /// the balloon and requests a close action.
    /// </summary>
    private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
    {
        CloseBaloon();
    }



      /// <summary>
    /// If the users hovers over the balloon, we don't close it.
    /// </summary>
    private void grid_MouseEnter(object sender, MouseEventArgs e)
    {
      //if we're already running the fade-out animation, do not interrupt anymore
      //(makes things too complicated for the sample)
      if (isClosing) return;

      //the tray icon assigned this attached property to simplify access
      TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
      taskbarIcon.ResetBalloonCloseTimer();
    }


    /// <summary>
    /// Closes the popup once the fade-out animation completed.
    /// The animation was triggered in XAML through the attached
    /// BalloonClosing event.
    /// </summary>
    private void OnFadeOutCompleted(object sender, EventArgs e)
    {
      Popup pp = (Popup)Parent;
      pp.IsOpen = false;
    }
  }
}
