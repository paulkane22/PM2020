using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
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
        private readonly IEventAggregator _eventAggregator;
        private ProjectWrapper _project;

        public DelegateCommand SaveProjectCommand { get; private set; }

        public ProjectDetailViewModel(IProjectRepository projectRepository, IEventAggregator eventAggregator)
        {
            _projectRepository = projectRepository;


            SaveProjectCommand = new DelegateCommand(OnSaveProject, OnSaveCanExecute);
        }

        public async Task LoadAsync(int projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            myProject = new ProjectWrapper(project);
            myProject.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(myProject.HasErrors))
                {
                    SaveProjectCommand.RaiseCanExecuteChanged();
                }
            };

            SaveProjectCommand.RaiseCanExecuteChanged();
        }

        public ProjectWrapper myProject
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }

        private async void OnSaveProject()
        {
           await _projectRepository.SaveAsync();
            _eventAggregator.GetEvent<AfterProjectSavedEvent>().Publish(
                new AfterProjectSavedEventArgs { Id = myProject.Id, DisplayMember=myProject.ProjectName}
                
                );



        }

        private bool OnSaveCanExecute()
        {
            return myProject != null && !myProject.HasErrors;
        }



    }
}
