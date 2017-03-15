using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Prodigy.Models.Core;
using Prodigy.Pages;
using Reading.Core;
using Savchin.Wpf.Input;

namespace Prodigy.Models
{
    public class MenuModel : BaseModel
    {
        public BitmapSource Logo { get; private set; }
        public override string Title => "Обучайка";

        public ICommand NavigateCommand { get; private set; }

        public NavigationService NavigationService { get; set; }
        public MenuModel()
        {
            NavigateCommand= new DelegateCommand<Type>(OnNavigateCommand);
            Logo = ResourceProvider.GetImage("Logo");
        }

        private void OnNavigateCommand(Type type)
        {
            var page = Activator.CreateInstance(type) as Page;
            NavigationService.Navigate(page);
            
            
        }
    }
}
