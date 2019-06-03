using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
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
