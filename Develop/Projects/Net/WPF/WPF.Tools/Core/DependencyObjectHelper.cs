using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;


namespace Savchin.Wpf.Core
{
    /// <summary>
    ///DependencyObjectHelper
    /// </summary>
    public static class DependencyObjectHelper
    {

        /// <summary>
        /// Clones the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static DependencyObject Clone(this DependencyObject obj)
        {
            string saved = XamlWriter.Save(obj);
            return (DependencyObject)XamlReader.Load(XmlReader.Create(new StringReader(saved)));
        }

        /// <summary>
        /// Determines whether [is design mode] [the specified obj].
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// 	<c>true</c> if [is design mode] [the specified obj]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDesignMode(this DependencyObject obj)
        {
            return DesignerProperties.GetIsInDesignMode(obj);
        }

        /// <summary>
        /// Finds the children.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj">The dep obj.</param>
        /// <returns></returns>
        public static IEnumerable<T> FindChildren<T>(this DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                {
                    yield return (T)child;
                }

                foreach (T childOfChild in FindChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }

        }

        /// <summary>
        /// Finds the parent.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="StopAt">The stop at.</param>
        /// <returns></returns>
        public static T FindParent<T>(DependencyObject item, Type StopAt) where T : class
        {
            if (item is T) return item as T;

            var parent = VisualTreeHelper.GetParent(item);
            if (parent == null) return null;


            var type = parent.GetType();
            if (StopAt != null && (type.IsSubclassOf(StopAt) || type == StopAt))
            {
                return null;
            }

            return type.IsSubclassOf(typeof(T)) || type == typeof(T) ?
                parent as T : FindParent<T>(parent, StopAt);
        }

        /// <summary>
        /// Finds the parent.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static T FindParent<T>(DependencyObject item) where T : class
        {
            if (item is T) return item as T;

            var parent = VisualTreeHelper.GetParent(item);
            if (parent == null) return null;

            var type = parent.GetType();


            return (type.IsSubclassOf(typeof(T))) || (type == typeof(T)) ?
                parent as T : FindParent<T>(parent);
        }


        /// <summary>
        /// Determines whether the specified obj is valid bindindings.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        ///   <c>true</c> if the specified obj is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this DependencyObject obj)
        {
            // Validate all the bindings on the obj
            var localValues = obj.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                var entry = localValues.Current;
                if (!BindingOperations.IsDataBound(obj, entry.Property)) continue;
                var expression = BindingOperations.GetBindingExpression(obj, entry.Property);

                if (expression != null && expression.HasError)
                    return false;
            }

            // Validate all the bindings on the children
            return LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>().All(IsValid);
        }

        /// <summary>
        /// Updates the bindings.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public static void UpdateBindings(this DependencyObject obj)
        {
            // Validate all the bindings on the obj
            var localValues = obj.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                var entry = localValues.Current;
                if (!BindingOperations.IsDataBound(obj, entry.Property)) continue;
                var expression = BindingOperations.GetBindingExpression(obj, entry.Property);

                if (expression != null)
                    expression.UpdateSource();
            }
            foreach (var o in LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>())
            {
                o.UpdateBindings();
            }
        }


        /// <summary>
        /// Sets the focus.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public static void SetFocus(this UIElement obj)
        {
            obj.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(() => Keyboard.Focus(obj)));
        }
    }
}
