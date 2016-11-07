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
	public interface IWindow
	{
		event EventHandler Closed;

		/// <summary>
		/// close the window
		/// </summary>
		void Close();

		/// <summary>
		/// set/get the caption of the window
		/// </summary>
		string Caption { get; set; }

		/// <summary>
		/// enable/disable dragging
		/// </summary>
		bool DraggingEnabled { get; set; }

		/// <summary>
		/// enable/disable resize
		/// </summary>
		bool ResizeEnabled { get; set; }

		/// <summary>
		/// enable or disable the automatic horizontal scrollbar
		/// it's set on auto by default
		/// </summary>
		ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }

		/// <summary>
		/// enable or disable the automatic vertical scrollbar
		/// it's set on auto by default
		/// </summary>
		ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
	}
}