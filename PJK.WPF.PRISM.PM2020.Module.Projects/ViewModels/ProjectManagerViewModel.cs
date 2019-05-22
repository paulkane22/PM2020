using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows.Data;



namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectManagerViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public DelegateCommand ShowAddProjectCommand { get; private set; }
        public DelegateCommand AddProjectCommand { get; private set; }
        public DelegateCommand<Project> EditProjectCommand { get; private set; }
        public DelegateCommand CancelAddProjectCommand { get; private set; }


        public ProjectManagerViewModel(IProjectDataService dataService, IEventAggregator eventAggregator)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            this.eventAggregator = eventAggregator;

            //Check if user is in design mode.
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            ShowAddProjectCommand = new DelegateCommand(OnShowAddProject);
            AddProjectCommand = new DelegateCommand(OnAddProject);
            EditProjectCommand = new DelegateCommand<Project>(OnEditProject);

            CancelAddProjectCommand = new DelegateCommand(OnCancelAddProject);

            this.Projects = new ListCollectionView(dataService.GetProjects());
        }

        private void OnEditProject(Project project)
        {
            ShowAddProject = true;
        }

        private void OnCancelAddProject()
        {
            ShowAddProject = false;
        }

        private void OnAddProject()
        {
            throw new NotImplementedException();
        }

        private void OnShowAddProject()
        {
            SelectedProject = new Project();
            ShowAddProject = true;
        }

        private Project _selectedProject;
        public Project SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value);}
        }

        public ICollectionView Projects { get; private set; }
        
        private bool _showAddProject = false;
        public bool ShowAddProject
        {
            get { return _showAddProject; }
            set { SetProperty(ref _showAddProject, value); }
        }

    }
}

