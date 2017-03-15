using System.Windows;
using System.Windows.Controls;
using Savchin.Wpf.Core;

namespace Prodigy.Controls
{
    /// <summary>
    /// Interaction logic for SentenceControl.xaml
    /// </summary>
    public partial class SentenceControl : UserControl
    {

        private Style labelStyle;
        private Style buttonStyle;

        public string Sentence
        {
            get { return (string)GetValue(SentenceProperty); }
            set { SetValue(SentenceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sentence.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SentenceProperty =
            DependencyProperty.Register("Sentence", typeof(string), typeof(SentenceControl),
            new UIPropertyMetadata(null, OnSentencePropertyChanged));




        public SentenceControl()
        {
            InitializeComponent();

            if (this.IsDesignMode()) return;
            labelStyle = (Style)App.CurrentApp.Resources["SyllableLabel"];
            buttonStyle = (Style)App.CurrentApp.Resources["SyllableButton"];
        }

        private static void OnSentencePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SentenceControl)d).ShowSentence();
        }

        private void ShowSentence()
        {
            root.Children.Clear();

            var text = Sentence;
            if (text == null) return;
            var words = text.Split(new char[] { ' ' });
            for (int index = 0; index < words.Length; index++)
            {
                var word = words[index];
                AddButton(word);
                if (index < words.Length - 1)
                {
                    root.Children.Add(new Label
                    {
                        Content = " ",
                        Style = labelStyle
                    });
                }
            }
        
        }
        private void AddButton(string text)
        {
            var button = new SpeakButton
            {
                Content = text,
                Style = buttonStyle
            };
            root.Children.Add(button);
        }
    }
}
