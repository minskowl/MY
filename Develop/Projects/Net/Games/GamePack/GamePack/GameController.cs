using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlipIt.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlipIt
{
    public enum Action
    {
        None = 0,
        Win,
        NewGame,
        Exit,

    }
    class GameController
    {
        private Scene _currentScene;
        private GameScene _gameScene;
        private WinScene _winScene;
        public bool ShowCustomCursor
        {
            get { return _winScene.Cursor.IsVisible; }
            set { _winScene.Cursor.IsVisible=_gameScene.Cursor.IsVisible= value; }
        }
        public GameController()
        {
            _winScene = new WinScene();
            _gameScene = new GameScene();
            _currentScene = _gameScene;
        }
        public void NewGame()
        {
            _gameScene.NewGame();
            _currentScene = _gameScene;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <returns></returns>
        public bool Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                return true;

            switch (_currentScene.Update(gameTime))
            {
                case Action.Win:
                    _currentScene = _winScene;
                    break;
                case Action.NewGame:
                    _currentScene = _gameScene;
                    _gameScene.NewGame();
                    break;
                case Action.Exit:
                    return true;

            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScene.Draw(spriteBatch);
        }
    }
}
