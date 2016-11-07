using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.CodeGeneration.Common
{
    public class GenerationEventArgs : EventArgs
    {
        private readonly Generation generation;

        public GenerationEventArgs(Generation generation)
        {
            this.generation = generation;
        }

        /// <summary>
        /// Gets the generation.
        /// </summary>
        /// <value>The generation.</value>
        public Generation Generation
        {
            get { return generation; }
        }
    }
}
