using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Savchin.Bubbles.Controls;

namespace Playback
{
    /// <summary>
    /// Interaction logic for SceneControl.xaml
    /// </summary>
    public partial class SceneControl : UserControl
    {
        private double delim;
        private double delimSize;
        private static int fieldMax = 25;



        public SceneControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Show(PlanetWars game)
        {
            delim = Math.Min(ActualWidth, ActualHeight) / fieldMax;
            delimSize = delim / 3;

            controlScene.Children.Clear();


            foreach (var planet in game.Planets())
            {
                ShowPlanet(planet);
            }

            foreach (var fleet in game.Fleets())
            {
                ShowFleet(game, fleet);
            }
        }

        private void ShowFleet(PlanetWars game, Fleet fleet)
        {
            var control = new FleetControl();
            control.Owner = fleet.Owner();
            control.Text = fleet.NumShips().ToString();

            var source = game.GetPlanet(fleet.SourcePlanet());
            var dest = game.GetPlanet(fleet.DestinationPlanet());
            var steps = fleet.TotalTripLength();
            var remaing = fleet.TurnsRemaining();
            var x = source.X + (dest.X - source.X) * (steps-remaing) / steps;
            var y = source.Y + (dest.Y - source.Y) * (steps-remaing) / steps;

            ShowControl(control, x, y);
        }

        private void ShowPlanet(Planet planet)
        {
            var planetControl = new PlanetControl();
            planetControl.Height = planetControl.Width = 20 + (planet.GrowthRate * delimSize);
            planetControl.Owner = planet.Owner;
            planetControl.Text = planet.NumShips.ToString();
            ShowControl(planetControl, planet.X, planet.Y);



        }
        private void ShowControl(UserControl control, double x, double y)
        {
            controlScene.Children.Add(control);
            Canvas.SetLeft(control, x * delim);
            Canvas.SetTop(control, (fieldMax -y ) * delim);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
