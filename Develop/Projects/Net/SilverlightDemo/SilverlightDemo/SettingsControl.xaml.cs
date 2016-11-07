using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EffectiveSoft.SilverlightDemo.Controls.Windows;
using EffectiveSoft.SilverlightDemo.Core;
using Visifire.Charts;
using Visifire.Commons;

namespace EffectiveSoft.SilverlightDemo
{
    public partial class SettingsControl : UserControl
    {
        private string[] chartColorSets = new[]
                                              {
                                                  "Visifire1",
                                                  "Visifire2",
                                                  "VisiRed",
                                                  "VisiBlue",
                                                  "VisiGreen",
                                                  "VisiViolet",
                                                  "VisiGray",
                                                  "VisiAqua",
                                                  "VisiOrange",
                                                  "DarkShades",
                                                  "Caravan",
                                                  "Picasso",
                                                  "DullShades",
                                                  "SandyShades"
                                              };


        public IEnumerable<Chart> Charts { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsControl"/> class.
        /// </summary>
        public SettingsControl()
        {
            InitializeComponent();

            foreach (var colorSet in chartColorSets)
            {
                comboChartColorsets.Items.Add(colorSet);
            }
            comboChartColorsets.SelectedItem = UIFactory.ColorSet.ToString();

            check3DView.IsChecked = UIFactory.View3D;
            checkBevel.IsChecked = UIFactory.Bevel;
            checkAnimation.IsChecked = UIFactory.AnimationEnabled;
            checkShadow.IsChecked = UIFactory.ShadowEnabled;
            checkFullscreen.IsChecked = UIFactory.Fullscreen;


        }
        private void ApplySettings()
        {
            UIFactory.View3D = check3DView.IsChecked.Value;
            UIFactory.Bevel = checkBevel.IsChecked.Value;
            UIFactory.AnimationEnabled = checkAnimation.IsChecked.Value;
            UIFactory.ShadowEnabled = checkShadow.IsChecked.Value;
            UIFactory.Fullscreen = checkFullscreen.IsChecked.Value;

            var colorset = (string)comboChartColorsets.SelectedItem;
            if (!string.IsNullOrEmpty(colorset))
            {
                UIFactory.ColorSet = (ColorSetNames)Enum.Parse(typeof(ColorSetNames), colorset, true);
            }

            foreach (Chart chart in Charts)
            {
                UIFactory.ChartSetup(chart);
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        private void Close()
        {
            ((Window)Parent).Close();
        }

        #region Button Handlers
        /// <summary>
        /// Handles the Click event of the ButtonApply control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            ApplySettings();
        }


        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Handles the Click event of the ButtonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            ApplySettings();
            Close();
        } 
        #endregion
    }
}
