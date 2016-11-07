using System;
using System.Configuration;
using System.Windows.Controls;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Managers;
using KnowledgeBase.Dal;
using Savchin.Wpf.Controls.Core;

namespace KnowledgeBase.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for ServerConnectionControl.xaml
    /// </summary>
    public partial class ServerConnectionControl : UserControl
    {
        public ServerConnectionControl()
        {
            InitializeComponent();

#if DEBUG
            boxLogin.Text = "admin";
            boxPassword.Password = "kbyercXX2";
#endif
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns></returns>
        public KbContext Connect()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["context"].ConnectionString;
                var factory = new Mssql.Dal.MssqlFactoryProvider(connectionString);
                
                var context = new KbContext(new DalMultiThreadProvider(factory), new ManagersFactory(factory));
                factory.Context = context;
                return context.Login(boxLogin.Text.Trim(), boxPassword.Password)
                    ? context : null;
            }
            catch (Exception ex)
            {
                ErrorForm.Show("Error connect", ex);
                return null;
            }
        }
    }
}
