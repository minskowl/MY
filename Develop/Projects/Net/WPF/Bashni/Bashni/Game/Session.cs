using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Serialization;
using Bashni.Controls;
using Bashni.Core;

namespace Bashni.Game
{
    public class Session : ObjectBase
    {
        public List<Color> Colors { get; set; }

        public Step Root { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        public Session()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="colors">The colors.</param>
        public Session(Field field, List<Color> colors)
        {
            var step = new Step
                           {
                               Field = field,
                           };
            Root = step;
            Colors = colors;
        }

        /// <summary>
        /// Gets the bests.
        /// </summary>
        /// <returns></returns>
        public Step[] GetBests()
        {
            var steps = Root.Steps;
            var min = steps.Min(e => e.Progress);
            var mins = steps.Where(e => e.Progress == min).ToArray();

            min = mins.Min(e => e.Number);
            mins = mins.Where(e => e.Number == min).Distinct().ToArray();
            return mins;
        }




        public override event PropertyChangedEventHandler PropertyChanged;
    }
}
