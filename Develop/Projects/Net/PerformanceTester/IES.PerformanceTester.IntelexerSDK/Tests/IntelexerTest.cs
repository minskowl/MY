using System;
using IES.PerformanceTester.IntelexerSDK.Core;
using IES.PerformanceTester.Tests;

namespace IES.PerformanceTester.IntelexerSDK.Tests
{
    public abstract class IntelexerTest : TestBase
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected override bool Initialize()
        {
            if (!base.Initialize()) return false;

            try
            {
                IntelexerHelper.InitializeSdk();
                return true;
            }
            catch (Exception ex)
            {
                Log.Fatal(" IntelexerHelper.InitializeSdk", ex);
            }
            return false;
        }
    }
}
