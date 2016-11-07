using Microsoft.Xna.Framework;
using Nuclex.Game.States;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;

namespace GameBox.Core
{
    public class Scene : GameState
    { 
        private readonly IGuiService _service;


        /// <summary>
        /// Gets or sets the screen.
        /// </summary>
        /// <value>The screen.</value>
        public Screen Screen { get; private set; }

        /// <summary>
        /// Gets or sets the state manager.
        /// </summary>
        /// <value>The state manager.</value>
        public GameBoxStateManager StateManager { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        /// <param name="gameStateManager">Game state manager the game state belongs to</param>
        public Scene(GameBoxStateManager gameStateManager)
            : base(gameStateManager)
        {
            StateManager = gameStateManager;
            Screen = new Screen();
            _service = (IGuiService)gameStateManager.GameServices.GetService(typeof(IGuiService));
        }

        /// <summary>
        /// Called when the game state has been entered
        /// </summary>
        protected override void OnEntered()
        {
            GameStateManager.InputCapturer.InputReceiver = Screen;
            base.OnEntered();
        }
        /// <summary>
        /// This is called when the game state should draw itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            _service.Visualizer.Draw(Screen);
        }

        /// <summary>
        /// Adds the control.
        /// </summary>
        /// <param name="control">The control.</param>
        public virtual void AddControl(Control control)
        {
            Screen.Desktop.Children.Add(control);
        }
    }
}
