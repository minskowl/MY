namespace GameBox.Core
{
    /// <summary>
    /// NullGame
    /// </summary>
    public class NullGame : IGame
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get { return "Null"; }
        }


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return "<None>"; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the game scene.
        /// </summary>
        /// <value>The game scene.</value>
        public Scene GameScene
        {
            get { return null; }
        }


        /// <summary>
        /// News the game.
        /// </summary>
        /// <param name="difficulty"></param>
        public void NewGame(Difficulty difficulty)
        {
            
        }
        /// <summary>
        /// Called when the component should be initialized. This method can be used for tasks like querying for services the component needs and setting up non-graphics resources.
        /// </summary>
        public void Initialize()
        {

        }
    }
}
