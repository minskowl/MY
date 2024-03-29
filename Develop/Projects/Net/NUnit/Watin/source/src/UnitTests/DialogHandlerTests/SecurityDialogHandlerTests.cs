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
using NUnit.Framework;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Native.Windows;

namespace WatiN.Core.UnitTests.DialogHandlerTests
{
	[TestFixture]
	public class SecurityDialogHandlerTests : BaseWatiNTest
	{
		[Test, Ignore("No Security dialog is shown any more")] // Category("InternetConnectionNeeded")]
		public void SecurityAlertDialogHandler()
		{
			SecurityAlertDialogHandlerMock securityAlertDialogHandlerMock = new SecurityAlertDialogHandlerMock();

			using (IE ie = new IENoWaitForComplete("http://sourceforge.net"))
			{
				ie.AddDialogHandler(securityAlertDialogHandlerMock);
				ie.Link(Find.ByText("Log in")).Click();

				ie.TextField(Find.ByName("form_loginname")).WaitUntilExists();

				Assert.IsTrue(securityAlertDialogHandlerMock.HasHandledSecurityAlertDialog);
			}
		}

		private class IENoWaitForComplete : IE
		{
			public IENoWaitForComplete(string url) : base(url) {}

			public override void WaitForComplete(int timeOutPeriod)
			{
				// Skip Wait logic
			}
		}

		private class SecurityAlertDialogHandlerMock : SecurityAlertDialogHandler
		{
			private bool _hasHandledSecurityAlertDialog;

			public bool HasHandledSecurityAlertDialog
			{
				get { return _hasHandledSecurityAlertDialog; }
			}

			public override bool HandleDialog(Window window)
			{
				bool handled = base.HandleDialog(window);

				if (handled && !HasHandledSecurityAlertDialog)
				{
					_hasHandledSecurityAlertDialog = true;
				}
				return handled;
			}
		}
	}
}