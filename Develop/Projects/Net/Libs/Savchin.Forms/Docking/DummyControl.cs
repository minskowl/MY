using System;
using System.Windows.Forms;

namespace Savchin.Forms.Docking
{
	internal class DummyControl : Control
	{
		public DummyControl()
		{
			SetStyle(ControlStyles.Selectable, false);
		}
	}
}
