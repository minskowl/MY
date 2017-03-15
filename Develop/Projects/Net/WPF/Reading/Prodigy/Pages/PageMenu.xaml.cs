using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Prodigy.Models;
using Reading.Core;

namespace Prodigy.Pages
{
    /// <summary>
    /// Interaction logic for PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {

        public PageMenu()
        {
            InitializeComponent();
           
        }

        private void PageMenu_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MenuModel {NavigationService = NavigationService };
        }








        private void Open<T>()
            where T: new()
        {
            NavigationService.Navigate(new T());
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


        
    }
}
