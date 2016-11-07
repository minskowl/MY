using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBox.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Nuclex.Game.States;
using Nuclex.UserInterface.Visuals.Flat;

namespace GameBox
{
    public class GameManager : GameBoxStateManager
    {
        private IGame _game = new NullGame();
        private GameBox _box;
        private FlatGuiVisualizer _visualizer;
        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        /// <param name="box">The box.</param>
        public GameManager(GameBox box)
            : base(box.Services)
        {
            _box = box;

        }
        /// <summary>
        /// Gives the game component a chance to initialize itself
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            _visualizer = (FlatGuiVisualizer)_box.GuiManager.Visualizer;
            _visualizer.RendererRepository.AddAssembly(typeof(IGame).Assembly);
        }
        /// <summary>
        /// Updates the active game state
        /// </summary>
        /// <param name="gameTime">Snapshot of the game's timing values</param>
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _box.Exit();
                return;
            }

            base.Update(gameTime);

        }

        /// <summary>
        /// News the game.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public void NewGame(Difficulty difficulty)
        {
            _game.NewGame(difficulty);
        }


        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void StartGame(IGame game)
        {
            _game = game;
            if (!_game.IsInitialized)
            {
                _game.Initialize();


                _visualizer.RendererRepository.AddAssembly(_game.GetType().Assembly);
                _visualizer.Graphics.LoadSkin(_box.Services, string.Format(@"Content\{0}Skin.xml", _game.Key));

                _game.GameScene.Screen.Desktop.Bounds = _box.GuiManager.Screen.Desktop.Bounds;
            }

            Switch(_game.GameScene);
            NewGame(_box.Difficulty);
            _box.ControlPanel.Title = string.Format("Game Box :: {0}", _game.Name);
            _box.ControlPanel.IsTopPanelVisible = true;
        }
    }
}
