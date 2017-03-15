using Prodigy.Properties;
using Reading.Core;

namespace Prodigy.Models.Reading
{
    internal class CompositionModel : SyllablesModelBase
    {


        public override string Title => "Наборы";

        protected override void SetSyllable()
        {
            var first = Primer.GetConsonant(SelectionMode.All);
            var second = Primer.GetConsonant(SelectionMode.All);
            while (first == second)
            {
                second = Primer.GetConsonant(SelectionMode.All);
            }
            var last = Primer.GetVowel(SelectionMode.Popular);

            SelectedItem = $"{first}{second}{last}";
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