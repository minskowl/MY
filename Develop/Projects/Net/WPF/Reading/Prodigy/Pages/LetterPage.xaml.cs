using System.Windows.Controls;
using Prodigy.Models.Reading;

namespace Prodigy.Pages
{
    /// <summary>
    /// Логика взаимодействия для LetterPage.xaml
    /// </summary>
    public partial class LetterPage : Page
    {
        public LetterPage()
        {
            InitializeComponent();

            DataContext = new LetterModel();
        }
    }
}
