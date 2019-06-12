using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public interface INavigationViewModel
    {
        Task LoadAsync();
        int SelectedDetailId { get; }
    }
}