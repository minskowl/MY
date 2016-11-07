using EffectiveSoft.IES.DataLayer;
using EffectiveSoft.IES.IntellexerAPI;

namespace IES.PerformanceTester.IntelexerSDK.Core
{
    internal class ClusterizableSentence : IClusterizableSentence
    {
        #region Implementation of IClusterizableSentence

        /// <summary>
        /// Identifier which uniquely identifies categorizer sentense
        /// </summary>
        /// <value>The ID.</value>
        public int ID { get; set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusterizableSentence"/> class.
        /// </summary>
        /// <param name="sentence">The sentence.</param>
        public ClusterizableSentence(ISentence sentence)
        {
            ID = sentence.SentenceID;
            Text = sentence.Text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusterizableSentence"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="text">The text.</param>
        public ClusterizableSentence(int id,string text)
        {
            ID = id;
            Text = text;
        }
    };
}