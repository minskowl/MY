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

namespace EffectiveSoft.SilverlightDemo.Controls
{
	public delegate void Callback( ClockData clockData);

	public class ClockPresenter
	{
		public Callback Response;

		public ClockPresenter( IClockView clockView, IClockModel clockModel )
		{
			this.ClockView = clockView;
			this.ClockModel = clockModel;
			
			this.ClockView.StartClock += new EventHandler( ClockView_StartClock );
			this.ClockView.StopClock += new EventHandler( ClockView_StopClock );

			this.ClockModel.ClockChanged += new EventHandler<ClockDataEventArgs>( ClockModel_ClockChanged );
		}

		public IClockView ClockView
		{
			get;
			set;
		}
		public IClockModel ClockModel
		{
			get;
			set;
		}

		private void ClockView_StopClock( object sender, EventArgs e )
		{
			this.ClockModel.Stop();
		}

		private void ClockView_StartClock( object sender, EventArgs e )
		{
			this.ClockModel.Start();
		}

		private void ClockModel_ClockChanged( object sender, ClockDataEventArgs e )
		{
			Callback temp = this.Response;
			if ( temp != null )
				temp( e.ClockData );
		}
	}
}
