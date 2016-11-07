using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TVSeriesTracker.Core
{
    public class ImageService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var url = context.Request.RawUrl;
        }

        public bool IsReusable {
            get { return true; }
        }
    }
}