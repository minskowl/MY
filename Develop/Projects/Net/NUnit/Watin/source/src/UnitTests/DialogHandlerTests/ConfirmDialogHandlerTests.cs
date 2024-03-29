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

namespace WatiN.Core.UnitTests.DialogHandlerTests
{
	[TestFixture]
	public class ConfirmDialogHandlerTests : BaseWithBrowserTests
	{
		[Test]
		public void ConfirmDialogHandlerOK()
		{
			Assert.AreEqual(0, Ie.DialogWatcher.Count, "DialogWatcher count should be zero");

			ConfirmDialogHandler confirmDialogHandler = new ConfirmDialogHandler();

			using (new UseDialogOnce(Ie.DialogWatcher, confirmDialogHandler))
			{
				Ie.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();

				confirmDialogHandler.WaitUntilExists();

				string message = confirmDialogHandler.Message;
				confirmDialogHandler.OKButton.Click();

				Ie.WaitForComplete();

				Assert.AreEqual("Do you want to do xyz?", message, "Unexpected message");
				Assert.AreEqual("OK", Ie.TextField("ReportConfirmResult").Text, "OK button expected.");
			}
		}

		[Test]
		public void ConfirmDialogHandlerCancel()
		{
			Assert.AreEqual(0, Ie.DialogWatcher.Count, "DialogWatcher count should be zero");

			ConfirmDialogHandler confirmDialogHandler = new ConfirmDialogHandler();

			using (new UseDialogOnce(Ie.DialogWatcher, confirmDialogHandler))
			{
				Ie.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();

				confirmDialogHandler.WaitUntilExists();

				string message = confirmDialogHandler.Message;
				confirmDialogHandler.CancelButton.Click();

				Ie.WaitForComplete();

				Assert.AreEqual("Do you want to do xyz?", message, "Unexpected message");
				Assert.AreEqual("Cancel", Ie.TextField("ReportConfirmResult").Text, "Cancel button expected.");
			}
		}

		public override Uri TestPageUri
		{
			get { return TestEventsURI; }
		}
	}
}