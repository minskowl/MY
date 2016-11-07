using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Tools.Core
{
    interface IObjectViewer
    {
        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
        /// <summary>
        /// Displays the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void Display(object obj);

        /// <summary>
        /// Shows the status.
        /// </summary>
        /// <param name="status">The status.</param>
        void ShowStatus(string status);
    }
}