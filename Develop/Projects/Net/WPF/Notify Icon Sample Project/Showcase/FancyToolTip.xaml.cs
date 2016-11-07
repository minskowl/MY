﻿using System.Windows;

namespace Samples
{
	/// <summary>
	/// Interaction logic for FancyToolTip.xaml
	/// </summary>
	public partial class FancyToolTip
	{
	  #region InfoText dependency property

	  /// <summary>
	  /// The tooltip details.
	  /// </summary>
	  public static readonly DependencyProperty InfoTextProperty =
	      DependencyProperty.Register("InfoText",
	                                  typeof (string),
	                                  typeof (FancyToolTip),
	                                  new FrameworkPropertyMetadata(""));

	  /// <summary>
	  /// A property wrapper for the <see cref="InfoTextProperty"/>
	  /// dependency property:<br/>
	  /// The tooltip details.
	  /// </summary>
	  public string InfoText
	  {
	    get { return (string) GetValue(InfoTextProperty); }
	    set { SetValue(InfoTextProperty, value); }
	  }

	  #endregion



		public FancyToolTip()
		{
			this.InitializeComponent();
		}

	}
}