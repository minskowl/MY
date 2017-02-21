using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Reading.Core;
using Savchin.Wpf.Core;

namespace Reading.Controls
{
    /// <summary>
    /// Interaction logic for WordControl.xaml
    /// </summary>
    public partial class WordControl
    {
        #region Properties
        private readonly Style labelStyle;
        private readonly Style buttonStyle;

        public bool SyllableView
        {
            get { return (bool)GetValue(SyllableViewProperty); }
            set { SetValue(SyllableViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SyllableView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SyllableViewProperty =
            DependencyProperty.Register("SyllableView", typeof(bool), typeof(WordControl), new UIPropertyMetadata(true, OnSyllableViewPropertyChanged));




        public Word Word
        {
            get { return (Word)GetValue(WordProperty); }
            set { SetValue(WordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Word.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WordProperty =
            DependencyProperty.Register("Word", typeof(Word), typeof(WordControl), new UIPropertyMetadata(null, OnWordPropertyChanged));
        #endregion




        public WordControl()
        {
            InitializeComponent();

            if (this.IsDesignMode()) return;

            labelStyle = (Style)App.CurrentApp.Resources["SyllableLabel"];
            buttonStyle = (Style)App.CurrentApp.Resources["SyllableButton"];
        }


        private static void OnWordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WordControl)d).ShowWord();
        }

        private static void OnSyllableViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WordControl)d).ShowWord();
        }

        private void ShowWord()
        {
            root.Children.Clear();

            var word = Word;
            if (word == null) return;
            if(SyllableView)
            {
                ShowSyllabled(word.Syllabled.Split('-'));
            }
            else
            {
                AddButton(word.Text);
            }



        }

        private void ShowSyllabled(string[] parts)
        {
            for (int index = 0; index < parts.Length; index++)
            {
                var syllable = parts[index];
                AddButton(syllable);
                if (index < parts.Length - 1)
                {
                    root.Children.Add(new Label
                                          {
                                              Content = "-",
                                              Style = labelStyle
                                          });
                }
            }
        }

        private void AddButton(string text)
        {
            var button = new SpeakButton
                             {
                                 Content = text.ToUpper(),
                                 Style = buttonStyle
                             };
            root.Children.Add(button);
        }
    }
}
