using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AoM.Viewer.Data;
using Savchin.Collection.Generic;
using Savchin.Core;

namespace AoM.Viewer.Controls
{
    /// <summary>
    /// Interaction logic for LocationControl.xaml
    /// </summary>
    public partial class LocationControl : UserControl
    {
        public LocationControl()
        {
            InitializeComponent();
            EnumHelper.GetValuesArray<LocationType>().Foreach(e => listLocationType.Items.Add(e));
            Enumerable.Range(1, 5).Foreach(e => listAct.Items.Add(e));
            Enumerable.Range(1, 20).Foreach(e => listPart.Items.Add(e));

        }

    }
}
