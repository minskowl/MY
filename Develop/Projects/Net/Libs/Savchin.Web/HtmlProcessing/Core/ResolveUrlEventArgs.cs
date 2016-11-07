using System;

namespace Savchin.Web.HtmlProcessing.Core
{
    /// <summary>
    /// Provides information about tag to be resolved
    /// </summary>
    public class ResolveUrlEventArgs : EventArgs
    {
        #region Fields

        private TagInfo _info  = null;
        private string _oldUrl = string.Empty;
        private string _newUrl = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the old URL.
        /// </summary>
        /// <value>The old URL.</value>
        public string OldUrl
        {
            get { return _oldUrl;  }
            set { _oldUrl = value; }
        }

        /// <summary>
        /// Gets or sets the new URL.
        /// </summary>
        /// <value>The new URL.</value>
        public string NewUrl
        {
            get { return _newUrl;  }
            set { _newUrl = value; }
        }

        /// <summary>
        /// Gets the tag info.
        /// </summary>
        /// <value>The tag info.</value>
        public TagInfo TagInfo
        {
            get { return _info;  }
            set { _info = value; }
        }

        #endregion
    }
}