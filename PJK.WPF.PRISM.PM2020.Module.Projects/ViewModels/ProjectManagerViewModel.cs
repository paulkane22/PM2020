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
using System.Windows.Data;



namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectManagerViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public DelegateCommand ShowAddProjectCommand { get; private set; }
        public DelegateCommand SaveProjectCommand { get; private set; }
        public DelegateCommand<ProjectWrapper> EditProjectCommand { get; private set; }
        public DelegateCommand CancelAddProjectCommand { get; private set; }


        public ProjectManagerViewModel(IProjectDataService dataService, IEventAggregator eventAggregator)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            this.eventAggregator = eventAggregator;

            //Check if user is in design mode.
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            ShowAddProjectCommand = new DelegateCommand(OnShowAddProject);
            SaveProjectCommand = new DelegateCommand(OnSaveProject, OnSaveCanExecute);
            EditProjectCommand = new DelegateCommand<ProjectWrapper>(OnEditProject);

            CancelAddProjectCommand = new DelegateCommand(OnCancelAddProject);

            // this.Projects = new ListCollectionView(dataService.GetProjects());

            ProjectWrapperService dsProjectWrapper = new ProjectWrapperService(dataService);

            this.Projects = dsProjectWrapper.ProjectWrappers;
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
                Projects.Add(SelectedProject);
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

        private ObservableCollection<ProjectWrapper> _projects;
        public ObservableCollection<ProjectWrapper> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        //public ICollectionView Projects { get; private set; }

        private bool _showAddProject = false;
        public bool ShowAddProject
        {
            get { return _showAddProject; }
            set { SetProperty(ref _showAddProject, value); }
        }





    }
}

