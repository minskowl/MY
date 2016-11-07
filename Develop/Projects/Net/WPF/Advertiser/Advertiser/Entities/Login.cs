using System.ComponentModel;

namespace Advertiser.Entities
{
    public enum Site
    {
        [Description("www.abw.by")]
        ABW,
        [Description("www.onliner.by")]
        Onliner,
        [Description("www.av.by")]
        AV,
        [Description("www.irr.by")]
        IRR,
        [Description("www.tut.by")]
        TUTBY
    }

    public class Login : Entity
    {
        public Site Site { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title
        {
            get { return string.Format("#{2} {0} {1}", Site, User, Id); }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Title;
        }
    }
}
