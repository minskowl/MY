using Microsoft.Xna.Framework;

namespace GameBox.Core
{
    public enum Difficulty
    {
        Easy = 1,
        Normal = 5,
        Hard = 10
    }

    public interface IGame : IGameComponent
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        string Key { get; }



        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        bool IsInitialized { get; }
        /// <summary>
        /// Gets the game scene.
        /// </summary>
        /// <value>The game scene.</value>
        Scene GameScene { get; }

        /// <summary>
        /// News the game.
        /// </summary>
        void NewGame(Difficulty difficulty);
    }
}
