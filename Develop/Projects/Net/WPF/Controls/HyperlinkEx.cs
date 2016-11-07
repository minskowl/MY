using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Savchin.Wpf.Controls
{
    /// <summary>
    /// Excetended Hyperlink functionality.
    /// Additional featers:
    /// *Hilight visited links
    /// </summary>
    public class HyperlinkEx : Hyperlink
    {


        public bool IsExternal
        {
            get { return (bool)GetValue(IsExternalProperty); }
            set { SetValue(IsExternalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsExternal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExternalProperty =
            DependencyProperty.Register("IsExternal", typeof(bool), typeof(HyperlinkEx), new PropertyMetadata(false));


        private static readonly List<string> visitedLinks = new List<string>();
        private static readonly Dictionary<string, List<HyperlinkEx>> trackingLinks = new Dictionary<string, List<HyperlinkEx>>();
        private static readonly SolidColorBrush visitedBrush = new SolidColorBrush(Colors.Purple);

        public string Key
        {
            get
            {
                if (NavigateUri != null)
                    return NavigateUri.ToString();
                if (Tag != null)
                    return Tag.ToString();
                if (CommandParameter != null)
                    return CommandParameter.ToString();
                return null;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                Tag = value;
                StartTrackLink(this);
            }
        }

        public HyperlinkEx()
        {
            RequestNavigate += Hyperlink_RequestNavigate;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (IsExternal)
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }

        }

        /// <summary>
        /// Clears the tracking.
        /// </summary>
        public static void ClearTracking()
        {
            visitedLinks.Clear();
            trackingLinks.Clear();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.FrameworkContentElement.Initialized"/> event. This method is invoked whenever <see cref="P:System.Windows.FrameworkContentElement.IsInitialized"/> is set to true.
        /// </summary>
        /// <param name="e">Event data for the event.</param>
        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);
            this.Unloaded += new System.Windows.RoutedEventHandler(HyperlinkEx_Unloaded);
            StartTrackLink(this);
        }

        /// <summary>
        /// Handles the Unloaded event of the HyperlinkEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void HyperlinkEx_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            EndTrackLink(this);
        }


        /// <summary>
        /// Handles the <see cref="E:System.Windows.Documents.Hyperlink.Click"/> routed event.
        /// </summary>
        protected override void OnClick()
        {
            SetVisited();

            base.OnClick();
        }


        private void SetVisited()
        {
            if (Foreground == visitedBrush)
                return;

            var key = Key;
            if (string.IsNullOrEmpty(key))
                return;

            if (!visitedLinks.Contains(key))
                visitedLinks.Add(key);

            Foreground = visitedBrush;
            if (trackingLinks.ContainsKey(key))
            {
                var storage = trackingLinks[key];
                if (storage != null)
                    foreach (HyperlinkEx hyperlinkEx in storage)
                    {
                        hyperlinkEx.Foreground = visitedBrush;
                    }
            }
        }

        /// <summary>
        /// Starts the track link.
        /// </summary>
        /// <param name="link">The link.</param>
        private static void StartTrackLink(HyperlinkEx link)
        {
            var key = link.Key;
            if (string.IsNullOrEmpty(key))
                return;

            if (!trackingLinks.ContainsKey(key))
            {
                var storage = new List<HyperlinkEx> { link };
                trackingLinks.Add(key, storage);
            }
            else
            {
                var storage = trackingLinks[key];
                if (!storage.Contains(link))
                    storage.Add(link);
            }

            if (visitedLinks.Contains(key))
                link.Foreground = visitedBrush;
        }

        /// <summary>
        /// Ends the track link.
        /// </summary>
        /// <param name="link">The link.</param>
        private static void EndTrackLink(HyperlinkEx link)
        {
            var key = link.Key;
            if (string.IsNullOrEmpty(key) || !trackingLinks.ContainsKey(key))
                return;
            var storage = trackingLinks[key];

            if (storage.Contains(link))
                storage.Remove(link);

            if (storage.Count == 0)
                trackingLinks.Remove(key);
        }


    }
}
