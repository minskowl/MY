using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameBox.Core;
using Nuclex.Game.States;

namespace GamePack.Bubles
{
    public class BubblesGame : IGame
    {
        #region Properties
        private GameScene _gameScene;
        private readonly GameBoxStateManager _manager;
        private bool _isInitialized;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return "Bubbles"; }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get { return Name; }
        }

        public bool IsInitialized
        {
            get { return _isInitialized; }
        }


        /// <summary>
        /// Gets the game scene.
        /// </summary>
        /// <value>The game scene.</value>
        public Scene GameScene
        {
            get { return _gameScene; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BubblesGame"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public BubblesGame(GameBoxStateManager manager)
        {
            _manager = manager;
        }

        public void NewGame(Difficulty difficulty)
        {
            _gameScene.NewGame(difficulty);
            _manager.Switch(_gameScene);
        }

        /// <summary>
        /// Called when the component should be initialized. This method can be used for tasks like querying for services the component needs and setting up non-graphics resources.
        /// </summary>
        public void Initialize()
        {
            _isInitialized = true;
            _gameScene = new GameScene(_manager);
        }

    }
}
