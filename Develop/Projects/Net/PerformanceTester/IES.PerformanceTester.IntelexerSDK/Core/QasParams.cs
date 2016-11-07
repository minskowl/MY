using System;
using EffectiveSoft.IES.IntellexerAPI;

namespace IES.PerformanceTester.IntelexerSDK.Core
{
    internal class QasParams : IQasParams
    {

        /// <summary>
        /// Queries
        /// </summary>
        public static readonly QasParams[] Queries = {
                                                         new QasParams("connection" ),  
                                                         new QasParams("How to make a bomb"),  
                                                         new QasParams("How to heat water?"),
                                                         new QasParams("cell"),
                                                         new QasParams("mixture"),
                                                         new QasParams("Hydroxyapatite"),
                                                     };
        /// <summary>
        /// Mimal answer relevance
        /// </summary>
        public double MinRelevance { get; set; }

        /// <summary>
        /// Specifies maximum result count
        /// </summary>
        public int MaxResultCount { get; set; }

        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <value>The query.</value>
        public string Query { get; set; }

        /// <summary>
        /// Gets the query type enum.
        /// </summary>
        /// <value>The query type enum.</value>
        public QueryTypeEnum QasQueryType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QasParams"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        public QasParams(string query)
            : this(query, QueryTypeEnum.NaturalLanguage, 100, 50)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QasParams"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="maxResultCount">The max result count.</param>
        /// <param name="minRelevance">The min relevance.</param>
        public QasParams(string query, int maxResultCount, double minRelevance)
            : this(query, QueryTypeEnum.NaturalLanguage, maxResultCount, minRelevance)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QasParamsImpl"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="type">The type.</param>
        /// <param name="maxResultCount">The max result count.</param>
        /// <param name="minRelevance">The min relevance.</param>
        public QasParams(string query, QueryTypeEnum type, int maxResultCount, double minRelevance)
        {
            Query = query;
            QasQueryType = type;
            MaxResultCount = maxResultCount;
            MinRelevance = minRelevance;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return String.Format("'{0}' {1} MaxResultCount={2} MinRelevance={3}",
                                 Query, QasQueryType, MaxResultCount, MinRelevance);
        }
    }
}