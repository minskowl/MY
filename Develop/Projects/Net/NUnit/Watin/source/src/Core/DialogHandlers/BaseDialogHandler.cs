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

using System;
using WatiN.Core.Interfaces;
using WatiN.Core.Native.Windows;

namespace WatiN.Core.DialogHandlers
{
    /// <summary>
    /// BaseDialogHandler
    /// </summary>
	public abstract class BaseDialogHandler : IDialogHandler
	{
		public override bool Equals(object obj)
		{
		    return obj != null && (GetType().Equals(obj.GetType()));
		}

	    public override int GetHashCode()
		{
			return GetType().ToString().GetHashCode();
		}

		#region IDialogHandler Members

		public abstract bool HandleDialog(Window window);
	    
        public virtual bool CanHandleDialog(Window window, IntPtr mainWindowHwnd)
        {
            //var topLevelHwnd = window.ToplevelWindow.Hwnd;
           // return topLevelHwnd == mainWindowHwnd && CanHandleDialog(window);

            return CanHandleDialog(window);
        }

        /// <summary>
        /// Determines whether this instance [can handle dialog] the specified window.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can handle dialog] the specified window; otherwise, <c>false</c>.
        /// </returns>
	    public abstract bool CanHandleDialog(Window window);

	    #endregion
	}
}
