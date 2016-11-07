using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Downloader
{
    public class RequestState
    {
        // This class stores the request state of the request.
        public WebRequest request;
        public RequestState()
        {
            request = null;
        }
    }

}
