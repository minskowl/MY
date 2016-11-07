using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
namespace Savchin.Wpf.Core
{
    public static class d
    {
        static bool? _inDesignMode;

        /// <summary>
        /// Indicates whether or not the framework is in design-time mode. (Caliburn.Micro implementation)
        /// </summary>
        private static bool InDesignMode
        {
            get
            {
                if (_inDesignMode == null)
                {
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    _inDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;

                    if (!_inDesignMode.GetValueOrDefault(false) && Process.GetCurrentProcess().ProcessName.StartsWith("devenv", StringComparison.Ordinal))
                        _inDesignMode = true;
                }

                return _inDesignMode.GetValueOrDefault(false);
            }
        }
        public static DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(d), new PropertyMetadata(BackgroundChanged));
        public static Brush GetBackground(DependencyObject dependencyObject)
        {
            return (Brush)dependencyObject.GetValue(BackgroundProperty);
        }
        public static void SetBackground(DependencyObject dependencyObject, Brush value)
        {
            dependencyObject.SetValue(BackgroundProperty, value);
        }
        private static void BackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!InDesignMode)
                return;

            d.GetType().GetProperty("Background").SetValue(d, e.NewValue, null);
        }
    }
}
