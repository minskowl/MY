using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Playback.Core;

namespace Playback
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        private DispatcherTimer timer;
        private FrameSource _source;

        public PlayerControl()
        {
            InitializeComponent();
        }

        public void ShowGame(FrameSource source)
        {
            _source = source;
            this.DataContext = _source;

            var frame = _source.NextFrame();
            controlScene.Show(frame);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var frame = _source.NextFrame();
            if (frame == null)
            {
                timer.Stop();
            }
            else
            {
                controlScene.Show(frame);
            }

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timer.Tick -= timer1_Tick;


            _source = null;

        }

        private void ButtonPlayForward_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void ButtonNextFrame_Click(object sender, RoutedEventArgs e)
        {
            var frame = _source.NextFrame();
            if (frame != null)
                controlScene.Show(frame);
        }

        private void ButtonPrevFrame_Click(object sender, RoutedEventArgs e)
        {
            var frame = _source.PrevFrame();
            if (frame != null)
                controlScene.Show(frame);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            var frame = _source.GetFrame(0);
            if (frame != null)
                controlScene.Show(frame);
        }
    }
}
