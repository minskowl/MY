using EffectiveSoft.IES.IntellexerAPI.Core;
using EffectiveSoft.IES.MsSql.DataLayer.Controllers;

namespace IES.PerformanceTester.IntelexerSDK.Core
{
    class IntelexerHelper
    {
        private static bool _initialized;
        private static object locker = new object();

        /// <summary>
        /// Initializes the SDK.
        /// </summary>
        public static void InitializeSdk()
        {
            lock (locker)
            {
                if (_initialized) return;

                ObjectFactory.CreateLicenseChecker().InitializeLicense();

                var controller = AppCore.UseWordCache ?
                                                          new SynchronizedWordControllerDecorator(new CacheWordControllerDecorator(new WordController())) :
                                                                                                                                                            
                                                          new SynchronizedWordControllerDecorator(new WordController());

                new DocumentDriverHost(new DocumentIndexDriver(controller));
                new CategorizerDriverHost(new CategorizerIndexDriver(controller));
                _initialized = true;
            }


        }
    }
}