using System.Windows;
using Advertiser.Core;
using Advertiser.Entities;
using Savchin.Development;

namespace Advertiser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AdvContext.Init();
          
        }
    }
}
