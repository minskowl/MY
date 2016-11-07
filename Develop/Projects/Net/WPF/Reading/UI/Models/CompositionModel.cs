using Reading.Core;
using Reading.Properties;


namespace Reading.Models
{
    internal class CompositionModel : SyllablesModelBase
    {


        public override string Title
        {
            get { return "Наборы"; }
        }

        protected override void SetSyllable()
        {
            var first = Primer.GetConsonant(SelectionMode.All);
            var second = Primer.GetConsonant(SelectionMode.All);
            while (first == second)
            {
                second = Primer.GetConsonant(SelectionMode.All);
            }
            var last = Primer.GetVowel(SelectionMode.Popular);

            SelectedItem = string.Format("{0}{1}{2}", first, second, last);
        }

        /// <summary>
        /// Initializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);

            SetSyllable();
        }
    }
}