using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Lookups;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectManagerViewModel : BindableBase
    {
        private IMessageDialogService _messageDialogService;
        private IEventAggregator _eventAggregator;
        private Func<IProjectDetailViewModel> _projectDetailViewModelCreator;
        private IProjectRepository _projectRepository;
        private IProjectDetailViewModel _projectDetailViewModel;
        private ISystemItemLookupDataService _systemItemLookupDataService;
        private Project myProject;

        public DelegateCommand ShowAddProjectCommand { get; private set; }
        public DelegateCommand CreateProjectCommand { get; private set; }
        public DelegateCommand SaveProjectCommand { get; private set; }
        public DelegateCommand<ProjectWrapper> EditProjectCommand { get; private set; }
        public DelegateCommand CancelAddProjectCommand { get; private set; }


        public ProjectManagerViewModel(INavigationViewModel navigationViewModel, 
                Func<IProjectDetailViewModel> projectDetailViewModelCreator, IProjectRepository projectRepository,
                IEventAggregator eventAggregator, IMessageDialogService messageDialogService, 
                ISystemItemLookupDataService systemItemLookupDataService)
        {
            //if (projectRepository == null) throw new ArgumentNullException("dataService");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;
            _projectDetailViewModelCreator = projectDetailViewModelCreator;
            _projectRepository = projectRepository;
            _systemItemLookupDataService = systemItemLookupDataService;
            NavigationViewModel = navigationViewModel;

            //Check if user is in design mode.
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            ShowAddProjectCommand = new DelegateCommand(OnShowAddProject);
            CreateProjectCommand = new DelegateCommand(OnCreateProject);
            SaveProjectCommand = new DelegateCommand(OnSaveProject, OnSaveCanExecute);
            EditProjectCommand = new DelegateCommand<ProjectWrapper>(OnEditProject);
            CancelAddProjectCommand = new DelegateCommand(OnCancelAddProject);

            SystemItems = new ObservableCollection<LookupItem>();

            // this.Projects = new ListCollectionView(dataService.GetProjects());

            _eventAggregator.GetEvent<OpenProjectDetailsViewEvent>().Subscribe(OnOpenProjectView);
            _eventAggregator.GetEvent<AfterProjectDeletedEvent>().Subscribe(OnAfterProjectDeleted);

        }


        public async Task LoadProjectsAsync()
        {
           await  NavigationViewModel.LoadAsync();
           await LoadSystemItemsLookupAsync();
        }


        private async Task LoadSystemItemsLookupAsync()
        {
            SystemItems.Clear();
            var lookup = await _systemItemLookupDataService.GetSystemItemLookupAsync();
            foreach (var lookupItem in lookup)
            {
                SystemItems.Add(lookupItem);
            }
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
        
        
        public IProjectDetailViewModel ProjectDetailViewModel
        {
            get { return _projectDetailViewModel; }
            set { SetProperty(ref _projectDetailViewModel, value); }
        }

        public bool HasChanges { get; private set; }

        private async void OnOpenProjectView(int projectId)
        {
            if (ProjectDetailViewModel!=null && ProjectDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("You've made changes. Navigate away?", "Question");
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            ProjectDetailViewModel = _projectDetailViewModelCreator();
            await ProjectDetailViewModel.LoadAsync(projectId);
        }

        private void OnShowAddProject()
        {
            myProject = new Project();
            myProject.ProjectName = "[New]";
            myProject.SystemId = 1;
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


        private void OnAfterProjectDeleted(int obj)
        {
            ProjectDetailViewModel = null;
        }

        private void OnCreateProject()
        {

        }

        private ObservableCollection<LookupItem> _systemItems;
        public ObservableCollection<LookupItem> SystemItems
        {
            get { return _systemItems; }
            set { SetProperty(ref _systemItems, value); }
        }


        #region "OldPopUpCode"

        private bool OnSaveCanExecute()
        {
            //return true;
            return SelectedProject != null && !SelectedProject.HasErrors;
        }

        private void OnSaveProject()
        {
            if (!SelectedProject.HasErrors)
            {
                _projectRepository.Add(myProject);
                HasChanges = _projectRepository.HasChanges();

                _projectRepository.SaveAsync();
               
                _eventAggregator.GetEvent<AfterProjectSavedEvent>().Publish(
                    new AfterProjectSavedEventArgs
                    {
                        Id = myProject.Id,
                        DisplayMember = myProject.ProjectName
                    }
                    );




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




        #endregion
    }
}

