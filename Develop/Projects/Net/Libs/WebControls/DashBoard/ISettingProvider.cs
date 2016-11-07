using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Web.UI
{
    public interface ISettingProvider<T> where T : class
    {

        /// <summary>
        /// Saves the specified setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="clientId">The client id.</param>
        void Save(T setting, string clientId);

        /// <summary>
        /// Loads the specified client id.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <returns></returns>
        T Load(string clientId);
    }
}
