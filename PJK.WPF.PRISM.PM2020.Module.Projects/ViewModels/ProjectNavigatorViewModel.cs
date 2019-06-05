using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectNavigatorViewModel : BindableBase, INavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private IProjectRepository _projectRepository;
        private NavigationItemViewModel _selectedProject;

        public DelegateCommand CreateProjectCommand { get; private set; }

        public ProjectNavigatorViewModel(IProjectRepository projectRepository, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _projectRepository = projectRepository;

            Projects = new ObservableCollection<NavigationItemViewModel>();
            CreateProjectCommand = new DelegateCommand(CreateProject);

        }

        private async void CreateProject()
        {
            _projectRepository.Add(new Project { ProjectName = "[new]", Deadline = new System.DateTime(2019,10,10) });
            await _projectRepository.SaveAsync();
            await LoadAsync();
        }


        public async Task LoadAsync()
        {
            var lookup = await _projectRepository.GetAllAsync();
            Projects.Clear();
            foreach (var item in lookup)
            {
                Projects.Add(new NavigationItemViewModel(item.Id, item.ProjectName, _eventAggregator));
            }
        }

        public ObservableCollection<NavigationItemViewModel> Projects { get; set; }

        public NavigationItemViewModel SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); }
        }
    }
}
