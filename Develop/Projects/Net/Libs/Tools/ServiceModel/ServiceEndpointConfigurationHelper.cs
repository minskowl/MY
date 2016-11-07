using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;

namespace Savchin.ServiceModel
{
    /// <summary>
    /// Provides different WCF service endpoints configuration helper functionality.
    /// </summary>
    public static class ServiceEndpointConfigurationHelper
    {
        private static readonly TimeSpan MaxClockSkew = TimeSpan.FromDays(1);

        /// <summary>
        /// Increases the LocalClientSettings/LocalClientSettings maxClockSkew value to 1 day.
        /// </summary>
        /// <param name="binding">The binding to set maxClockSkew on.</param>
        public static void ConfigureMaxClockSkew(CustomBinding binding)
        {
            var security = binding.Elements.Find<SymmetricSecurityBindingElement>();

            security.LocalClientSettings.MaxClockSkew = MaxClockSkew;
            security.LocalServiceSettings.MaxClockSkew = MaxClockSkew;

            var secureTokenParams = (SecureConversationSecurityTokenParameters)security.ProtectionTokenParameters;
            var bootstrap = secureTokenParams.BootstrapSecurityBindingElement;
            bootstrap.LocalClientSettings.MaxClockSkew = MaxClockSkew;
            bootstrap.LocalServiceSettings.MaxClockSkew = MaxClockSkew;
        }
    }
}
