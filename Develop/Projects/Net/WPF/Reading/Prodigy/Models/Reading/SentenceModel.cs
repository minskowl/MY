using System.Collections.Generic;
using Prodigy.Models.Core;
using Prodigy.Properties;
using Reading.Core;

namespace Prodigy.Models.Reading
{
    public class SentenceModel : ListModel<string>
    {
        private readonly SenetenceCollection _sentences = new SenetenceCollection();


        public override string Title => "Предложения";

        protected override List<string> BuildList()
        {
            return _sentences.GetSentences();
        }

        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);

            ItemsRepetable = settings.WordRepeatable;
        }
    }
}
