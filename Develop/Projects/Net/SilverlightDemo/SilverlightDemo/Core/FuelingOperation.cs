namespace EffectiveSoft.SilverlightDemo.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class FuelingOperation
    {
        /// <summary>
        /// Gets or sets the pump number.
        /// </summary>
        /// <value>The pump number.</value>
        public short PumpNumber { get; set; }
        /// <summary>
        /// Gets or sets the litres.
        /// </summary>
        /// <value>The litres.</value>
        public short Litres { get; set; }

        /// <summary>
        /// Gets or sets the type of the fuel.
        /// </summary>
        /// <value>The type of the fuel.</value>
        public FuelType FuelType { get; set; }
        /// <summary>
        /// Gets the hour.
        /// </summary>
        /// <value>The hour.</value>
        public int Hours{get;set;}
        /// <summary>
        /// Gets or sets the minute.
        /// </summary>
        /// <value>The minute.</value>
        public int Minutes { get; set; }
      

    }
}
