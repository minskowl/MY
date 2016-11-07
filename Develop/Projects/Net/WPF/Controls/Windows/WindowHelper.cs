using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;

namespace Savchin.Wpf.Controls.Windows
{
    /// <summary>
    /// WindowHelper
    /// </summary>
    public static class WindowHelper
    {


        public static ApplicationSettingsBase GetStateType(DependencyObject obj)
        {
            return (ApplicationSettingsBase)obj.GetValue(StateTypeProperty);
        }

        public static void SetStateType(DependencyObject obj, Type value)
        {
            obj.SetValue(StateTypeProperty, value);
        }

        // Using a DependencyProperty as the backing store for StateType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateTypeProperty =
            DependencyProperty.RegisterAttached("StateType", typeof(ApplicationSettingsBase), typeof(WindowHelper), new UIPropertyMetadata(null, OnStateChanged));

  


        public static string GetStateKey(DependencyObject obj)
        {
            return (string)obj.GetValue(StateKeyProperty);
        }

        public static void SetStateKey(DependencyObject obj, string value)
        {
            obj.SetValue(StateKeyProperty, value);
        }

        // Using a DependencyProperty as the backing store for StateKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateKeyProperty =
            DependencyProperty.RegisterAttached("StateKey", typeof(string), typeof(WindowHelper), new UIPropertyMetadata(null, OnStateChanged));

        private static void OnStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var w = d as Window;
            if(w==null) return;
            var cont = w.GetValue(StateTypeProperty) as ApplicationSettingsBase;
            var key = w.GetValue(StateKeyProperty) as String;
            if(cont==null || key==null)return;
            w.Closing += new System.ComponentModel.CancelEventHandler(w_Closing);
            var state= cont[key] as WindowStateBack;

            if (state != null) state.Restore(w);
        }

        static void w_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var w = sender as Window;
            w.Closing -= w_Closing;

            var cont = w.GetValue(StateTypeProperty) as ApplicationSettingsBase;
            var key = w.GetValue(StateKeyProperty) as String;
            if (cont == null || key == null) return;
            var state = cont[key] as WindowStateBack;
            if (state == null)
            {
                state = new WindowStateBack();
                state.Copy(w);
                cont[key] = state;
            }
            else
            {
                state.Copy(w);
            }
            cont.Save();
        }

     
    }
}
