using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Savchin.Web.UI
{
    /// <summary>
    /// Extended image. Url fix and autodetect size of image.
    /// </summary>
    public class ImageEx : Image
    {
        /// <summary>
        /// Gets or sets a value indicating whether [auto detect size].
        /// </summary>
        /// <value><c>true</c> if [auto detect size]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool AutoDetectSize
        {
            get
            {
                object obj1 = ViewState["AutoDetectSize"];
                return (obj1 != null) ? (bool)obj1 : false;
            }
            set
            {
                ViewState["AutoDetectSize"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the location of an image to display in the <see cref="T:System.Web.UI.WebControls.Image"/> control.
        /// </summary>
        /// <value></value>
        /// <returns>The location of an image to display in the <see cref="T:System.Web.UI.WebControls.Image"/> control.</returns>
        public override string ImageUrl
        {
            get { return (DesignMode || UrlResolved) ? base.ImageUrl : ControlHelper.GetFullImageUrl(base.ImageUrl, Page); }
            set { base.ImageUrl = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [URL resolved].
        /// </summary>
        /// <value><c>true</c> if [URL resolved]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        public bool UrlResolved
        {
            get
            {
                object obj1 = ViewState["UrlResolved"];
                return (obj1 != null) ? (bool)obj1 : false;
            }
            set
            {
                ViewState["UrlResolved"] = value;
            }
        }


        /// <summary>
        /// Detects the size of the image.
        /// </summary>
        protected void DetectImageSize()
        {
            try
            {
                string imageFile = MapPathSecure(ResolveClientUrl(ImageUrl));
                System.Drawing.SizeF imageSize = ControlHelper.GetImageSize(imageFile);

                Width = Unit.Pixel(Convert.ToInt32(imageSize.Width));
                Height = Unit.Pixel(Convert.ToInt32(imageSize.Height));
            }
            catch (HttpException)
            {
            }
            catch (Exception ex)
            {
                Util.Log.Error("DetectImageSize", ex);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (!DesignMode && AutoDetectSize)
                DetectImageSize();

            base.OnPreRender(e);
        }

        /// <summary>
        /// Adds the attributes of an <see cref="T:System.Web.UI.WebControls.Image"/> to the output stream for rendering on the client.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that contains the output stream to render on the client browser.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if(!Width.IsEmpty)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Width, Width.ToString());
            }

            if (!Height.IsEmpty)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Height, Height.ToString());
            }
        }
    }
}
