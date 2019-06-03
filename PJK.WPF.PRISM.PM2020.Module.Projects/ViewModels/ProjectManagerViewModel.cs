using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PJK.WPF.PRISM.PM2020.DataAccess;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectManagerViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private IProjectRepository _projectRepository;
        private IProjectDetailViewModel _projectDetailViewModel;


        public DelegateCommand ShowAddProjectCommand { get; private set; }
        public DelegateCommand SaveProjectCommand { get; private set; }
        public DelegateCommand<ProjectWrapper> EditProjectCommand { get; private set; }
        public DelegateCommand CancelAddProjectCommand { get; private set; }


        public ProjectManagerViewModel(INavigationViewModel navigationViewModel, IProjectRepository projectRepository, IEventAggregator eventAggregator)
        {
            if (projectRepository == null) throw new ArgumentNullException("dataService");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            _eventAggregator = eventAggregator;
            _projectRepository = projectRepository;
            NavigationViewModel = navigationViewModel;
            


            //Check if user is in design mode.
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            ShowAddProjectCommand = new DelegateCommand(OnShowAddProject);
            SaveProjectCommand = new DelegateCommand(OnSaveProject, OnSaveCanExecute);
            EditProjectCommand = new DelegateCommand<ProjectWrapper>(OnEditProject);
            CancelAddProjectCommand = new DelegateCommand(OnCancelAddProject);

            // this.Projects = new ListCollectionView(dataService.GetProjects());

            _eventAggregator.GetEvent<OpenProjectDetailsViewEvent>().Subscribe(OnOpenProjectView);

        }

        public async Task LoadProjectsAsync()
        {
           await  NavigationViewModel.LoadAsync();
        }


        private bool OnSaveCanExecute()
        {
            //return true;
            return SelectedProject != null && !SelectedProject.HasErrors;
        }

        private void OnSaveProject()
        {
            if (!SelectedProject.HasErrors)
            {
   //             Projects.Add(SelectedProject);
            }
            ShowAddProject = false;
        }

        private void OnEditProject(ProjectWrapper project)
        {
            ShowAddProject = true;
        }

        private void OnCancelAddProject()
        {
            ShowAddProject = false;
        }


        private void OnShowAddProject()
        {
            Project myProject = new Project();
            myProject.ProjectName = "[New]";
            myProject.Id = 100;
            SelectedProject = new ProjectWrapper(myProject);
            SelectedProject.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedProject.HasErrors))
                {
                    ((DelegateCommand)SaveProjectCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveProjectCommand).RaiseCanExecuteChanged();

            ShowAddProject = true;
        }

        private ProjectWrapper  _selectedProject;
        public ProjectWrapper SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value);}

        }

        private bool _showAddProject = false;
        public bool ShowAddProject
        {
            get { return _showAddProject; }
            set { SetProperty(ref _showAddProject, value); }
        }

        public INavigationViewModel NavigationViewModel { get; }
        
        
        public IProjectDetailViewModel myProjectDetailViewModel
        {
            get { return _projectDetailViewModel; }
            set { SetProperty(ref _projectDetailViewModel, value); }
        }

        private async void OnOpenProjectView(int projectId)
        {
            //myProjectDetailViewModel = new ProjectDetailViewModel(new PM202DbContext);
            //await LoadAsync(projectId);
        }
    }
}

