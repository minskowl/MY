using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Savchin.Wpf.Core
{
    public static class UI
    {
        #region InputBindings in style

        public static InputBindingCollection GetInputBindings(UIElement element)
        {
            return (InputBindingCollection)element.GetValue(InputBindingsProperty);
        }

        public static void SetInputBindings(UIElement element, InputBindingCollection inputBindings)
        {
            element.SetValue(InputBindingsProperty, inputBindings);
        }

        public static readonly DependencyProperty InputBindingsProperty = DependencyProperty.RegisterAttached("InputBindings", typeof(InputBindingCollection), typeof(UI),
         new FrameworkPropertyMetadata(new InputBindingCollection(), OnInputBindingsChanged));

        private static void OnInputBindingsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as UIElement;
            if (element == null) return;
            element.InputBindings.Clear();
            element.InputBindings.AddRange((InputBindingCollection)e.NewValue);
        } 

        #endregion

        #region EnableScrollbarAlways
        public static void EnableScrollbarAlways()
        {
            FrameworkElement.DataContextProperty.AddOwner(typeof(ScrollBar));
            ScrollBar.IsEnabledProperty.OverrideMetadata(typeof(ScrollBar), new UIPropertyMetadata(true, OnScrollViewerIsEnabledPropertyChanged, ScrollViewerForceEnabled));
        }

        private static void OnScrollViewerIsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {

        }
        private static object ScrollViewerForceEnabled(DependencyObject source, object value)
        {
            return true;
        } 
        #endregion
    }
}
