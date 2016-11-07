using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Advertiser.Core
{
    sealed class BrowserOptions : IDisposable
    {
        private const string keyImages = "Display Inline Images";
        private const string keyExtensions = "Enable Browser Extensions";
        private const string keyIE = @"Software\Microsoft\Internet Explorer\Main";

        private readonly Dictionary<string, object> _previous = new Dictionary<string, object>();

        public BrowserOptions(bool? loadImages = null, bool? loadExtenssios = null)
        {
            using (RegistryKey ieKey = Registry.CurrentUser.CreateSubKey(keyIE))
            {
                SetBoolValue(ieKey, keyImages, loadImages);
                SetBoolValue(ieKey, keyExtensions, loadExtenssios);
            }
        }


        #region Implementation of IDisposable

        public void Dispose()
        {
            if (_previous.Count == 0) return;

            using (var ieKey = Registry.CurrentUser.CreateSubKey(keyIE))
                foreach (var pair in _previous)
                {
                    ieKey.SetValue(pair.Key, pair.Value);
                }
        }

        #endregion
        private void SetBoolValue(RegistryKey registry, string key, bool? value)
        {
            if (value.HasValue)
                SetValue(registry, key, value.Value ? "yes" : "no");
        }

        private void SetValue(RegistryKey registry, string key, object newValue)
        {
            _previous.Add(key, registry.GetValue(keyImages));
            registry.SetValue(keyImages, newValue);
        }
    }
}
