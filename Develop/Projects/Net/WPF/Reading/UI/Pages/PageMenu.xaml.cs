using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Reading.Core;
using Reading.Models;

namespace Reading.Pages
{
    /// <summary>
    /// Interaction logic for PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();

            DataContext = new MenuModel();


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageSyllables());
        }

        private void buttonWords_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageWords());
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageSettings());
        }
        private void buttonWordList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageWordList());
        }

        private void buttonCounting_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageCounting());
        }
        private void buttonSummation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageSummation());
        }
        private void buttonSummationTable_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageTableSummation());
        }
        private void buttonCompare_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageCompare());
        }
        private void buttonSentences_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageSentence());
        }
        private void buttonFindPair_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFindPair());
        }

        private static void GetSeq(string dictionary = "REVERSE.TXT")
        {

            var reexp = new System.Text.RegularExpressions.Regex(@"([^ауеиояюэыёьъй\s\-][^ауеиояюэыё\s\-]+[ауеиояюэыё])");
            var text = File.ReadAllText(dictionary);
            var mathes = reexp.Matches(text);
            var result = new List<string>();
            foreach (System.Text.RegularExpressions.Match mathe in mathes)
            {
                if (mathe.Value.Length < 3 || result.Contains(mathe.Value)) continue;

                result.Add(mathe.Value);
            }
            File.WriteAllLines(@"c:\result.txt", result);
        }

        private static void SplitWordLists(string dictionary = "REVERSE.TXT")
        {
            var reader = File.OpenText(dictionary);
            var words = new List<List<string>>();
            words.Add(new List<string>());
            words.Add(new List<string>());
            words.Add(new List<string>());
            words.Add(new List<string>());


            var word = reader.ReadLine();
            while (word != null)
            {
                word = word.Trim();
                if (word.IndexOf('-') == -1)
                {
                    var syllablesCount = Primer.GetSyllablesCount(word);
                    if (word.Length > 2 && syllablesCount > 0 && syllablesCount < 5)
                    {
                        var separ = Primer.setProb(word);
                        Debug.Assert(separ.Replace("-", string.Empty).Length == word.Length);
                        words[syllablesCount - 1].Add(separ);
                    }
                }
                word = reader.ReadLine();
            }
            reader.Close();

            for (int index = 0; index < words.Count; index++)
            {
                var list = words[index];
                var fileName = (index + 1) + ".txt";
                if (File.Exists(fileName))
                    File.Delete(fileName);
                if (list.Count > 0)
                    File.WriteAllLines(fileName, list);
            }
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            GetSeq(@"c:\REVERSE.TXT");
            //SplitWordLists("input.txt");
        }


        private void button3_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageComposition());
        }



    }
}
