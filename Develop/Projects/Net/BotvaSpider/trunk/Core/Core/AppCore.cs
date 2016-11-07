using System;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Controls;
using BotvaSpider.Logging;


namespace BotvaSpider.Core
{
    /// <summary>
    /// AppCore
    /// </summary>
    public static class AppCore
    {

        #region Properties

        private static readonly CompositLogger AllLogs = new CompositLogger();
        public static readonly Version Version;
        public static readonly Logger LogFights = new Logger(LoggerType.Fights);
        public static readonly Logger LogMine = new Logger(LoggerType.Mine);
        public static readonly Logger LogAccountant = new Logger(LoggerType.Accountant);
        public static readonly Logger LogSystem = new Logger(LoggerType.System);
        public static readonly Logger LogOutput = new Logger();

        #region Settings Sections
        private static AcountSettings acountSettings;
        /// <summary>
        /// Gets the app settings.
        /// </summary>
        /// <value>The app settings.</value>
        public static AcountSettings AcountSettings
        {
            get { return acountSettings; }
        }

        private static AppSettings appSettings;
        /// <summary>
        /// Gets the app settings.
        /// </summary>
        /// <value>The app settings.</value>
        public static AppSettings AppSettings
        {
            get { return appSettings; }
        }
        private static AttackSettings attackSettings;
        /// <summary>
        /// Gets the attack settings.
        /// </summary>
        /// <value>The attack settings.</value>
        public static AttackSettings AttackSettings
        {
            get { return attackSettings; }
        }
        private static GameSettings gameSettings;
        /// <summary>
        /// Gets the gameSettings.
        /// </summary>
        /// <value>The gameSettings.</value>
        public static GameSettings GameSettings
        {
            get { return gameSettings; }
        }
        private static BotvaSettings botvaSettings;
        /// <summary>
        /// Gets the botva settings.
        /// </summary>
        /// <value>The botva settings.</value>
        public static BotvaSettings BotvaSettings
        {
            get { return botvaSettings; }
        }

        private static MinerSettings minerSettings;

        /// <summary>
        /// Gets the miner settings.
        /// </summary>
        /// <value>The miner settings.</value>
        public static MinerSettings MinerSettings
        {
            get { return minerSettings; }
        }
        private static AccountantSettings accountantSettings;
        /// <summary>
        /// Gets the accountant settings.
        /// </summary>
        /// <value>The accountant settings.</value>
        public static AccountantSettings AccountantSettings
        {
            get { return accountantSettings; }
        }
        #endregion

        /// <summary>
        /// Gets or sets the form main.
        /// </summary>
        /// <value>The form main.</value>
        public static MainFormBase FormMain
        {
            get; private set;
        }

        #endregion
        /// <summary>
        /// Initializes the <see cref="AppCore"/> class.
        /// </summary>
        static AppCore()
        {
            Version = typeof(AppCore).Assembly.GetName().Version;
            AllLogs.Add(LogFights);
            AllLogs.Add(LogMine);
            AllLogs.Add(LogAccountant);
            AllLogs.Add(LogSystem);
            AllLogs.Add(LogOutput);
            
        }

        /// <summary>
        /// Initilaizes this instance.
        /// </summary>
        /// <returns></returns>
        public static bool Initilaize(MainFormBase mainForm)
        {
            FormMain = mainForm;
            appSettings = AppSettings.Create();
            if (!LoadSettings(appSettings.GameConfig)) return false;
            
            return true;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="type">The type.</param>
        public static ILogger GetLogger(LoggerType type)
        {
            switch (type)
            {
                case LoggerType.Output:
                    return LogOutput;
                case LoggerType.Fights:
                    return LogFights;
                case LoggerType.System:
                    return LogSystem;
                case LoggerType.Accountant:
                    return LogAccountant;
                case LoggerType.Mine:
                    return LogMine;
                case LoggerType.All:
                    return AllLogs;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }
        /// <summary>
        /// Sets the active settings.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void SetActiveSettings(string path)
        {
            LoadSettings(path);
            appSettings.GameConfig = path;
            appSettings.Save();
        }


        /// <summary>
        /// Unloads this instance.
        /// </summary>
        public static void Unload()
        {
            if (gameSettings == null || appSettings == null) return;
            gameSettings.Save();
            appSettings.Save();
            gameSettings = null;
            appSettings = null;

        }

        private static bool LoadSettings(string path)
        {
            try
            {
                gameSettings = GameSettings.Load(path);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Error load settings " + path, ex);

                if (MessageBox.Show(@"Создать настройки по умолчанию?
Yes -создадуться настройки поумолчанию. После создания  проверьте пожалуйста настройки.
No - подправить настройки в ручную и запустить заново бота (Для продвинутых пользователей).",
                    "Незагрузился файл настроек" + path, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    gameSettings = GameSettings.Create(path);
                    GameSettings.Save();
                }
                else
                {
                    return false;
                }


            }

            botvaSettings = gameSettings.BotvaSettings;
            accountantSettings = botvaSettings.AccountantSettings;
            minerSettings = botvaSettings.MinerSettings;
            attackSettings = botvaSettings.AttackSettings;
            acountSettings = botvaSettings.AcountSettings;
            return true;
        }


    }
}
