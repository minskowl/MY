using System.Windows.Controls;
using Prodigy.Models.Reading;
using Savchin.Wpf.Core;

namespace Prodigy.Pages.Reading
{
    /// <summary>
    /// Логика взаимодействия для LetterPage.xaml
    /// </summary>
    public partial class LetterPage : Page
    {
        public LetterPage()
        {
            InitializeComponent();
            if (!this.IsDesignMode())
                DataContext = new LetterModel();
        }
    }
}
