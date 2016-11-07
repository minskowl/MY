using System;
using System.Windows.Controls;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Google;
using KnowledgeBase.Dal;
using Savchin.Wpf.Controls.Core;

namespace KnowledgeBase.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for GoogleConnectionControl.xaml
    /// </summary>
    public partial class GoogleConnectionControl : UserControl
    {
        public GoogleConnectionControl()
        {
            InitializeComponent();

#if DEBUG
            boxLogin.Text = "minskowl@gmail.com";
            boxPassword.Password = "elfxf123";
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
                var context = new GoogleContext(new DalMultiThreadProvider(null));
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
