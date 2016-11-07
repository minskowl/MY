using System.Drawing;
using System.Web;

namespace Savchin.Web.UI
{
    /// <summary>
    /// CaptchaImageHandler
    /// </summary>
    public class CaptchaImageHandler : IHttpHandler
    {

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            HttpApplication app = context.ApplicationInstance;

            //-- get the unique GUID of the captcha; this must be passed in via the querystring
            string guid = app.Request.QueryString["guid"];
            CaptchaImage ci = null;

            if (!string.IsNullOrEmpty(guid))
            {
                if (string.IsNullOrEmpty(app.Request.QueryString["s"]))
                {
                    ci = (CaptchaImage)HttpRuntime.Cache.Get(guid);
                }
                else
                {
                    ci = (CaptchaImage)HttpContext.Current.Session[guid];
                }

            }

            if (ci == null)
            {
                app.Response.StatusCode = 404;
                context.ApplicationInstance.CompleteRequest();
                return;
            }

            //-- write the image to the HTTP output stream as an array of bytes
            Bitmap b = ci.RenderImage();
            b.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            b.Dispose();
            app.Response.ContentType = "image/jpeg";
            app.Response.StatusCode = 200;
            context.ApplicationInstance.CompleteRequest();
        }

        public bool IsReusable
        {
            get { return true; }
        }

    }
}
