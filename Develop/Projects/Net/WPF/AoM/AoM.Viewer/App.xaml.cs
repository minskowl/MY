using System.Windows;
using AoM.Viewer.Data;

namespace AoM.Viewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SourceData.ReadCraft();
        }
    }
}
