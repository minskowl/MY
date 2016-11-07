using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TVSeriesTracker.Core;

namespace TVSeriesTracker.Controllers
{
    public class ImagesController : Controller
    {
        public Task<ActionResult> Get(string url)
        {

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Timeout = 20000;
            request.Proxy = new WebProxy("188.64.128.1", 3128);
            //     var t=  request.GetResponse();
            // return new ImageResult(t.GetResponseStream(), t.ContentType);
            Task<WebResponse> task = Task.Factory.FromAsync(request.BeginGetResponse, asyncResult => request.EndGetResponse(asyncResult), (object)null);

            return task.ContinueWith(t => (ActionResult)new ImageResult(t.Result.GetResponseStream(), t.Result.ContentType));


        }


    }
}
