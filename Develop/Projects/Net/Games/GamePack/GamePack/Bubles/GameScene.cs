using System;
using GameBox.Core;
using Nuclex.UserInterface;

namespace GamePack.Bubles
{
    sealed class GameScene : Scene
    {
        private readonly FieldControl _field;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameScene"/> class.
        /// </summary>
        /// <param name="gameStateManager">Game state manager the game state belongs to</param>
        public GameScene(GameBoxStateManager gameStateManager)
            : base(gameStateManager)
        {
            _field = new FieldControl
                          {
                              Bounds = { Location = new UniVector(25, 25) }
                          };
            _field.EndGamge += ControlEndGamge;
            AddControl(_field);
        }



        /// <summary>
        /// News the game.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public void NewGame(Difficulty difficulty)
        {
            _field.NewGame(difficulty);
        }
        
        void ControlEndGamge(object sender, EventArgs e)
        {
            StateManager.ShowEndGame("End game, Score: " + _field.Score);
        }
    }
}
