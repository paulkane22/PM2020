using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using Prism.Events;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectDetailViewModel : BindableBase, IProjectDetailViewModel
    {

        private readonly IEventAggregator _eventAggregator;
        private IProjectRepository _projectRepository;
        private Project _selectedProject;
        private int _id = 0;
        private bool _hasChanges;



        public ProjectDetailViewModel(IProjectRepository projectRepository, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _projectRepository = projectRepository;

            SelectedProject = new Project { Id = 0, ProjectName = "[N e w   P r o j e c t]", Deadline = System.DateTime.Now };

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
                var lookup = await _projectRepository.GetByIdAsync(args.Id);
                SelectedProject = lookup;
        }

        public async Task LoadAsync(int id)
        {
            var lookup = await _projectRepository.GetByIdAsync(id);
            SelectedProject = lookup;
        }


        public bool HasChanges
        {
            get { return _hasChanges; }
            set { SetProperty(ref _hasChanges, value); }
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); }
        }

    }
}
