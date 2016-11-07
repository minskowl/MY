using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBox.Core;
using Nuclex.Game.States;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;

namespace GameBox
{
    class SelectGameScene : Scene
    {
        private List<IGame> _games;
        private const float Left = 50;
        private const float Top = 50;
        private const float Width = 200;
        private GameManager _gameManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectGameScene"/> class.
        /// </summary>
        /// <param name="games">The games.</param>
        /// <param name="gameStateManager">Game state manager the game state belongs to</param>
        public SelectGameScene(List<IGame> games, GameManager gameManager)
            : base(gameManager)
        {
            _games = games;
            _gameManager = gameManager;

            var label = new LabelControl
                                    {
                                        Text = "Select game",
                                    };
            label.Bounds.Location.X = Left;
            label.Bounds.Location.Y = Top;
            label.Frame = "labelTitle";

            Screen.Desktop.Children.Add(label);

            var gameList = new ListControl();
            gameList.SelectionMode = ListSelectionMode.Single;
            gameList.Bounds.Location.X = Left;
            gameList.Bounds.Location.Y = Top + 25;
            gameList.Bounds.Size.Y = 100;
            gameList.Bounds.Size.X = Width;

            foreach (var game in _games)
            {
                gameList.Items.Add(game.Name);
            }

            Screen.Desktop.Children.Add(gameList);

            var button = new ButtonControl();
            button.Text = "Play";
            button.Bounds.Location.X = Left;
            button.Bounds.Location.Y = Top + 150;
            button.Bounds.Size.X = Width;
            button.Bounds.Size.Y = 30;
            button.Pressed += (sender, arguments) =>
                                  {
                                      if (gameList.SelectedItems.Count > 0)
                                      {
                                          var name = gameList.Items[gameList.SelectedItems[0]];
                                          var game = _games.FirstOrDefault(e => e.Name == name);
                                          if (game != null)
                                          {
                                              _gameManager.StartGame(game);
                                              
                                          }
                                      }

                                  };
            Screen.Desktop.Children.Add(button);
        }
    }
}
