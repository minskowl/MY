using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Savchin.Wpf.Controls
{
    public enum MessageResult
    {
        None,
        OK,
        Cancel,
        Yes,
        No
    }
    public enum MessageImage
    {
        None,
        Information,
        Question,
        Exclamation,
        Error,
        Stop,
        Warning
    }
    public enum MessageButton
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    /// <summary>
    /// WpfMessageBox
    /// </summary>
    public partial class WpfMessageBox
    {
        public static MessageResult mMessageResult;
        public MessageButton mMessageButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="WpfMessageBox"/> class.
        /// </summary>
        public WpfMessageBox()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Shows the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        public static MessageResult Show(string text, string caption)
        {
            return Show(text, caption, MessageButton.OK, MessageImage.None, MessageResult.OK);
        }

        /// <summary>
        /// Shows the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultResult">The default result.</param>
        /// <returns></returns>
        public static MessageResult Show(string text, string caption, MessageButton button, MessageImage icon, MessageResult defaultResult)
        {
            var msg = new WpfMessageBox
            {
                MessageTitle = { Text = caption },

            };
            msg.SetVisibility(button);
            msg.Initialize(icon, text);
            msg.SetDefaultResult(defaultResult);
            msg.ShowDialog();
            return mMessageResult;
        }



        private void SetVisibility(MessageButton button)
        {
            mMessageButton = button;
            if (button == MessageButton.OK)
            {
                btnOk.IsDefault = true;
                btnCancel.Visibility = Visibility.Collapsed;
                btnYes.Visibility = Visibility.Collapsed;
                btnNo.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Visible;
            }
            else if (button == MessageButton.OKCancel)
            {
                btnOk.IsDefault = true;
                btnCancel.Visibility = Visibility.Visible;
                btnYes.Visibility = Visibility.Collapsed;
                btnNo.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Visible;
            }
            else if (button == MessageButton.YesNo)
            {
                btnYes.IsDefault = true;
                btnCancel.Visibility = Visibility.Collapsed;
                btnYes.Visibility = Visibility.Visible;
                btnNo.Visibility = Visibility.Visible;
                btnOk.Visibility = Visibility.Collapsed;
            }
            else if (button == MessageButton.YesNoCancel)
            {
                btnYes.IsDefault = true;
                btnCancel.Visibility = Visibility.Visible;
                btnYes.Visibility = Visibility.Visible;
                btnNo.Visibility = Visibility.Visible;
                btnOk.Visibility = Visibility.Collapsed;
            }
        }

        private void SetDefaultResult(MessageResult defaultResult)
        {
            if (defaultResult == MessageResult.Yes)
            {
                btnYes.IsDefault = true;
                btnYes.Focus();
                btnNo.IsDefault = false;
                btnCancel.IsDefault = false;
                btnOk.IsDefault = false;
            }
            else if (defaultResult == MessageResult.No)
            {
                btnNo.IsDefault = true;
                btnNo.Focus();
                btnYes.IsDefault = false;
                btnCancel.IsDefault = false;
                btnOk.IsDefault = false;
            }
            else if (defaultResult == MessageResult.Cancel)
            {
                btnCancel.IsDefault = true;
                btnCancel.Focus();
                btnYes.IsDefault = false;
                btnNo.IsDefault = false;
                btnOk.IsDefault = false;
            }
            else if (defaultResult == MessageResult.OK)
            {
                btnOk.IsDefault = true;
                btnOk.Focus();
                btnYes.IsDefault = false;
                btnNo.IsDefault = false;
                btnCancel.IsDefault = false;
            }
        }

        private void Initialize(MessageImage image, string text)
        {
            var uriImage = GetImageUri(image);
            if (string.IsNullOrWhiteSpace(uriImage))
            {
                imgMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                imgMessage.Source =  new BitmapImage(new Uri(uriImage, UriKind.Relative));

                if (GetTextWidth(text) > (MinWidth - 60))
                {
                    MessageBlock.Padding = new Thickness(25, 0, 0, 0);
                }
            }

            MessageBlock.Text = text;
        }
        private double GetTextWidth(string text)
        {
            return new FormattedText(text, CultureInfo.CurrentCulture,
                                     System.Windows.FlowDirection.LeftToRight,
                                     new Typeface(this.MessageBlock.FontFamily.Source),
                                     this.MessageBlock.FontSize, Brushes.Black).Width;
        }

        private string GetImageUri(MessageImage image)
        {
            switch (image)
            {
                case MessageImage.Error:
                    return "Images\\Error.png";
                case MessageImage.Warning:
                    return "Images\\Warning.png";
                case MessageImage.Question:
                    return "Images\\Question.png";
                case MessageImage.Information:
                    return "Images\\Information.png";
                default:
                    return null;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            mMessageResult = MessageResult.OK;
            DialogResult = true;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            mMessageResult = MessageResult.Yes;
            DialogResult = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            mMessageResult = MessageResult.No;
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mMessageResult = MessageResult.Cancel;
            DialogResult = true;
        }

        private void WpfMessageBox_Name_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mMessageButton == MessageButton.YesNo)
            {
                if (mMessageResult != MessageResult.Yes && mMessageResult != MessageResult.No)
                    e.Cancel = true;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }


    }
}