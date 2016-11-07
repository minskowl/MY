using System;
using System.Collections.Generic;
using Advertiser.Entities;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace Advertiser.Controllers
{
    public interface IAdvController : IDisposable
    {
        string MainUrl { get; }

        bool DoLogin(Login login);
        void DoLogout();
        void Post(Wheels adv);
        void Clear();

        IE Browser { set; }
        ILogWriter LogWriter { set; }
        Dictionary<int, Phone> Phones { set; }

        void DoUp();
    }
}