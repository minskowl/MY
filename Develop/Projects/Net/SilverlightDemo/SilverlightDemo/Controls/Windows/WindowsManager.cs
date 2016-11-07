using System;
using System.Windows;
using System.Windows.Controls;

namespace EffectiveSoft.SilverlightDemo.Controls.Windows
{
	/// <summary>
	/// Class that handles the creation and destruction of dynamic windows
	/// </summary>
	public class WindowsManager : IWindowsManager
	{
		private Canvas _canvas = null;

		/// <summary>
		/// creates the manager and stores the canvas to which attach the windows
		/// </summary>
		/// <param name="surface"></param>
		public WindowsManager(Canvas surface)
		{
			_canvas = surface;
		}

		public IWindow ShowWindow(FrameworkElement content, string caption, Point location)
		{
			Window w = new Window();
			w.Caption = caption;
			w.Content = content;
			w.Closed += new EventHandler(w_Closed);
			Canvas.SetLeft(w, location.X);
			Canvas.SetTop(w, location.Y);
			_canvas.Children.Add(w);
			return w;
		}

		/// <summary>
		/// show a window attaching it to the canvas
		/// </summary>
		/// <param name="w"></param>
		/// <param name="location"></param>
		public void ShowWindow(IWindow w, Point location)
		{
			Window wtmp = w as Window;
			wtmp.Closed += new EventHandler(w_Closed);
			Canvas.SetLeft(wtmp, location.X);
			Canvas.SetTop(wtmp, location.Y);
			_canvas.Children.Add(wtmp);
		}

		void w_Closed(object sender, EventArgs e)
		{
			// remove the object from the childern colelction and dispose it dispose the object 
			Window w = (Window)sender;
			_canvas.Children.Remove(w);
		}
	}
}
