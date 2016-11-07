using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EffectiveSoft.IES.IntellexerAPI;
using EffectiveSoft.IES.IntellexerAPI.Core;
using IES.PerformanceTester.Core;
using IES.PerformanceTester.IntelexerSDK.Core;
using IES.PerformanceTester.Tests;
using log4net;

namespace IES.PerformanceTester.IntelexerSDK.Tests
{
    /// <summary>
    /// SearchTest
    /// </summary>
    public class SearchTest : IntelexerTest
    {
        #region Propperties
        /// <summary>
        /// Gets the count tests.
        /// </summary>
        /// <value>The count tests.</value>
        protected override int CountTests
        {
            get { return QasParams.Queries.Length; }
        }


        private readonly IQas _qas; 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchTest"/> class.
        /// </summary>
        public SearchTest()
        {
            _qas = ObjectFactory.CreateQas();
            PerformanceLog = LogManager.GetLogger("SDKSearch");
        }


        /// <summary>
        /// Does the test.
        /// </summary>
        protected override void DoTest(int testNumber)
        {

            var query = QasParams.Queries[testNumber];
            var results = _qas.Search(query);
            var docCount = results.Select(e => e.DocumentID).Distinct().Count();

            Log.DebugFormat("Do Query {0} DocCount={1} ResultsCount={2}", query, docCount, results.Count());
        }




    }
}