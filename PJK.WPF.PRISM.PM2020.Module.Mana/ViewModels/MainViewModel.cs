using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PJK.WPF.PRISM.PM2020.DataAccess;
using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Mana.Event;
using PJK.WPF.PRISM.PM2020.Module.Mana.Services;
using PJK.WPF.PRISM.PM2020.Module.Mana.Services.Lookups;
using PJK.WPF.PRISM.PM2020.Module.Mana.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Mana.Wrappers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.ViewModels
{
    public class MainViewModel : BindableBase
    {

        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IProjectRepository _projectRepository;
        private LookupDataService _lookupDataService;

        private NavigationListViewModel _navigationListViewModel;
        private ProjectDetailViewModel _projectDetailViewModel;
        private ObservableCollection<ProjectWrapper> _projects;
        private ProjectWrapper _selectedProject;
        
        private bool _showPopup;
        private bool _hasChanges;
        private bool _inEditMode;

        public DelegateCommand EditDetailCommand { get; private set; }
        public DelegateCommand DeleteDetailCommand { get; private set; }
        public DelegateCommand SaveDetailCommand { get; private set; }
        public DelegateCommand CancelPopupCommand { get; private set; }
        

        public MainViewModel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {

            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _projectRepository = new ProjectRepository(new PM202DbContext());
            _lookupDataService = new LookupDataService();

            _projects = new ObservableCollection<ProjectWrapper>();
            _navigationListViewModel = new NavigationListViewModel();
            _projectDetailViewModel = new ProjectDetailViewModel();

            EditDetailCommand = new DelegateCommand(OnEditDetailExecute);
            DeleteDetailCommand = new DelegateCommand(OnDeleteDetailExecute);
            SaveDetailCommand = new DelegateCommand(OnSaveDetailExecute, CanSaveDetailExecute);
            CancelPopupCommand = new DelegateCommand(OnCancelPopup);

            _eventAggregator.GetEvent<EditDetailEvent>().Subscribe(OnEditDetailExecute);
            _eventAggregator.GetEvent<AddDetailEvent>().Subscribe(OnAddDetailExecute);
            _eventAggregator.GetEvent<DeleteDetailEvent>().Subscribe(OnDeleteDetailExecute);

            ComboPriority = new ObservableCollection<LookupItem>();
            ComboStatus = new ObservableCollection<LookupItem>();
            ComboSystemList = new ObservableCollection<LookupItem>();
        }

        private void OnEditDetailExecute()
        {
            if(SelectedProject != null)
            {
                InEditMode = true;
                ShowPopup = true;
            }
            //await LoadDetailByIdAsync(Sele);
        }

        private async void OnDeleteDetailExecute()
        {
            if (SelectedProject != null)
            {
                var value = _messageDialogService.ShowOKCancelDialog($"Do you really wish to delete {SelectedProject.ProjectName}?", "Delete Project?");
                if (value == MessageDialogResult.OK)
                {
                    _projectRepository.Remove(SelectedProject.Model);
                    await _projectRepository.SaveAsync();
                    await LoadAsync();
                }
            }
        }

        private async void OnSaveDetailExecute()
        {
            if(SelectedProject != null)
            {
                // Projects.Add(SelectedProject);
                if (!InEditMode)
                {
                    _projectRepository.Add(SelectedProject.Model);
                }
                await _projectRepository.SaveAsync();
                HasChanges = _projectRepository.HasChanges();
                ShowPopup = false;
                await LoadAsync();
            }
        }

        private bool CanSaveDetailExecute()
        {
            // return true;
            return SelectedProject != null 
                 && !SelectedProject.HasErrors;
        }

        private void OnCancelPopup()
        {
            SelectedProject = null;
            InEditMode = false;
            ShowPopup = false;
        }

        public async Task LoadAsync()
        {
            var lookup = await _projectRepository.GetAllAsync();
            Projects.Clear();
            foreach (var item in lookup)
            {
                Projects.Add(new ProjectWrapper(item));
            }

            await LoadPriorityLookupAsync();
            await LoadStatusLookupAsync();
            await LoadSystemListLookupAsync();

        }

        private async Task LoadSystemListLookupAsync()
        {
            ComboSystemList.Clear();
            ComboSystemList.Add(new NullLookupItem { DisplayMember = " - " });
            var combolookup = await _lookupDataService.GetSystemItemLookupAsync();
            foreach (var item in combolookup)
            {
                ComboSystemList.Add(item);
            }
        }

        private async Task LoadStatusLookupAsync()
        {
            ComboStatus.Clear();
            ComboStatus.Add(new NullLookupItem { DisplayMember = " - " });
            var combolookup1 = await _lookupDataService.GetComboLookupAsync(ComboGroups.Status);
            foreach (var item in combolookup1)
            {
                ComboStatus.Add(item);
            }
        }

        private async Task LoadPriorityLookupAsync()
        {
            ComboPriority.Clear();
            ComboPriority.Add(new NullLookupItem { DisplayMember = " - " });
            var combolookup = await _lookupDataService.GetComboLookupAsync(ComboGroups.Priority);
            foreach (var item in combolookup)
            {
                ComboPriority.Add(item);
            }
        }

        private async Task LoadDetailByIdAsync(int id)
        {
            if (id == 0)
            {
               SelectedProject = new ProjectWrapper(CreateNewDetail());
            }
            else
            {
                var lookup = await _projectRepository.GetByIdAsync(id);
                SelectedProject = new ProjectWrapper(lookup);
            }
        }


        public ProjectDetailViewModel ProjectDetailViewModel
        {
            get { return _projectDetailViewModel; }
            set { SetProperty(ref _projectDetailViewModel, value); }
        }

        public ObservableCollection<ProjectWrapper> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        public ObservableCollection<LookupItem> ComboPriority { get; }
        public ObservableCollection<LookupItem> ComboStatus { get; }
        public ObservableCollection<LookupItem> ComboSystemList { get; }

        public ProjectWrapper SelectedProject
        {
            get { return _selectedProject; }
            set {
                SetProperty(ref _selectedProject, value);
                if(SelectedProject != null)
                {
                    SelectedProject.PropertyChanged += (s, e) =>
                    {
                        SaveDetailCommand.RaiseCanExecuteChanged();
                    };
                }
                SaveDetailCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ShowPopup
        {
            get { return _showPopup; }
            set { SetProperty(ref _showPopup, value); }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set { SetProperty(ref _hasChanges, value); }
        }

        private async void OnAddDetailExecute()
        {
            await  LoadDetailByIdAsync(0);
            ShowPopup = true;
        }

        private Project CreateNewDetail()
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

            return myProject;
        }

        public bool InEditMode
        {
            get { return _inEditMode; }
            set
            {
                SetProperty(ref _inEditMode, value);
            }
        }


    }
}
