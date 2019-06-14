using System.Windows;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOKCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK ? MessageDialogResult.OK : MessageDialogResult.Cancel;
        }
    }

    public enum MessageDialogResult
    {
        OK,
        Cancel
    }
}
