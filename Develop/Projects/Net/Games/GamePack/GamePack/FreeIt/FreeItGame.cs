using System;
using GameBox.Core;

namespace GamePack.FreeIt
{

    /// <summary>
    /// FlipItGame
    /// </summary>
    public class FreeItGame : IGame
    {

        #region Properties
        private readonly GameScene _gameScene;

        private readonly GameBoxStateManager _manager;


        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get { return "FreeIt"; }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return "Free It"; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized { get; private set; }

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
        /// Initializes a new instance of the <see cref="FreeItGame"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FreeItGame(GameBoxStateManager manager)
        {
            _manager = manager;
            _gameScene = new GameScene(manager);
            _gameScene.GameWined += _gameScene_GameWined;

        }

        void _gameScene_GameWined(object sender, EventArgs e)
        {
            _manager.ShowEndGame("You win");
        }




        /// <summary>
        /// News the game.
        /// </summary>
        /// <param name="difficulty"></param>
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
            IsInitialized = true;
        }
    }
}
