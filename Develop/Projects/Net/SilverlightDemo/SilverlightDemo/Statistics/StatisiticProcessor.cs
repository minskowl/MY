using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using EffectiveSoft.SilverlightDemo.Core;
using Visifire.Charts;
using System.Windows;

namespace EffectiveSoft.SilverlightDemo.Statistics
{
    public class StatisiticProcessor
    {

        private readonly List<FuelingOperation> data = new List<FuelingOperation>();

     //   private readonly List<SeriesSource> viewers = new List<SeriesSource>();
       //


        private readonly static StatisiticProcessor instance = new StatisiticProcessor();

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisiticProcessor"/> class.
        /// </summary>
        private StatisiticProcessor()
        {



        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static StatisiticProcessor Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public List<FuelingOperation> Data
        {
            get { return data; }
        }

        /// <summary>
        /// Adds the operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public void AddOperation(FuelingOperation operation)
        {
            operation.Hours = VirtualClock.Instance.ClockData.Hours;
            operation.Minutes = VirtualClock.Instance.ClockData.Minutes;
            data.Add(operation);

        }
       
      
 
    }
}
