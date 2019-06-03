namespace PJK.WPF.PRISM.PM2020.Module.Projects.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOKCancelDialog(string text, string title);
    }
}