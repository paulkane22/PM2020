using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Lookups;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectDetailViewModel : BindableBase, IProjectDetailViewModel
    {
        private IEventAggregator _eventAggregator;
        private IProjectRepository _projectRepository;
        private ISystemItemLookupDataService _systemItemLookupDataService;
        private ProjectWrapper _selectedProject;
        private int _id = 0;
        private bool _hasChanges;
        private bool _inEditMode;
        private string _title = "Add New Project";

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SaveDetailCommand { get; private set; }


        public ProjectDetailViewModel(
            IProjectRepository projectRepository, 
            IEventAggregator eventAggregator, 
            ISystemItemLookupDataService systemItemLookupDataService)
        {
            _eventAggregator = eventAggregator;
            _projectRepository = projectRepository;
            _systemItemLookupDataService = systemItemLookupDataService;

            CancelCommand = new DelegateCommand(OnCancelExecute);
            SaveDetailCommand = new DelegateCommand(OnSaveDetailExecute, SaveDetailCanExecute);

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);

            SystemItems = new ObservableCollection<LookupItem>();
        }

        private async void OnSaveDetailExecute()
        {
            if(!InEditMode)
            {
                _projectRepository.Add(SelectedProject.Model);
            }
            else
            {
                
            }

            await _projectRepository.SaveAsync();
            HasChanges = _projectRepository.HasChanges();
            InEditMode = false;

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish();
            _eventAggregator.GetEvent<RefreshListEvent>().Publish();
        }

        private bool SaveDetailCanExecute()
        {
            return SelectedProject != null && !SelectedProject.HasErrors && (HasChanges || !InEditMode);
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
            await LoadSelectedProjectAsync(id);
            await LoadSystemItemsLookupAsync();
            SaveDetailCommand.RaiseCanExecuteChanged();
        }

        private async Task LoadSelectedProjectAsync(int id)
        {
            if (id == 0)
            {
                CreateNewDetail();
                int Id = SelectedProject.Id;
            }
            else
            {
                var lookup = await _projectRepository.GetByIdAsync(id);
                SelectedProject = new ProjectWrapper(lookup);
            }

            SelectedProject.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _projectRepository.HasChanges();
                }
                if (e.PropertyName == nameof(SelectedProject.HasErrors))
                {
                    SaveDetailCommand.RaiseCanExecuteChanged();
                }
            };
        }

        private async Task LoadSystemItemsLookupAsync()
        {
            //LOAD SYSTEM LIST FOR DROPDOWN.
            SystemItems.Clear();
            SystemItems.Add(new NullLookupItem {DisplayMember = " - "});
            var lookup = await _systemItemLookupDataService.GetSystemItemLookupAsync();
            foreach (var lookupItem in lookup)
            {
                SystemItems.Add(lookupItem);
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set { SetProperty(ref _hasChanges, value);
                SaveDetailCommand.RaiseCanExecuteChanged();
            }
        }

        public bool InEditMode
        {
            get { return _inEditMode; }
            set {
                SetProperty(ref _inEditMode, value);
                if (value == true)
                {
                    Title = "Edit Project";
                }
                else
                {
                    Title = "Add New Project";
                }
            }
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }


        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title , value); }
        }




        public ProjectWrapper SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); }
        }

        public ObservableCollection<LookupItem> SystemItems { get; }

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
