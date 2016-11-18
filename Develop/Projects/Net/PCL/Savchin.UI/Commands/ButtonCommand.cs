using System;

namespace CITrader.Controls.Commands
{
    public class ButtonCommand : DelegateCommandEx
    {
        #region Construction


        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonCommand"/> class.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <param name="isCancel">if set to <c>true</c> [is cancel].</param>
        public ButtonCommand(string caption, Action executeMethod, bool isDefault = false, bool isCancel = false)
            : base(executeMethod)
        {
            Caption = caption;
            IsDefault = isDefault;
            IsCancel = isCancel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonCommand"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        public ButtonCommand(Action executeMethod)
            : base(executeMethod)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonCommand"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        public ButtonCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>The caption.</value>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancel.
        /// </summary>
        /// <value><c>true</c> if this instance is cancel; otherwise, <c>false</c>.</value>
        public bool IsCancel { get; set; }

        #endregion
    }

    public class ButtonCommand<T> : DelegateCommandEx<T>
    {
        #region Construction


        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonCommand"/> class.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <param name="isCancel">if set to <c>true</c> [is cancel].</param>
        public ButtonCommand(string caption, Action<T> executeMethod, bool isDefault = false, bool isCancel = false)
            : base(executeMethod)
        {
            Caption = caption;
            IsDefault = isDefault;
            IsCancel = isCancel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonCommand"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        public ButtonCommand(Action<T> executeMethod)
            : base(executeMethod)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonCommand"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        public ButtonCommand(Action<T> executeMethod, Func<T,bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>The caption.</value>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancel.
        /// </summary>
        /// <value><c>true</c> if this instance is cancel; otherwise, <c>false</c>.</value>
        public bool IsCancel { get; set; }

        #endregion
    }
}