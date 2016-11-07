using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Savchin.Forms.Core
{
    /// <summary>
    /// ControlHelper
    /// </summary>
    public static class ControlHelper
    {
        private static bool? _isInDesignMode;
        /// <summary>
        /// Gets a value indicating whether [design mode].
        /// </summary>
        /// <value><c>true</c> if [design mode]; otherwise, <c>false</c>.</value>
        public static bool DesignMode
        {
            get
            {
                if (_isInDesignMode == null)
                {
                    _isInDesignMode = false;
#if DEBUG
                    if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime ||
                        Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV"))
                    {
                        _isInDesignMode = true;
                    }
#endif
                }
                return _isInDesignMode.Value;  
            }
        }

        /// <summary>
        /// Determines whether [is in design mode].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is in design mode]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInDesignMode()
        {
            return DesignMode;
        }


        /// <summary>
        /// Determines whether [is in design mode] [the specified control].
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>
        /// 	<c>true</c> if [is in design mode] [the specified control]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInDesignMode(this Control control)
        {
            return DesignMode;
        }

        /// <summary>
        /// Finds the controls recursive.
        /// </summary>
        /// <typeparam name="TControl">The type of the control.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="result">The result.</param>
        public static void FindControlsRecursive<TControl>(Control.ControlCollection collection, List<TControl> result)
           where TControl : class
        {
            foreach (Control control in collection)
            {
                var obj = control as TControl;
                if (obj != null && !result.Contains(obj))
                    result.Add(obj);


                FindControlsRecursive(control.Controls, result);
            }

        }

        /// <summary>
        /// Finds the controls.
        /// </summary>
        /// <typeparam name="TControl">The type of the control.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public static List<TControl> FindControls<TControl>(this Control container)
          where TControl : class
        {
            var result = new List<TControl>();
            foreach (Control control in container.Controls)
            {
                var obj = control as TControl;
                if (obj != null && !result.Contains(obj))
                    result.Add(obj);
            }
            return result;
        }

        /// <summary>
        /// Finds the controls recursive.
        /// </summary>
        /// <typeparam name="TControl">The type of the control.</typeparam>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static List<TControl> FindControlsRecursive<TControl>(this Control control)
         where TControl : class
        {
            var result = new List<TControl>();
            FindControlsRecursive(control.Controls, result);
            return result;
        }
    }
}
