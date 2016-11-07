using BotvaSpider.Core;

namespace BotvaSpider.Data
{
    public class Skill
    {
        /// <summary>
        /// Gets or sets the type of the skil.
        /// </summary>
        /// <value>The type of the skil.</value>
        public SkillType SkilType { get; set; }
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        public double Points { get; set; }

        /// <summary>
        /// Gets or sets the additional points.
        /// </summary>
        /// <value>The additional points.</value>
        public double AdditionalPoints { get; set; }
    }
}