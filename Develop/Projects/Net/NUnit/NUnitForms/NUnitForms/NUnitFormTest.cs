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

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Extensions.Forms.SendKey;
using NUnit.Extensions.Forms.Win32Interop;
using NUnit.Framework;

namespace NUnit.Extensions.Forms
{
    /// <summary>
    /// One of three base classes for your NUnitForms tests.  This one can be
    /// used by people who do not need or want "built-in" Assert functionality.
    ///
    /// This is the recommended base class for all unit tests that use NUnitForms.  
    /// </summary>
    /// <remarks>
    /// You should probably extend this class to create all of your test fixtures.  The benefit is that
    /// this class implements setup and teardown methods that clean up after your test.  Any forms that
    /// are created and displayed during your test are cleaned up by the tear down method.  This base
    /// class also provides easy access to keyboard and mouse controllers.  It has a method that allows
    /// you to set up a handler for modal dialog boxes.  It allows your tests to run on a separate 
    /// (usually hidden) desktop so that they are faster and do not interfere with your normal desktop
    /// activity.  If you want custom setup and teardown behavior, you should override the virtual 
    /// Setup and TearDown methods.  Do not use the setup and teardown attributes in your child class.
    /// </remarks>
    public class NUnitFormTest
    {
        private static readonly FieldInfo isUserInteractive =
            typeof (SystemInformation).GetField("isUserInteractive", BindingFlags.Static | BindingFlags.NonPublic);

        private KeyboardController keyboard = null;
        private ModalFormTester modal;
        private MouseController mouse = null;

        private Desktop testDesktop;

        /// <summary>
        /// True if the modal handlers for this test have been verified; else false.
        /// </summary>
        /// <remarks>
        /// It would be better form to make this private and provide a protected getter property, though
        /// that could break existing tests.
        /// </remarks>
        protected bool verified = false;

        /// <summary>
        /// This property controls whether the separate hidden desktop is displayed for the duration of
        /// this test.  You will need to override and return true from this property if your test makes
        /// use of the keyboard or mouse controllers.  (The hidden desktop cannot accept user input.)  For
        /// tests that do not use the keyboard and mouse controller (most should not) you don't need to do
        /// anything with this.  The default behavior is fine.
        /// </summary>
        public virtual bool DisplayHidden
        {
            get { return false; }
        }

