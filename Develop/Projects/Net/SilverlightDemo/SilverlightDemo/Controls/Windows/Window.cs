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
	[TemplatePart(Name = "PART_Window", Type = typeof(Grid))]
	[TemplatePart(Name = "PART_CaptionBar", Type = typeof(Border))]
	[TemplatePart(Name = "PART_CaptionText", Type = typeof(TextBlock))]
	[TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
	[TemplatePart(Name = "PART_ScrollContent", Type = typeof(ScrollViewer))]
	[TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
	public class Window : ContentControl, IWindow
	{
		private FrameworkElement captionBar = null;
		private Grid window = null;
		private TextBlock captionText;
		//private Grid content = null;
		private ScrollViewer scrollcontent = null;
		private ContentPresenter contentpresenter = null;
		private double innerContentPresenterOffset = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class.
        /// </summary>
		public Window()
		{
			this.DefaultStyleKey = typeof(Window);

			// HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
			// VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

			this.LayoutUpdated += new EventHandler(Window_LayoutUpdated);
		}

		void Window_LayoutUpdated(object sender, EventArgs e)
		{
			// The layout is comepletely set, let's compute the content offset and fix its dimensions,
			// we need to do this only the first time because it will cause reevaluation of the size of the 
			// scrollviewer container too.
			// This is the offset that takes into account any element of the window
			if (innerContentPresenterOffset == -1)
			{
				// innerContentPresenterOffset = this.ActualWidth - contentpresenter.ActualWidth;
				// We cannot use the starting content presenter size to compute the offset cause it can be greater than
				// the actual window sie (due to the minvalue), so we compute it taking into account any horizontal offset
				// of the scrollviewer
				innerContentPresenterOffset = this.ActualWidth - (scrollcontent.ActualWidth - scrollcontent.Margin.Left
					- scrollcontent.Margin.Right - scrollcontent.Padding.Left - scrollcontent.Padding.Right
					- scrollcontent.BorderThickness.Left - scrollcontent.BorderThickness.Right);
				innerContentPresenterOffset = Math.Max(innerContentPresenterOffset, 0);

				SetContentPresenterSizeAndMinSize();
			}
		}

		/// <summary>
		/// Gets called once the template is applied
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			window = GetTemplateChild("PART_Window") as Grid;

			scrollcontent = GetTemplateChild("PART_ScrollContent") as ScrollViewer;
			scrollcontent.HorizontalScrollBarVisibility = HorizontalScrollBarVisibility;
			scrollcontent.VerticalScrollBarVisibility = VerticalScrollBarVisibility;

			contentpresenter = GetTemplateChild("PART_ContentPresenter") as ContentPresenter;

			captionBar = GetTemplateChild("PART_CaptionBar") as FrameworkElement;

			captionText = GetTemplateChild("PART_CaptionText") as TextBlock;
			captionText.Text = _Caption;

			Button closeButton = GetTemplateChild("PART_CloseButton") as Button;
			if (closeButton != null)
				closeButton.Click += new RoutedEventHandler(closeButton_Click);

			DefineDragEvents();
			DefineResizeEvents();

			Canvas.SetZIndex(this, currentZIndex);
		}

		#region HorizontalScrollBarVisibility

		/// <summary> 
		/// Gets or sets the HorizontalScrollBarVisibility possible Value of the ScrollBarVisibility object.
		/// </summary> 
		public ScrollBarVisibility HorizontalScrollBarVisibility
		{
			get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty); }
			set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
		}

		/// <summary> 
		/// Identifies the HorizontalScrollBarVisibility dependency property.
		/// </summary> 
		public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty =
						DependencyProperty.Register(
								"HorizontalScrollBarVisibility",
								typeof(ScrollBarVisibility),
								typeof(Window),
								new PropertyMetadata(OnHorizontalScrollBarVisibilityPropertyChanged));

		/// <summary>
		/// HorizontalScrollBarVisibilityProperty property changed handler. 
		/// </summary>
		/// <param name="d">Window that changed its HorizontalScrollBarVisibility.</param>
		/// <param name="e">DependencyPropertyChangedEventArgs.</param> 
		private static void OnHorizontalScrollBarVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Window _Window = d as Window;
			if (_Window != null)
			{
				if (_Window.scrollcontent != null)
					_Window.scrollcontent.HorizontalScrollBarVisibility = (ScrollBarVisibility)e.NewValue;
			}
		}
		#endregion HorizontalScrollBarVisibility

		#region VerticalScrollBarVisibility

		/// <summary> 
		/// Gets or sets the VerticalScrollBarVisibility possible Value of the ScrollBarVisibility object.
		/// </summary> 
		public ScrollBarVisibility VerticalScrollBarVisibility
		{
			get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty); }
			set { SetValue(VerticalScrollBarVisibilityProperty, value); }
		}

		/// <summary> 
		/// Identifies the VerticalScrollBarVisibility dependency property.
		/// </summary> 
		public static readonly DependencyProperty VerticalScrollBarVisibilityProperty =
						DependencyProperty.Register(
								"VerticalScrollBarVisibility",
								typeof(ScrollBarVisibility),
								typeof(Window),
								new PropertyMetadata(OnVerticalScrollBarVisibilityPropertyChanged));

		/// <summary>
		/// VerticalScrollBarVisibilityProperty property changed handler. 
		/// </summary>
		/// <param name="d">Window that changed its VerticalScrollBarVisibility.</param>
		/// <param name="e">DependencyPropertyChangedEventArgs.</param> 
		private static void OnVerticalScrollBarVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Window _Window = d as Window;
			if (_Window != null)
			{
				if (_Window.scrollcontent != null)
					_Window.scrollcontent.VerticalScrollBarVisibility = (ScrollBarVisibility)e.NewValue;
			}
		}
		#endregion VerticalScrollBarVisibility

		public event EventHandler Closed;

		public void Close()
		{
			this.Visibility = Visibility.Collapsed;
			// fire the closed event
			if (Closed != null)
				Closed(this, EventArgs.Empty);
		}

		void closeButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		#region Properties

		/// <summary>
		/// Sets whether dragging is enable
		/// </summary>
		public bool DraggingEnabled { get { return _DraggingEnabled; } set { _DraggingEnabled = value; } }
		private bool _DraggingEnabled = true;

		/// <summary>
		/// this property accesses and set and internal member of the template
		/// used top draw the title of the window, it may happen that you want to get or set
		/// this property before the template is actually loaded, so you have to
		/// bufferize the value and apply it later in the OnApplyTemplate function
		/// </summary>
		public string Caption
		{
			get { return (captionText != null) ? captionText.Text : _Caption; }
			set
			{
				if (captionText != null)
					captionText.Text = value;
				else
					_Caption = value;
			}
		}
		private string _Caption = "";

		#endregion

		#region Dragging Functions

		private bool isDragging = false;
		private Point initialWindowLocation;
		private Point initialDragPoint;
		private static int currentZIndex = 1;

		private void DefineDragEvents()
		{
			if (captionBar != null)
			{
				captionBar.MouseLeftButtonDown +=
					 new MouseButtonEventHandler(captionBar_MouseLeftButtonDown);
				captionBar.MouseMove +=
					new MouseEventHandler(captionBar_MouseMove);
				captionBar.MouseLeftButtonUp +=
					 new MouseButtonEventHandler(captionBar_MouseLeftButtonUp);
			}
		}

		/// <summary>
		/// Fires when the left mouse button goes down on the caption bar
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void captionBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (DraggingEnabled)
			{
				// Bring the panel to the front
				Canvas.SetZIndex(this, currentZIndex++);
				// Capture the mouse
				((FrameworkElement)sender).CaptureMouse();
				// Store the start position
				this.initialDragPoint = e.GetPosition(this.Parent as UIElement);
				this.initialWindowLocation.X = Canvas.GetLeft(this);
				this.initialWindowLocation.Y = Canvas.GetTop(this);
				// Set dragging to true
				this.isDragging = true;
			}
		}

		/// <summary>
		/// Fires when the left mouse button goes up on the caption bar
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void captionBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (DraggingEnabled)
			{
				// Release the mouse
				((FrameworkElement)sender).ReleaseMouseCapture();
				// Set dragging to false
				isDragging = false;
			}
		}

		/// <summary>
		/// Fires when the mouse moves on the caption bar
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void captionBar_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.isDragging)
			{
				Point position = e.GetPosition(this.Parent as UIElement);
				Canvas c = this.Parent as Canvas;
				// Move the panel
				double X = initialWindowLocation.X + position.X - initialDragPoint.X;
				if ((X >= 0) && (X + captionBar.ActualWidth <= c.ActualWidth))
					Canvas.SetLeft(this, X);
				double Y = initialWindowLocation.Y + position.Y - initialDragPoint.Y;
				if ((Y >= 0) && (Y + captionBar.ActualHeight <= c.ActualHeight))
					Canvas.SetTop(this, Y);
			}
		}

		#endregion

		#region Resize Functions

		private const int HotSpotWidth = 3;
		/// <summary>
		/// states if a resize operation is in progress
		/// </summary>
		private bool isResizing = false;
		private ResizeAnchor resizeAnchor = ResizeAnchor.None;
		private Point initialResizePoint;
		private Size initialWindowSize;
		private const int MinWindowWidth = 60;

		/// <summary>
		/// defines where the user mouse is positioned inside the control
		/// </summary>
		private enum ResizeAnchor
		{
			None,
			Left,
			TopLeft,
			Top,
			TopRight,
			Right,
			BottomRight,
			Bottom,
			BottomLeft
		}

		/// <summary>
		/// enable/disable support for resize this window
		/// </summary>
		public bool ResizeEnabled { get { return _ResizeEnabled; } set { _ResizeEnabled = value; } }
		private bool _ResizeEnabled = true;

		/// <summary>
		/// returns true if the window can be resized
		/// </summary>
		private bool CanResize
		{
			get { return ((ResizeEnabled) && (resizeAnchor != ResizeAnchor.None)); }
		}

		private void DefineResizeEvents()
		{
			if (window != null)
			{
				window.MouseLeftButtonDown += new MouseButtonEventHandler(window_MouseLeftButtonDown);
				window.MouseMove += new MouseEventHandler(window_MouseMove);
				window.MouseLeftButtonUp += new MouseButtonEventHandler(window_MouseLeftButtonUp);
			}
		}

		void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (CanResize)
			{
				// Capture the mouse
				((FrameworkElement)sender).CaptureMouse();
				// Store the start position
				this.initialResizePoint = e.GetPosition(this.Parent as UIElement);
				initialWindowSize.Width = (!double.IsNaN(this.Width) ? this.Width : this.ActualWidth);
				initialWindowSize.Height = (!double.IsNaN(this.Height) ? this.Height : this.ActualHeight);
				this.initialWindowLocation.X = Canvas.GetLeft(this);
				this.initialWindowLocation.Y = Canvas.GetTop(this);
				// Set resizing to true
				this.isResizing = true;
			}
		}

		void window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if ((ResizeEnabled) && (isResizing))
			{
				// Release the mouse
				((FrameworkElement)sender).ReleaseMouseCapture();
				// Set resizing to false
				isResizing = false;
			}
		}

		void window_MouseMove(object sender, MouseEventArgs e)
		{
			if (ResizeEnabled)
			{
				if (!isResizing)
				{
					Point pos = e.GetPosition(window);

					if ((pos.Y <= HotSpotWidth) && (pos.X <= HotSpotWidth))
					{
						window.Cursor = Cursors.Hand;
						resizeAnchor = ResizeAnchor.TopLeft;
					}
					else if ((pos.Y <= HotSpotWidth) && (pos.X >= (window.ActualWidth - HotSpotWidth)))
					{
						window.Cursor = Cursors.Hand;
						resizeAnchor = ResizeAnchor.TopRight;
					}
					else if (pos.Y <= HotSpotWidth)
					{
						window.Cursor = Cursors.SizeNS;
						resizeAnchor = ResizeAnchor.Top;
					}
					else if ((pos.Y >= (window.ActualHeight - HotSpotWidth)) && (pos.X <= HotSpotWidth))
					{
						window.Cursor = Cursors.Hand;
						resizeAnchor = ResizeAnchor.BottomLeft;
					}
					else if ((pos.Y >= (window.ActualHeight - HotSpotWidth)) && (pos.X >= (window.ActualWidth - HotSpotWidth)))
					{
						window.Cursor = Cursors.Hand;
						resizeAnchor = ResizeAnchor.BottomRight;
					}
					else if (pos.Y >= (window.ActualHeight - HotSpotWidth))
					{
						window.Cursor = Cursors.SizeNS;
						resizeAnchor = ResizeAnchor.Bottom;
					}
					else if (pos.X <= HotSpotWidth)
					{
						window.Cursor = Cursors.SizeWE;
						resizeAnchor = ResizeAnchor.Left;
					}
					else if (pos.X >= (window.ActualWidth - HotSpotWidth))
					{
						window.Cursor = Cursors.SizeWE;
						resizeAnchor = ResizeAnchor.Right;
					}
					else
					{
						window.Cursor = null;
						resizeAnchor = ResizeAnchor.None;
					}
				}
				else
				{
					Point position = e.GetPosition(this.Parent as UIElement);

					double deltaX = position.X - initialResizePoint.X;
					double deltaY = position.Y - initialResizePoint.Y;

					switch (resizeAnchor)
					{
						case ResizeAnchor.Left:
							ResizeLeft(deltaX);
							break;
						case ResizeAnchor.Top:
							ResizeTop(deltaY);
							break;
						case ResizeAnchor.Right:
							ResizeRight(deltaX);
							break;
						case ResizeAnchor.Bottom:
							ResizeBottom(deltaY);
							break;
						case ResizeAnchor.TopLeft:
							ResizeLeft(deltaX);
							ResizeTop(deltaY);
							break;
						case ResizeAnchor.TopRight:
							ResizeRight(deltaX);
							ResizeTop(deltaY);
							break;
						case ResizeAnchor.BottomLeft:
							ResizeLeft(deltaX);
							ResizeBottom(deltaY);
							break;
						case ResizeAnchor.BottomRight:
							ResizeRight(deltaX);
							ResizeBottom(deltaY);
							break;
					}

					// let's resize the contentpresenter to fix the resize bug of controls inside a scrollviewer with
					// horizontal scrollbar visible
					contentpresenter.Width = this.Width - innerContentPresenterOffset;
				}
			}
		}

		private void ResizeBottom(double deltaY)
		{
			this.Height = Math.Max(initialWindowSize.Height + deltaY, captionBar.ActualHeight);
		}

		private void ResizeRight(double deltaX)
		{
			this.Width = Math.Max(initialWindowSize.Width + deltaX, MinWindowWidth);
		}

		private void ResizeTop(double deltaY)
		{
			// fix to avoid to move the window when we reached the minimal width resizing from the top
			double MaxY = initialWindowLocation.Y + this.initialWindowSize.Height - captionBar.ActualHeight;
			Canvas.SetTop(this, Math.Min(initialResizePoint.Y + deltaY, MaxY));
			this.Height = Math.Max(initialWindowSize.Height - deltaY, captionBar.ActualHeight);
		}

		private void ResizeLeft(double deltaX)
		{
			// fix to avoid to move the window when we reached the minimal width resizing from the left
			double MaxX = initialWindowLocation.X + this.initialWindowSize.Width - MinWindowWidth;
			Canvas.SetLeft(this, Math.Min(initialResizePoint.X + deltaX, MaxX));
			this.Width = Math.Max(initialWindowSize.Width - deltaX, MinWindowWidth);
		}

		/// <summary>
		/// we set the width of the content presenter to avoid unwanted resize of the window
		/// due to exapnsion of the surfaces of the containers cause they are resized proportionally with the content
		/// if no dimensions are setted for them
		/// </summary>
		private void SetContentPresenterSizeAndMinSize()
		{
			contentpresenter.Width = this.ActualWidth - innerContentPresenterOffset;
			contentpresenter.MinWidth = (contentpresenter.Content as FrameworkElement).MinWidth;
			contentpresenter.MinHeight = (contentpresenter.Content as FrameworkElement).MinHeight;
		}

		#endregion
	}
}
