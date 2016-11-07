using System.Globalization;
using System.Reflection;
using System.Windows;
using Savchin.Wpf.Controls.Localization;
using SimpleInjector;
using TVSeriesTracker.Core;
using TVSeriesTracker.Imdb;
using TVSeriesTracker.Models;
using TVSeriesTracker.Properties;

namespace TVSeriesTracker
{
    public partial class App
    {
        public static Container Container { get; private set; }

        static App()
        {
            CreateContainer();

            SetLocalization();
        }

        private static void CreateContainer()
        {
            var container = new Container();


            container.Register<IDataBase, DataBase>(Lifestyle.Singleton);
            container.Register<IImdbManager, ImdbManager>();
            container.Register<IWatchListPageModel>(() => new WatchListPageModel(
                                                              container.GetInstance<IImdbManager>(),
                                                              container.GetInstance<IDataBase>()), Lifestyle.Singleton);

            container.RegisterSingle<ITranslationManager>(TranslationManager.Instance);
            container.Verify();

            Container = container;


        }

      

        private static void SetLocalization()
        {
            TranslationManager.Instance.TranslationProvider = new ResxTranslationProvider("TVSeriesTracker.Properties.Resources",Assembly.GetAssembly(typeof (Resources)));
            TranslationManager.Instance.CurrentLanguage = new CultureInfo(Settings.Default.Language);
        }
    }
}