        /// <summary>
        /// This property controls whether a separate desktop is used at all.  I highly recommend that you
        /// leave this as returning true.  Tests on the separate desktop are faster and safer.  (There is 
        /// no danger of keyboard or mouse input going to your own separate running applications.)  However
        /// I have heard report of operating systems or environments where the separate desktop does not work.
        /// In that case there are 2 options.  You can override this method from your test class to return false.
        /// Or you can set an environment variable called "UseHiddenDesktop" and set that to "false"  Either will
        /// cause the tests to run on your original, standard desktop. 
        /// </summary>
        public virtual bool UseHidden
        {
            get
            {
                string useHiddenDesktop = Environment.GetEnvironmentVariable("UseHiddenDesktop");
                if (useHiddenDesktop != null && useHiddenDesktop.ToUpper().Equals("FALSE"))
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Returns a reference to the current MouseController for doing Mouse tests.  I recommend
        /// this only when you are writing your own custom controls and need to respond to actual
        /// mouse input to test them properly.  In most other cases there is a better way to test
        /// the form's logic.
        /// </summary>
        public MouseController Mouse
        {
            get { return mouse; }
        }

        /// <summary>
        /// Returns a reference to the current KeyboardController for doing Keyboard tests.  I recommend
        /// this only when you are writing your own custom controls and need to respond to actual
        /// keyboard input to test them properly.  In most other cases there is a better way to test
        /// for the form's logic.
        /// </summary>
        public KeyboardController Keyboard
        {
            get { return keyboard; }
        }

        /// <summary>
        /// This is the base classes setup method.  It will be called by NUnit before each test.
        /// You should not have anything to do with it.
        /// </summary>
        [SetUp]
        public void init()
        {
            verified = false;

            if (!SystemInformation.UserInteractive)
            {
                isUserInteractive.SetValue(null, true);
            }

            if (UseHidden)
            {
                testDesktop = new Desktop("NUnitForms Test Desktop", DisplayHidden);
            }

            modal = new ModalFormTester();
            mouse = new MouseController();
            keyboard = new KeyboardController(new OldSendKeysFactory());

            Setup();
        }

        /// <summary>
        /// A patch method to allow migration to an alternative SendKeys class instead
        /// of the dot Net SendKeys class. Once the new class is completed this method
        /// will be replaced by a method to allow use of the dot Net class.
        /// 
        /// This method must only be called at the start of the test fixture's overriden
        /// SetUp().
        /// </summary>
        protected void EmulateSendKeys()
        {
            keyboard =
                new KeyboardController(new SendKeysFactory(new SendKeysParserFactory(), new SendKeyboardInput()));
        }

        /// <summary>
        /// A patch method to allow migration to an alternative SendKeys class instead
        /// of the dot Net SendKeys class. Once the new class is completed this method
        /// will be replaced by a method to allow use of the dot Net class.
        /// 
        /// This method must only be called at the start of the test fixture's overriden
        /// SetUp().
        /// </summary>
        protected void EmulateWindowSpecificSendKeys()
        {
            keyboard =
                new KeyboardController(
                    new SendKeysFactory(new SendKeysParserFactory(), new WindowSpecificSendKeyboardInput()));
        }

        /// <summary>
        /// This method is needed because the way the FileDialogs working are strange.
        /// It seems that both open/save dialogs initial title is "Open". The handler
        /// </summary>
        protected void ExpectFileDialog(string modalHandler)
        {
            ExpectModal("Open", modalHandler);
        }

        /// <summary>
        /// This method is needed because the way the FileDialogs working are strange.
        /// It seems that both open/save dialogs initial title is "Open". The handler
        /// </summary>
        protected void ExpectFileDialog(string modalHandler, bool expected)
        {
            ExpectModal("Open", modalHandler, expected);
        }

        /// <summary>
        /// This method is needed because the way the FileDialogs working are strange.
        /// It seems that both open/save dialogs initial title is "Open". The handler
        /// </summary>
        protected void ExpectFileDialog(ModalFormActivated handler)
        {
            modal.ExpectModal("Open", handler, true);
        }

        /// <summary>
        /// This method is needed because the way the FileDialogs working are strange.
        /// It seems that both open/save dialogs initial title is "Open". The handler
        /// </summary>
        protected void ExpectFileDialog(ModalFormActivated handler, bool expected)
        {
            modal.ExpectModal("Open", handler, true);
        }

        /// <summary>
        /// One of four overloaded methods to set up a modal dialog handler.  If you expect a modal
        /// dialog to appear and can handle it during the test, use this method to set up the handler.
        /// </summary>
        /// <param name="name">The caption on the dialog you expect.</param>
        /// <param name="handler">The method to call when that dialog appears.</param>
        protected void ExpectModal(string name, ModalFormActivated handler)
        {
            modal.ExpectModal(name, handler, true);
        }

        /// <summary>
        /// One of four overloaded methods to set up a modal dialog handler.  If you expect a modal
        /// dialog to appear and can handle it during the test, use this method to set up the handler.
        /// Because "expected" is usually (always) true if you are calling this, I don't expect it will
        /// be used externally.
        /// </summary>
        /// <param name="name">The caption on the dialog you expect.</param>
        /// <param name="handler">The method to call when that dialog appears.</param>
        /// <param name="expected">A boolean to indicate whether you expect this modal dialog to appear.</param>
        protected void ExpectModal(string name, ModalFormActivated handler, bool expected)
        {
            modal.ExpectModal(name, handler, expected);
        }

        /// <summary>
        /// One of four overloaded methods to set up a modal dialog handler.  If you expect a modal
        /// dialog to appear and can handle it during the test, use this method to set up the handler.
        /// Because "expected" is usually (always) true if you are calling this, I don't expect it will
        /// be used externally.
        /// </summary>
        /// <param name="name">The caption on the dialog you expect.</param>
        /// <param name="handlerName">The name of the method to call when that dialog appears.</param>
        /// <param name="expected">A boolean to indicate whether you expect this modal dialog to appear.</param>
        protected void ExpectModal(string name, string handlerName, bool expected)
        {
            ExpectModal(name,
                        (ModalFormActivated) Delegate.CreateDelegate(typeof (ModalFormActivated), this, handlerName),
                        expected);
        }

        /// <summary>
        /// One of four overloaded methods to set up a modal dialog handler.  If you are not sure which
        /// to use, use this one.  If you expect a modal dialog to appear and can handle it during the
        /// test, use this method to set up the handler. Because "expected" is usually (always) true 
        /// if you are calling this, I don't expect it will be used externally.
        /// </summary>
        /// <param name="name">The caption on the dialog you expect.</param>
        /// <param name="handlerName">The name of the method to call when that dialog appears.</param>
        protected void ExpectModal(string name, string handlerName)
        {
            ExpectModal(name, handlerName, true);
        }

        /// <summary>
        /// Override this Setup method if you have custom behavior to execute before each test
        /// in your fixture.
        /// </summary>
        public virtual void Setup()
        {
        }

        /// <summary>
        /// This method is called by NUnit after each test runs.  If you have custom
        /// behavior to run after each test, then override the TearDown method and do
        /// it there.  That method is called at the beginning of this one.
        /// You should not need to do anything with it.  Do not call it.
        /// If you do call it, call it as the last thing you do in your test.
        /// </summary>
        [TearDown]
        public void Verify()
        {
            TearDown();

            if (!verified)
            {
                verified = true;
                List<Form> allForms = new FormFinder().FindAll();

                foreach (Form form in allForms)
                {
                    if (!KeepAlive.ShouldKeepAlive(form))
                    {
                        form.Dispose();
                        form.Hide();
                    }
                }

                string[] errors = new string[0];
                bool modalVerify = modal.Verify();
                if (!modalVerify)
                {
                    errors = modal.GetErrors();
                }
                modal.Dispose();

                if (UseHidden)
                {
                    testDesktop.Dispose();
                }

                mouse.Dispose();
                keyboard.Dispose();

                if (!modalVerify)
                {
                    string message = "";
                    foreach (string m in errors)
                    {
                        message += m + ((errors.Length > 1) ? "\r\n" : "");
                    }
                    throw new FormsTestAssertionException(message);
                }
            }
        }

        /// <summary>
        /// Override this TearDown method if you have custom behavior to execute after each test
        /// in your fixture.
        /// </summary>
        public virtual void TearDown()
        {
        }
    }
}