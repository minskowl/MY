using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Visifire.Charts;

namespace EffectiveSoft.SilverlightDemo.Core
{
    public class FuelTypeInfo
    {

        private static Dictionary<FuelType, FuelTypeInfo> types = new Dictionary<FuelType, FuelTypeInfo>();

        #region Properties

        private readonly string htmlColor;
        /// <summary>
        /// Gets the color of the HTML.
        /// </summary>
        /// <value>The color of the HTML.</value>
        public string HtmlColor
        {
            get { return htmlColor; }
        }

        private readonly Brush brushSolid;
        /// <summary>
        /// Gets the solid brush.
        /// </summary>
        /// <value>The solid brush.</value>
        public Brush SolidBrush
        {
            get { return brushSolid; }
        }
        private readonly FuelType type;
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public FuelType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return type.ToString(); }
        }
        #endregion



        /// <summary>
        /// Initializes the <see cref="FuelTypeInfo"/> class.
        /// </summary>
        static FuelTypeInfo()
        {
            types.Add(FuelType.Regular, new FuelTypeInfo(FuelType.Regular, "#FFD8D69B"));
            types.Add(FuelType.Premium, new FuelTypeInfo(FuelType.Premium, "#FFADD89B"));
            types.Add(FuelType.Super, new FuelTypeInfo(FuelType.Super, "#FF9BBFD8"));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FuelTypeInfo"/> class.
        /// </summary>
        /// <param name="htmlColor">Color of the HTML.</param>
        private FuelTypeInfo(FuelType type, string htmlColor)
        {
            this.htmlColor = htmlColor;
            this.type = type;
            brushSolid = new SolidColorBrush { Color = ConverterColor.ToColor(htmlColor).Value };
        }

        /// <summary>
        /// Creates all series.
        /// </summary>
        /// <returns></returns>
        public static DataSeries[] CreateAllSeries()
        {
            return new DataSeries[3]
                       {
                           types[FuelType.Regular].CreateSeries(),
                           types[FuelType.Premium].CreateSeries(),
                           types[FuelType.Super].CreateSeries()
                       };
        }


        /// <summary>
        /// Creates the legend.
        /// </summary>
        /// <returns></returns>
        public Legend CreateLegend()
        {
            return UIFactory.CreateLegend(Name);
        }

        /// <summary>
        /// Creates the series.
        /// </summary>
        /// <returns></returns>
        public DataSeries CreateSeries()
        {
            return new DataSeries
                       {
                           RenderAs = RenderAs.Column, 
                           Background = SolidBrush, 
                           Name = Name, 
                           LegendText = Name,
                           Legend = "legend"
                       };
        }

        /// <summary>
        /// Gets the info.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static FuelTypeInfo GetInfo(FuelType type)
        {
            return types[type];
        }
    }
}
