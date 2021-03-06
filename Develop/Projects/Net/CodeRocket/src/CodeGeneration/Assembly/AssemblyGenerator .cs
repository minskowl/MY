using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace Savchin.CodeGeneration
{

    public class AssemblyGenerator : GeneratorBase<AssemblyGenerateProject>
    {

        /// <summary>
        /// Generates the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="generations">The generations.</param>
        public void Generate(TreeNode node, GenerateMode mode, List<Generation> generations)
        {
            context.Put("node", node);
            List<Generation> correctGenerations = new List<Generation>(generations);
            string nodeTypeName = node.Tag.GetType().Name;
            foreach (Generation generation in generations)
            {
                if (nodeTypeName.IndexOf(generation.ObjectType,StringComparison.InvariantCultureIgnoreCase ) == -1)
                    correctGenerations.Remove(generation);
            }
            Generate(correctGenerations, mode); 
        }




        /// <summary>
        /// Gets the name of the destination file.
        /// </summary>
        /// <param name="generation">The generation.</param>
        /// <returns></returns>
        protected override string GetDestinationFileName(Generation generation)
        {
            return string.Format(generation.DestinationFile, ((TreeNode)context.Get("node")).Text);
 
        }
    }

}
