#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//Copyright 2006-2009 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using SHDocVw;

namespace WatiN.Core.Native.InternetExplorer
{
    public class IEWaitForComplete : WaitForComplete
    {
        protected IE _ie;

        /// <summary>
        /// Initializes a new instance of the <see cref="IEWaitForComplete"/> class.
        /// </summary>
        /// <param name="ie">The ie.</param>
        public IEWaitForComplete(IE ie) : base(ie)
        {
            _ie = ie;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IEWaitForComplete"/> class.
        /// </summary>
        /// <param name="ie">The ie.</param>
        /// <param name="waitForCompleteTimeOut">The wait for complete time out.</param>
        public IEWaitForComplete(IE ie, int waitForCompleteTimeOut) : base(ie, waitForCompleteTimeOut)
        {
            _ie = ie;
        }

        public override void DoWait()
        {
            InitTimeout();

            WaitWhileIEBusy((IWebBrowser2) _ie.InternetExplorer);
            WaitWhileIEReadyStateNotComplete((IWebBrowser2) _ie.InternetExplorer);

            WaitForCompleteOrTimeout();
        }
    }
}