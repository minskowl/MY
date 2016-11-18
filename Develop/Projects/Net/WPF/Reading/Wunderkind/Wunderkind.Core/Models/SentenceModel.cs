using System.Collections.Generic;
using Reading.Core;
using Wunderkind.Core;

namespace Reading.Models
{
    public class SentenceModel : ListModel<string>
    {
        private SenetenceCollection _sentences=new SenetenceCollection();

   

        public override string Title
        {
            get { return "Предложения"; }
        }

        protected override List<string> BuildList()
        {
            return _sentences.GetSentences();
        }

        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);

          
            ItemsRepetable = settings.WordRepeatable;

            SetNewItem();
        }
    }
}
