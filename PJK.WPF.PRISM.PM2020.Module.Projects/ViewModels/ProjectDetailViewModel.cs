using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectDetailViewModel : BindableBase, IProjectDetailViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private IProjectRepository _projectRepository;
        private ProjectWrapper _selectedProject;
        private int _id = 0;
        private bool _hasChanges;
        private bool _inEditMode;

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SaveDetailCommand { get; private set; }


        public ProjectDetailViewModel(IProjectRepository projectRepository, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _projectRepository = projectRepository;

            CancelCommand = new DelegateCommand(OnCancelExecute);
            SaveDetailCommand = new DelegateCommand(OnSaveDetailExecute, SaveDetailCanExecute);

            if (SelectedProject == null)
            {
               // CreateNewDetail();
            }
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
        }

        private async void OnSaveDetailExecute()
        {
            if(!InEditMode)
            {
                _projectRepository.Add(SelectedProject.Model);
                await _projectRepository.SaveAsync();
                
            }
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish();
            _eventAggregator.GetEvent<RefreshListEvent>().Publish();
        }

        private bool SaveDetailCanExecute()
        {
            return SelectedProject.HasErrors;
        }

        private void OnCancelExecute()
        {
            SelectedProject = null;
            _eventAggregator.GetEvent<CancelDetailEvent>().Publish();
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var lookup = await _projectRepository.GetByIdAsync(args.Id);
            SelectedProject = new ProjectWrapper(lookup);
        }

        public async Task LoadAsync(int id)
        {
            if(id == 0)
            {
                CreateNewDetail();
                int Id = SelectedProject.Id;
            }
            else
            {
                var lookup = await _projectRepository.GetByIdAsync(id);
                SelectedProject = new ProjectWrapper(lookup);
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set { SetProperty(ref _hasChanges, value); }
        }

        public bool InEditMode
        {
            get { return _inEditMode; }
            set { SetProperty(ref _inEditMode, value); }
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public ProjectWrapper SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); }
        }


        private void CreateNewDetail()
        {
            Project myProject = new Project
            {
                Id = 0,
                ProjectName = "<New Project 1>",
                SystemId = 0,
                Priority = 0,
                Deadline = System.DateTime.Now,
                StatusId = 0,
                Complete = false,
                Comment = ""
            };
            SelectedProject = new ProjectWrapper(myProject);
        }
    }
}
