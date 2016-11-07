using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
    /// <summary>
    /// ControlHelper
    /// </summary>
    public class ControlHelper
    {
        public const string NameSeparator = ".";
        /// <summary>
        /// Parents the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static object GetParent(object o)
        {
            if (o is Control)
            {
                return ((Control)o).Parent;
            }
            if (o is MenuItem)
            {
                return ((MenuItem)o).Parent;
            }
            if (o is Component)
            {
                return ((Component)o).Container;
            }
            return null;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static string GetName(object o)
        {
            if (o is ToolStripControlHost)
            {
                return ((ToolStripControlHost)o).Name;
            }
            if (o is ToolStripItem)
            {
                return ((ToolStripItem)o).Name;
            }
            if (o is Control)
            {
                return ((Control)o).Name;
            }
            if (o is MenuItem)
            {
                return ((MenuItem)o).Text.Replace("&", string.Empty).Replace(".", string.Empty);
            }
            if (o is MainMenu)
            {
                return "MainMenu";
            }
            if (o is ContextMenu)
            {
                return "ContextMenu";
            }
            if (o is Component)
            {
                return ((Component)o).Site.Name;
            }
            throw new Exception("Object name not defined for type " + o.GetType().Name);
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        /// <exception cref="AmbiguousNameException"> Throw if can't build uniue name</exception>
        public static string GetUniqueShortName(Form form, object control)
        {

            var parent = GetParent(control);
            var name = GetName(control);

            do
            {
                if (IsGoodName(name, form)) break;

                if (parent == null || parent is Form)
                {
                    throw new AmbiguousNameException("Name is not unique " + name);
                }
                name = GetName(parent) + NameSeparator + name;
                parent = GetParent(parent);

            } while (true);

            if (name.StartsWith(NameSeparator) && control is MenuItem)
            {
                name = GetUniqueShortName(null, ((MenuItem)control).GetContextMenu().SourceControl) + name;
            }

            return name;
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static string GetFullName(Form form, object control)
        {

            var parent = GetParent(control);
            var name = GetName(control);

            do
            {
              

                if (parent == null || parent is Form)
                {
                    if (IsGoodName(name, form)) break;

                    throw new AmbiguousNameException("Name is not unique " + name);
                }
                name = GetName(parent) + NameSeparator + name;
                parent = GetParent(parent);

            } while (true);

            if (name.StartsWith(NameSeparator) && control is MenuItem)
            {
                name = GetFullName(null, ((MenuItem)control).GetContextMenu().SourceControl) + name;
            }

            return name;
        }
        private static bool IsGoodName(string name, Form form)
        {
            Control control = null;
            MenuItem menuItem = null;
            try
            {
                control = new Finder<Control>(name, form).Find();
            }
            catch (NoSuchControlException)
            {
            }
            try
            {
                menuItem = new Finder<MenuItem>(name, form).Find();
            }
            catch (NoSuchControlException)
            {
            }
            return !(
                    (control == null && menuItem == null) ||
                    (control != null && menuItem != null)
                    );
        }
    }
}
