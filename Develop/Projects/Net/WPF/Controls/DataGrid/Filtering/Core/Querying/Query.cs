using System;
using System.Collections.Generic;

namespace Savchin.Wpf.Controls.DataGrid.Filtering.Core.Querying
{
    public class Query
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        public Query()
        {
            lastFilterString    = String.Empty;
            lastQueryParameters = new List<object>();
        }

        public string        FilterString { get; set; }
        public List<object>  QueryParameters { get; set; }

        private string       lastFilterString { get; set; }
        private List<object> lastQueryParameters { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is query changed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is query changed; otherwise, <c>false</c>.
        /// </value>
        public bool IsQueryChanged
        {
            get
            {
                bool queryChanged = false;

                if (FilterString != lastFilterString)
                {
                    queryChanged = true;
                }
                else
                {
                    if (QueryParameters.Count != lastQueryParameters.Count)
                    {
                        queryChanged = true;
                    }
                    else
                    {
                        for (int i = 0; i < QueryParameters.Count; i++)
                        {
                            if (!QueryParameters[i].Equals(lastQueryParameters[i]))
                            {
                                queryChanged = true;
                                break;
                            }
                        }
                    }
                }

                return queryChanged;
            }
        }

        public void StoreLastUsedValues()
        {
            lastFilterString    = FilterString;
            lastQueryParameters = QueryParameters;
        }
    }
}
