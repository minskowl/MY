using System;
using GameBox.Core;
using Savchin.Utils;

namespace GamePack.FreeIt
{
    class GameScene : Scene
    {

        #region Data
        private static readonly int[][] easyField = new[]
                                               {
                                                   new[] {2, 2, 2, 2},
                                                   new[] {2, 2, 2, 2},
                                                   new[] {2, 2, 2, 2},
                                                   new[] {2, 2, 2, 2}
                                               };
        private static readonly int[][] hardField = new[]
                                               {
                                                   new[] {0, 2, 2, 2, 2, 0},
                                                   new[] {2, 2, 2, 2, 2, 2},
                                                   new[] {2, 2, 2, 2, 2, 2},
                                                   new[] {0, 2, 2, 2, 2, 0}
                                               };

        private static readonly int[][] initField = new[]
                                                        {
                                                            new[] {2, 2, 2, 2},
                                                            new[] {2, 2, 1, 2},
                                                            new[] {2, 1, 1, 1},
                                                            new[] {2, 2, 1, 2}
                                                        }; 
        #endregion

        /// <summary>
        /// Gets a value indicating whether this instance is win.
        /// </summary>
        /// <value><c>true</c> if this instance is win; otherwise, <c>false</c>.</value>
        public bool IsWin
        {
            get { return _field.IsWin; }
        }

        private Field _field;
        /// <summary>
        /// Occurs when [game wined].
        /// </summary>
        public event EventHandler GameWined;




        /// <summary>
        /// Initializes a new instance of the <see cref="GameScene"/> class.
        /// </summary>
        /// <param name="gameStateManager">Game state manager the game state belongs to</param>
        public GameScene(GameBoxStateManager gameStateManager)
            : base(gameStateManager)
        {
            _field = new Field(Screen);
            _field.Position.X = 50;
            _field.Position.Y = 50;

            _field.Initialize(initField);
        }

        /// <summary>
        /// News the game.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public void NewGame(Difficulty difficulty)
        {
            _field.Initialize(difficulty == Difficulty.Easy ? easyField : hardField);


            var steps = Randomizer.GetIntegerBetween((int)difficulty * 2, (int)difficulty*4);
            for (int i = 0; i < steps; i++)
            {
                var row = Randomizer.GetIntegerBetween(0, _field.Rows);
                var column = Randomizer.GetIntegerBetween(0, _field.Columns);
                _field.ToggleTile(row, column);
            }


        }

        /// <summary>
        /// Allows the game state to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsWin)
            {
                InvokeGameWined(EventArgs.Empty);
            }
        }


        private void InvokeGameWined(EventArgs e)
        {
            EventHandler handler = GameWined;
            if (handler != null) handler(this, e);
        }
    }
}
