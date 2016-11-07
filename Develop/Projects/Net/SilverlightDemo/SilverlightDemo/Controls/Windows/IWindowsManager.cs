using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EffectiveSoft.SilverlightDemo.Controls.Windows
{
	public interface IWindowsManager
	{
		IWindow ShowWindow(FrameworkElement content, string caption, Point location);

		void ShowWindow(IWindow w, Point location);
	}
}
