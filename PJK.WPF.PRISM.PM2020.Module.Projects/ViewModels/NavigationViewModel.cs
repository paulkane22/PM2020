using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Lookups;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{


    public class NavigationViewModel : BindableBase, INavigationViewModel
    {

        private IProjectLookupDataService _projectLookupDataService;
        private readonly IEventAggregator _eventAggregator;

        public NavigationViewModel(IProjectLookupDataService projectLookupDataService, IEventAggregator eventAggregator)
        {
            _projectLookupDataService = projectLookupDataService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AfterProjectSavedEvent>().Subscribe(AfterProjectSaved);

            Projects = new ObservableCollection<NavigationItemViewModel>();
        }

        private void AfterProjectSaved(AfterProjectSavedEventArgs obj)
        {
           var lookupItem =  Projects.Single(f => f.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _projectLookupDataService.GetProjectLookupAsync();
            Projects.Clear();
            foreach(var item in lookup)
            {
                Projects.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
            }

        }

        public ObservableCollection<NavigationItemViewModel> Projects { get; }

        private NavigationItemViewModel _selectedProject;
        public NavigationItemViewModel SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value);
                    if(_selectedProject != null)
                    {
                    _eventAggregator.GetEvent<OpenProjectDetailsViewEvent>().Publish(_selectedProject.Id);
                    }
            }
        }
    }
}
