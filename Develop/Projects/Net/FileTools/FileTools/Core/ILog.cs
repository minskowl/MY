using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileTools.Core
{
    public interface ILog
    {
        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="measage">The measage.</param>
        void AddLog(string measage);
    }
}
