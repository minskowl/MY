using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Savchin.Wpf.Controls
{
    public class ButtonEx
    {
        // Boilerplate code to register attached property "bool? DialogResult"
        public static bool? GetDialogResult(DependencyObject obj) { return (bool?)obj.GetValue(DialogResultProperty); }
        public static void SetDialogResult(DependencyObject obj, bool? value) { obj.SetValue(DialogResultProperty, value); }
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached("DialogResult", typeof(bool?), typeof(ButtonEx), new UIPropertyMetadata(OnDialogResultChanged));

        private static void OnDialogResultChanged(DependencyObject obj, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            // Implementation of DialogResult functionality
            var button = obj as Button;
            if (button == null)
                throw new InvalidOperationException(
                  "Can only use ButtonHelper.DialogResult on a Button control");
            button.Click += (sender, e2) =>
            {
                Window.GetWindow(button).DialogResult = GetDialogResult(button);
            };
        }
    }
}
