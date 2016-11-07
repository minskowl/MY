using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Automation.Mining
{
    public class VisiteGladeResult : CristalBenefit
    {
        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        public List<int> Sequence { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="VisiteGladeResult"/> class.
        /// </summary>
        public VisiteGladeResult()
        {
            Sequence= new List<int>();
        }
    }
}
