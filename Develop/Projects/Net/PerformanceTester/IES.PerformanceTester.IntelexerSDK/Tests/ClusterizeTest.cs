using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EffectiveSoft.IES.IntellexerAPI;
using EffectiveSoft.IES.IntellexerAPI.Core;
using EffectiveSoft.IES.MsSql.DataLayer.Controllers;
using IES.PerformanceTester.Core;
using IES.PerformanceTester.IntelexerSDK.Core;
using IES.PerformanceTester.Tests;
using log4net;

namespace IES.PerformanceTester.IntelexerSDK.Tests
{
    /// <summary>
    /// ClusterizeTest
    /// </summary>
    public class ClusterizeTest : IntelexerTest
    {
        private readonly IQas _qas;
        private readonly IClusterizer _clusterizer;
        private readonly SentenceController _sentenceController;
        /// <summary>
        /// Gets the count tests.
        /// </summary>
        /// <value>The count tests.</value>
        protected override int CountTests
        {
            get { return QasParams.Queries.Length; }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="SearchTest"/> class.
        /// </summary>
        public ClusterizeTest()
        {
            _qas = ObjectFactory.CreateQas();
            _clusterizer = ObjectFactory.CreateClusterizer();
            _sentenceController = new SentenceController();
            PerformanceLog = LogManager.GetLogger("SDKClusterize");
        }



        /// <summary>
        /// Does the test.
        /// </summary>
        protected override void DoTest(int testNumber)
        {

            var query = QasParams.Queries[testNumber];
            var resultsSearch = _qas.Search(query);

            var sentences = GetQuerySentences(resultsSearch);
            Log.DebugFormat("Thread {0} do clusterize '{1}' senteces {2}",
                            Thread.CurrentThread.ManagedThreadId, (query.Query), sentences.Length);

            _clusterizer.Clusterize(query.Query, sentences);

            Log.Debug("Do Clusterise ");
        }

        private IClusterizableSentence[] GetQuerySentences(IEnumerable<IQasResult> results)
        {
            var ids = results.Select(e => e.SentenceID).Distinct().ToArray();
            var sentences = _sentenceController.GetSentences(ids).ToArray();

            return sentences.Length == 0 ? new IClusterizableSentence[0] :
                                                                             Randomizer.GetFromArray(sentences, Randomizer.GetIntegerBetween(2, sentences.Length))
                                                                                 .Select(e => new ClusterizableSentence(e))
                                                                                 .Cast<IClusterizableSentence>().ToArray();
        }

    }
}