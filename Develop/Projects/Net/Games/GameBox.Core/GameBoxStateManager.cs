using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Nuclex.Game.States;

namespace GameBox.Core
{
    /// <summary>
    /// GameBoxStateManager
    /// </summary>
   public  class GameBoxStateManager : GameStateManager
   {
       private EndGameScene _endGameScene;
       /// <summary>
       /// Initializes a new instance of the <see cref="GameBoxStateManager"/> class.
       /// </summary>
       /// <param name="gameServices">Services container the game state manager will add itself to</param>
       public GameBoxStateManager(GameServiceContainer gameServices) : base(gameServices)
       {

       }

       /// <summary>
       /// Gives the game component a chance to initialize itself
       /// </summary>
       public override void Initialize()
       {
           base.Initialize();
           _endGameScene= new EndGameScene(this);
       }

       /// <summary>
       /// Shows the end game.
       /// </summary>
       /// <param name="text">The text.</param>
       public void ShowEndGame(string text)
       {
           _endGameScene.Text = text;
           Switch(_endGameScene);
       }
   }
}
