using System;
using BotvaSpider.Core;

namespace BotvaSpider.Fighting
{
    /// <summary>
    /// FightResultEventArgs
    /// </summary>
    public class FightResultEventArgs : EventArgs
    {
        private readonly FightResult result;
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public FightResult Result
        {
            get { return result; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FightResultEventArgs"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public FightResultEventArgs(FightResult result)
        {
            this.result = result;
        }


    }
}