using System.Windows.Controls;
using Reading.Models;

namespace Reading.Pages
{
    /// <summary>
    /// Логика взаимодействия для LetterPage.xaml
    /// </summary>
    public partial class LetterPage 
    {
        public LetterPage()
        {
            InitializeComponent();

            DataContext = new LetterModel();
        }
    }
}
