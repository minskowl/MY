using System.Collections.Generic;
using System.IO;
using System.Linq;
using Prodigy.Models.Core;
using Prodigy.Properties;
using Reading.Core;

namespace Prodigy.Models.Reading
{
    public class CompositionListModel : ListModel<string>
    {


        public override string Title => "Наборы";

       
        protected override List<string> BuildList()
        {
            var result = File.ReadAllLines(ResourceProvider.CompositionsFile).TakeWhile(e => !string.IsNullOrWhiteSpace(e)).ToList();
            return result;
        }


    }
}
