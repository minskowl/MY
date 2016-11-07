using KnowledgeBase.Dal;
using KnowledgeBase.KbTools.Commands;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Mssql.Dal;

namespace KnowledgeBase.KbTools.Core
{
    public static class AppCore
    {
        private static readonly FormMain  mainForm = new FormMain();
        private static readonly CommandCollection commands= new CommandCollection();

        /// <summary>
        /// Gets the main form.
        /// </summary>
        /// <value>The main form.</value>
        public static FormMain MainForm
        {
            get { return mainForm; }
        }

        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <value>The commands.</value>
        public static CommandCollection Commands
        {
            get { return commands; }
        }

        /// <summary>
        /// Initializes the <see cref="AppCore"/> class.
        /// </summary>
        static AppCore()
        {
            KbContext.CurrentKb = new KbContext(new DalSingleThreadProvider(new MssqlFactoryProvider(string.Empty)));
        }
    }
}
