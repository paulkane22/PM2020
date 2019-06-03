using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using Prism.Mvvm;
using PJK.WPF.PRISM.PM2020.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Lookups;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectManagerListViewModel : BindableBase
    {
        private IProjectLookupDataService _projectLookupDataService;

        public ProjectManagerListViewModel(IProjectLookupDataService projectLookupDataService)
        {
            _projectLookupDataService = projectLookupDataService;
            Projects = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _projectLookupDataService.GetProjectLookupAsync();
            Projects.Clear();
            foreach (var item in lookup)
            {
                Projects.Add(item);
            }

        }

        public ObservableCollection<LookupItem> Projects { get; }
    }
}
