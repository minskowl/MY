using System.Collections.Generic;
using System.IO;
using System.Linq;
using Reading.Core;
using Reading.Properties;

namespace Reading.Models
{
    public class CompositionListModel : ListModel<string>
    {
        #region Overrides of BaseModel


        public override string Title
        {
            get { return "Наборы"; }
        }

        #endregion

        #region Overrides of ListModel<string>
        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);


            SetNewItem();
        }
        protected override List<string> BuildList()
        {
            var result =
                File.ReadAllLines(ResourceProvider.CompositionsFile).TakeWhile(e => !string.IsNullOrWhiteSpace(e)).
                    ToList();
            return result;
        }

        #endregion
    }
}
