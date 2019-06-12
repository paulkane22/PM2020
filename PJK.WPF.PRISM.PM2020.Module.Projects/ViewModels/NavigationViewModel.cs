using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Lookups;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{


    public class NavigationViewModel : BindableBase, INavigationViewModel
    {

        private IProjectLookupDataService _projectLookupDataService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        public NavigationViewModel(IProjectLookupDataService projectLookupDataService, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _projectLookupDataService = projectLookupDataService;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            
            _eventAggregator.GetEvent<AfterProjectSavedEvent>().Subscribe(AfterProjectSaved);
            _eventAggregator.GetEvent<AfterProjectDeletedEvent>().Subscribe(AfterProjectDeleted);

            Projects = new ObservableCollection<NavigationItemViewModel>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _projectLookupDataService.GetProjectLookupAsync();
            Projects.Clear();
            foreach(var item in lookup)
            {
                Projects.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }

        }

        public ObservableCollection<NavigationItemViewModel> Projects { get; }


        private void AfterProjectDeleted(int projectId)
        {
            var project = Projects.SingleOrDefault(f => f.Id == projectId);
            if(project != null)
            {
                Projects.Remove(project);
            }

        }

        private void AfterProjectSaved(AfterProjectSavedEventArgs project)
        {
            var lookupItem = Projects.SingleOrDefault(f => f.Id == project.Id);

            if (lookupItem == null)
            {
                Projects.Add(new NavigationItemViewModel(project.Id, project.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = project.DisplayMember;
            }
        }
    }
}
