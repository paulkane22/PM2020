using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectDetailViewModel : BindableBase, IProjectDetailViewModel
    {
        IProjectRepository _projectRepository;
        private IEventAggregator _eventAggregator;
        private ProjectWrapper _project;
        private bool _hasChanges;

        public DelegateCommand SaveProjectCommand { get; private set; }

        public ProjectDetailViewModel(IProjectRepository projectRepository, IEventAggregator eventAggregator)
        {
            _projectRepository = projectRepository;
            _eventAggregator = eventAggregator;
            SaveProjectCommand = new DelegateCommand(OnSaveProject, OnSaveCanExecute);
        }

        public async Task LoadAsync(int projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            Project = new ProjectWrapper(project);
            Project.PropertyChanged += (s, e) =>
            {
                if(!HasChanges)
                {
                    HasChanges = _projectRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Project.HasErrors))
                {
                    SaveProjectCommand.RaiseCanExecuteChanged();
                }
            };
            SaveProjectCommand.RaiseCanExecuteChanged();
        }

     
        public bool HasChanges
        {
            get { return _hasChanges; }
            set {
                if(_hasChanges != value)
                    {
                        SetProperty(ref _hasChanges, value);
                        SaveProjectCommand.RaiseCanExecuteChanged();
                    }
                }
        }


        public ProjectWrapper Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }

        private async void OnSaveProject()
        {
           await _projectRepository.SaveAsync();
            HasChanges = _projectRepository.HasChanges();
            _eventAggregator.GetEvent<AfterProjectSavedEvent>().Publish(
                new AfterProjectSavedEventArgs
                {
                    Id = Project.Id, 
                    DisplayMember = Project.ProjectName
                }
                );

        }

        private bool OnSaveCanExecute()
        {
            //return false;
            return Project != null && !Project.HasErrors && HasChanges;
        }

    }
}
