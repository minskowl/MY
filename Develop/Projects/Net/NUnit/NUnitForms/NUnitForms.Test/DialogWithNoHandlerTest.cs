using System.Windows.Forms;
using NUnit.Extensions.Forms.TestApplications;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.Test
{
    [TestFixture]
    public class DialogWithNoHandlerTest : NUnitFormTest
    {
        private ButtonTester acceptButton;
        private ButtonTester rejectButton;


        [Test]
        public void TestAcceptButton()
        {
            ExpectModal(
                "DialogWithNoHandlersForm",
                delegate
                    {
                        acceptButton = new ButtonTester("button1");
                        acceptButton.Click();
                    });
            DialogWithNoHandlersForm form = new DialogWithNoHandlersForm();
            DialogResult result = form.ShowDialog();
            Assert.AreEqual(DialogResult.OK, result, "Wrong dialog result.");
            Assert.IsFalse(form.Visible, "Form was still visible.");
        }

        [Test]
        public void TestRejectButton()
        {
            ExpectModal(
                "DialogWithNoHandlersForm",
                delegate
                    {
                        rejectButton = new ButtonTester("button2");
                        rejectButton.Click();
                    });

            DialogWithNoHandlersForm form = new DialogWithNoHandlersForm();
            DialogResult result = form.ShowDialog();
            Assert.AreEqual(DialogResult.Cancel, result, "Wrong dialog result.");
            Assert.IsFalse(form.Visible, "Form was still visible.");
        }
    }
}