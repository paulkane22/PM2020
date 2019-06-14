namespace PJK.WPF.PRISM.PM2020.Module.Mana.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOKCancelDialog(string text, string title);
    }
}