using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
   

    public class NavigationViewModel : BindableBase, INavigationViewModel
    {

        private IProjectLookupDataService _projectLookupDataService;

        public NavigationViewModel(IProjectLookupDataService projectLookupDataService)
        {
            _projectLookupDataService = projectLookupDataService;
            Projects = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _projectLookupDataService.GetProjectLookupAsync();
            Projects.Clear();
            foreach(var item in lookup)
            {
                Projects.Add(item);
            }

        }



        public ObservableCollection<LookupItem> Projects { get; }
    }
}
