using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Savchin.UI
{

    public enum MessageResult
    {
        None = 0,
        OK = 1,
        Cancel = 2,
        Yes = 3,
        No = 4
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
    public static class MessageBox
    {
        public static MessageResult Show(string text, string caption, MessageButton button = MessageButton.OK, MessageImage icon = MessageImage.None, MessageResult defaultResult = MessageResult.OK)
        {
            var dialog = new MessageDialog(text, caption);
            switch (button)
            {
                case MessageButton.OK:
                    dialog.Commands.Add(new UICommand { Label = "Ok", Id = MessageResult.OK });
                    break;
                case MessageButton.OKCancel:
                    dialog.Commands.Add(new UICommand { Label = "Ok", Id = MessageResult.OK });
                    dialog.Commands.Add(new UICommand { Label = "Cancel", Id = MessageResult.Cancel });
                    break;
                case MessageButton.YesNo:
                    dialog.Commands.Add(new UICommand { Label = "Yes", Id = MessageResult.Yes });
                    dialog.Commands.Add(new UICommand { Label = "No", Id = MessageResult.No });
                    break;
                case MessageButton.YesNoCancel:
                    dialog.Commands.Add(new UICommand { Label = "Yes", Id = MessageResult.Yes });
                    dialog.Commands.Add(new UICommand { Label = "No", Id = MessageResult.No });
                    dialog.Commands.Add(new UICommand { Label = "Cancel", Id = MessageResult.Cancel });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }

            var res = dialog.ShowAsync();
            var command = res.GetResults();
            return (MessageResult)command.Id;
        }
    }
}
