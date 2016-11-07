using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bashni.Game
{
    public interface ISolutionBuilder : IDisposable 
    {

        int VariantsCount { get; }
        bool IsBusy { get; }

        /// <summary>
        /// Builds the specified step.
        /// </summary>
        /// <param name="step">The step.</param>
        void Build(Step step);

        /// <summary>
        /// Cancels the async.
        /// </summary>
        void CancelAsync();
        /// <summary>
        /// Builds the specified steps.
        /// </summary>
        /// <param name="steps">The steps.</param>
        void Build(Step[] steps);

        /// <summary>
        /// Builds the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        void Build(Session game);

        event RunWorkerCompletedEventHandler RunWorkerCompleted;
    }
}