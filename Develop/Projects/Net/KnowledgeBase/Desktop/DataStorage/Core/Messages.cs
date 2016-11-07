using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;

namespace KnowledgeBase.Desktop.Core
{
    static class Messages
    {
        /// <summary>
        /// Shows the security alert.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void ShowSecurityAlert(PermissionException ex)
        {
            ShowSecurityAlert(ex.Message);
        }

        /// <summary>
        /// Shows the security alert.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void ShowSecurityAlert(string text)
        {
            ShowSecurityAlert("Security alert.", text);
        }

        /// <summary>
        /// Shows the security alert.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="categoryID">The category ID.</param>
        public static void ShowSecurityAlert(string text, int categoryID)
        {
            var category = KbContext.CurrentKb.ManagerCategory.GetByID(categoryID);
            if (category != null)
            {
                ShowSecurityAlert(string.Format("Category {0} security alert.", category.Name), text);
            }
            else
            {
                ShowSecurityAlert(text);
            }

        }

        private static void ShowSecurityAlert(string title, string text)
        {
            MessageBox.Show(Application.Current.MainWindow, text, title, MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}
