using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Savchin.Bubbles.Core;

namespace Savchin.Bubbles.Controls
{
    /// <summary>
    /// Interaction logic for StatisticsColorPanel.xaml
    /// </summary>
    public partial class StatisticsColorPanel : UserControl
    {
        Dictionary<BubbleColor, BubleLabel> labels = new Dictionary<BubbleColor, BubleLabel>();
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsColorPanel"/> class.
        /// </summary>
        public StatisticsColorPanel()
        {
            InitializeComponent();


            //for (var i = 1; i <= 5; i++)
            //{
            //    var color = (BubleColor)i;
            //    var control = new BubleLabel { Color = color };
            //    layout.Children.Add(control);
            //    labels.Add(color, control);
            //}
        }

        public void Compute(FieldControl field)
        {
            grid.ItemsSource = (from row in field.Bubbles
                       group row by row.Color into g
                       select new GridRow
                       {
                           Color = g.Key,
                                 Count = g.Count()
                       }).ToArray();
            //field
            //foreach (BubleColor color in labels.Keys)
            //{
            //    labels[color].Text = field.GetCount(color).ToString();
            //}
        }

        private class GridRow
        {
            public BubbleColor Color { get; set; }
            public int Count { get; set; }
            public int PosibleScore
            {
                get { return Count*(Count - 1); }
            }
        }
    }
}
