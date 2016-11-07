using Nuclex.Game.States;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;

namespace GameBox.Core
{
    /// <summary>
    /// EndGameScene
    /// </summary>
    class EndGameScene : Scene
    {
        private LabelControl _labelControl;
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return _labelControl.Text; }
            set { _labelControl.Text = value; }

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EndGameScene"/> class.
        /// </summary>
        /// <param name="gameStateManager">Game state manager the game state belongs to</param>
        public EndGameScene(GameBoxStateManager gameStateManager)
            : base(gameStateManager)
        {
            _labelControl = new LabelControl("You win!!!") {Bounds = {Location = new UniVector(200, 200)}};
            this.Screen.Desktop.Children.Add(_labelControl);
           
        }
    }
}
