using System;

namespace Savchin.Web
{
    /// <summary>
    /// MailToUrlBuilder class build such urls mailto:mtscf@microsoft.com?subject=Feedback&body=The InetSDK Site Is Superlative"
    /// </summary>
    public class MailToUrlBuilder
    {

        private string to;
        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public string To
        {
            get { return to; }
            set { to = value; }
        }

        private string subject;
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        private string body;
        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string Body
        {
            get { return body; }
            set { body = value; }
        }







        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            var builder = new UriBuilder("mailto", to);

            string query = GetQueryString();
            if (!string.IsNullOrEmpty(query))
            {
                builder.Query = query.Substring(1);
            }

            return builder.ToString();
        }

        private string GetQueryString()
        {
            var queryBuilder = new QueryStringBuilder();
            if (!string.IsNullOrEmpty(subject))
                queryBuilder.Add("subject", subject);
            if (!string.IsNullOrEmpty(body))
                queryBuilder.Add("body", body);


            return queryBuilder.ToString();
        }
    }
}
