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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectDetailViewModel : BindableBase, IProjectDetailViewModel
    {
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IProjectRepository _projectRepository;
        private ISystemItemLookupDataService _systemItemLookupDataService;
        private ProjectWrapper _selectedProject;

        private int _id = 0;
        private bool _hasChanges;
        private bool _inEditMode;
        private string _title = "Add New Project";
        private SubTaskWrapper _selectedSubtask;

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SaveDetailCommand { get; private set; }
        public DelegateCommand AddSubtaskCommand { get; private set; }
        public DelegateCommand RemoveSubtaskCommand { get; private set; }

        public ProjectDetailViewModel(
            IProjectRepository projectRepository, 
            IEventAggregator eventAggregator, 
            ISystemItemLookupDataService systemItemLookupDataService, 
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _projectRepository = projectRepository;
            _systemItemLookupDataService = systemItemLookupDataService;

            CancelCommand = new DelegateCommand(OnCancelExecute);
            SaveDetailCommand = new DelegateCommand(OnSaveDetailExecute, SaveDetailCanExecute);
            AddSubtaskCommand = new DelegateCommand(OnAddSubtaskExecute);
            RemoveSubtaskCommand = new DelegateCommand(OnRemoveSubtaskExecute, OnRemoveSubtaskCanExecute);

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);

            SystemItems = new ObservableCollection<LookupItem>();
            Subtasks = new ObservableCollection<SubTaskWrapper>();

        }

        private bool OnRemoveSubtaskCanExecute()
        {
            return SelectedSubtask != null;
        }

        private void OnRemoveSubtaskExecute()
        {
            throw new NotImplementedException();
        }

        private void OnAddSubtaskExecute()
        {
            throw new NotImplementedException();
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
            
            
            
            
            
            //TODO - FIX CACHING ISSUE
            var localRefreshProject = _projectRepository.GetByIdAsync(SelectedProject.Id);
            //SelectedProject = 


            InEditMode = false;



            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish();
            _eventAggregator.GetEvent<RefreshListEvent>().Publish();
        }

        private bool SaveDetailCanExecute()
        {
            return SelectedProject != null 
                && !SelectedProject.HasErrors 
                && Subtasks.All(pn=> !pn.HasErrors)
                && (HasChanges || !InEditMode);
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

            InitialiseProjectSubtasks(_selectedProject.Model.Subtasks);

            await LoadSystemItemsLookupAsync();
            SaveDetailCommand.RaiseCanExecuteChanged();
        }

        private void InitialiseProjectSubtasks(ICollection<ProjectSubtask> subtasks)
        {
            foreach (var wrapper in Subtasks)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            Subtasks.Clear();
            foreach (var projectSubtask in subtasks)
            {
                var wrapper = new SubTaskWrapper(projectSubtask);
                Subtasks.Add(wrapper);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
         
            }
        }

        private void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           if(!HasChanges)
            {
                HasChanges = _projectRepository.HasChanges();
            }
           if(e.PropertyName == nameof(SubTaskWrapper.HasErrors))
            {
                SaveDetailCommand.RaiseCanExecuteChanged();
            }
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


 
        public SubTaskWrapper SelectedSubtask
        {
            get { return _selectedSubtask; }
            set {
                SetProperty(ref _selectedSubtask, value);
                RemoveSubtaskCommand.RaiseCanExecuteChanged();
            }
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
        public ObservableCollection<SubTaskWrapper> Subtasks { get; }

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
