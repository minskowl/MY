#region Copyright (c) 2003-2005, Luke T. Maxon

/********************************************************************************************************************
'
' Copyright (c) 2003-2005, Luke T. Maxon
' All rights reserved.
' 
' Redistribution and use in source and binary forms, with or without modification, are permitted provided
' that the following conditions are met:
' 
' * Redistributions of source code must retain the above copyright notice, this list of conditions and the
' 	following disclaimer.
' 
' * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
' 	the following disclaimer in the documentation and/or other materials provided with the distribution.
' 
' * Neither the name of the author nor the names of its contributors may be used to endorse or 
' 	promote products derived from this software without specific prior written permission.
' 
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
' WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
' PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
' ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
' LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
' INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
' OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
' IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'
'*******************************************************************************************************************/

#endregion

using System.Windows.Forms;
using NUnit.Extensions.Forms.TestApplications;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.Recorder.Test
{
    ///<summary>
    /// Test Fixture for the <see cref="TextBoxRecorder"/>.
    ///</summary>
    [TestFixture]
    [Category("Recorder")]
    public class TextBoxRecorderTest : NUnitFormTest
    {
        [Test]
        public void ProgrammaticallyChangeTextIsNotRecorded()
        {
            Form form = new TextBoxTestForm();
            form.Show();
            TestWriter writer = new TestWriter(form);
            Assert.AreEqual("", writer.Test);

            TextBoxTester textBox = new TextBoxTester("myTextBox", form);
            textBox.Properties.Text = "abc";

            Assert.AreEqual(@"", writer.Test);
        }

        [Test]
        public void ProgrammaticallyChangeTextIsNotRecordedTwoBoxes()
        {
            Form form = new TextBoxTestForm();
            form.Show();
            TestWriter writer = new TestWriter(form);
            Assert.AreEqual("", writer.Test);

            TextBoxTester anotherBox = new TextBoxTester("anotherTextBox", form);
            anotherBox.FireEvent("Enter");

            TextBoxTester textBox = new TextBoxTester("myTextBox", form);
            textBox.Properties.Text = "abc";

            anotherBox.FireEvent("Leave");

            Assert.AreEqual(@"", writer.Test);
        }

        ///<summary>
        /// Tests text entry events.
        ///</summary>
        [Test]
        public void TextBoxEnter()
        {
            Form form = new TextBoxTestForm();
            form.Show();
            TestWriter writer = new TestWriter(form);
            Assert.AreEqual("", writer.Test);

            TextBoxTester textBox = new TextBoxTester("myTextBox", form);
            //doing 2 of these tests the collapsing processor.
            textBox.Enter("abc");
            textBox.Enter("abcd");

            Assert.AreEqual(
                @"[Test]
public void Test()
{

	TextBoxTester myTextBox = new TextBoxTester(""myTextBox"");

	myTextBox.Enter(""abcd"");

}",
                writer.Test);
        }

        ///<summary>
        /// Tests multiline text entry events.
        ///</summary>
        [Test]
        [Ignore]
        public void TextBoxEnterMultiline()
        {
            Form form = new TextBoxTestForm();
            form.Show();
            TestWriter writer = new TestWriter(form);
            Assert.AreEqual("", writer.Test);

            TextBoxTester textBox = new TextBoxTester("myTextBox", form);
            textBox.Properties.Multiline = true;

            textBox.Enter("abc\nabcd\nabcde");

            Assert.AreEqual(
                @"[Test]
public void Test()
{

	TextBoxTester myTextBox = new TextBoxTester(""myTextBox"");

	myTextBox.Enter(""abc\nabcd\nabcde"");

}",
                writer.Test);
        }
    }
}