using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Desktop.Core;
using Savchin.Wpf.Core;

namespace KnowledgeBase.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for UserRightsControl.xaml
    /// </summary>
    public partial class UserRightsControl : UserControl
    {
        public UserRightsControl()
        {
            InitializeComponent();
        
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var expander in this.FindChildren<Expander>())
            {
                DisplayHeaderInfo(expander);
            }
        }

        /// <summary>
        /// Shows the rights.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="rights">The rights.</param>
        public void ShowRights(User user, IList<UserRight> rights)
        {
            var trees = new Dictionary<Permission, CategoryTreeControl>
                            {
                                {Permission.View, treeView},
                                {Permission.Publish, treePublish},
                                {Permission.Moderate, treeModerate},
                                {Permission.Admin, treeAdmin}
                            };

            if (user.RootPermission.HasValue)
                trees[user.RootPermission.Value].CheckNode(0);

            foreach (var right in rights)
            {
                trees[right.Permission].CheckNode(right.CategoryID);
            }


        }
        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Permission> GetPermissions()
        {
            var result = new Dictionary<int, Permission>();

            FillRights(result, treeView, Permission.View);
            FillRights(result, treePublish, Permission.Publish);
            FillRights(result, treeModerate, Permission.Moderate);
            FillRights(result, treeAdmin, Permission.Admin);

            return result;
        }
        private void FillRights(Dictionary<int, Permission> storage, CategoryTreeControl tree, Permission permission)
        {
            foreach (var categoryID in tree.GetSelectedCategories())
            {
                storage[categoryID] = permission;
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            var exp = sender as Expander;
            var cont = (CategoryTreeControl)exp.Content;

            exp.Header = cont.Name.Substring(4);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            DisplayHeaderInfo( (Expander)sender  );
        }

        private void DisplayHeaderInfo(Expander exp)
        {
            var cont = (CategoryTreeControl)exp.Content;
            
            if(cont.RootNode.IsChecked??false)
            {
                exp.Header = cont.Name.Substring(4) + " : " + cont.RootNode.Text;
                return;
            }
            var builder = new StringBuilder();
            FindChecked(builder,cont.RootNode.Childrens);
            if(builder.Length>0)
            {
                exp.Header = cont.Name.Substring(4) + " : " + builder.ToString(0, builder.Length-2);
            }
            else
            {
                exp.Header = cont.Name.Substring(4);
            }
        }

        private void FindChecked(StringBuilder result,IEnumerable<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.IsChecked ?? false)
                {
                    result.AppendFormat("{0}, ", node.Text);
                }
                else
                {
                    FindChecked(result,node.Childrens);
                }
            }
        }

  
    }
}
