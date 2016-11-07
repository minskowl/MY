using System.IO;
using System.Web;
using System.Web.UI;
using dotnetCHARTING;


namespace Savchin.Web.UI
{
    /// <summary>
    /// ChartEx
    /// </summary>
    public class ChartEx : ImageEx
    {
        private Chart chart = new Chart();
        private object dataSource;
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public object DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                if (value != null)
                {
                    if (value is SeriesCollection)
                    {
                        chart.SeriesCollection.Add(value as SeriesCollection);
                    }
                    else
                    {
                        chart.Series.Data = value;
                        chart.SeriesCollection.Add();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public Chart Instance
        {
            get { return chart; }
            set { chart = value; }
        }
        /// <summary>
        /// Gets the relative images path.
        /// </summary>
        /// <value>The relative images path.</value>
        public string ImagesPath
        {
            get
            {
                return(string) ViewState["ImagesPath"];
            }
            set { ViewState["ImagesPath"] = value; }
        }
        /// <summary>
        /// Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object and stores tracing information about the control if tracing is enabled.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HTmlTextWriter"/> object that receives the control content.</param>
        public override void RenderControl(HtmlTextWriter writer)
        {
            string imageFileName = Path.GetFileName(RenderToImage());
            string currentUrl = HttpContext.Current.Request.Url.ToString();
            ImageUrl = currentUrl.Substring(0, currentUrl.LastIndexOf('/') + 1) + ImagesPath + imageFileName;

            base.RenderControl(writer);
        }

        /// <summary>
        /// Called when final init chart.
        /// </summary>
        protected virtual void OnFinalInitChart()
        {
        }

        /// <summary>
        /// Renders to image.
        /// </summary>
        /// <returns></returns>
        private string RenderToImage()
        {
            OnFinalInitChart();

            chart.Width = Width;
            chart.Height = Height;
            chart.TempDirectory = ImagesPath;

            return chart.FileManager.SaveImage();
        }
    }




}
