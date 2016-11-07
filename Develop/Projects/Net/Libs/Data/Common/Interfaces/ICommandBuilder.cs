using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Savchin.Data.Common
{
    public interface ICommandBuilder
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IDbCommand Build();
    }
}
