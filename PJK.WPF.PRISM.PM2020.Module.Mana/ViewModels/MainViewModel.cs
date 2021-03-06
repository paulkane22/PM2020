﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private ProjectSubtaskWrapper _SelectedProjectSubtask;

        public DelegateCommand EditDetailCommand { get; private set; }
        public DelegateCommand DeleteDetailCommand { get; private set; }
        public DelegateCommand SaveDetailCommand { get; private set; }
        public DelegateCommand CancelPopupCommand { get; private set; }
        public DelegateCommand AddProjectSubtaskCommand { get; private set; }
        public DelegateCommand RemoveProjectSubtaskCommand { get; private set; }


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
            AddProjectSubtaskCommand = new DelegateCommand(OnAddProjectSubtask);
            RemoveProjectSubtaskCommand = new DelegateCommand(OnRemoveProjectSubtaskExecute, CanRemoveProjectSubtaskExecute);

            _eventAggregator.GetEvent<EditDetailEvent>().Subscribe(OnEditDetailExecute);
            _eventAggregator.GetEvent<AddDetailEvent>().Subscribe(OnAddDetailExecute);
            _eventAggregator.GetEvent<DeleteDetailEvent>().Subscribe(OnDeleteDetailExecute);
            _eventAggregator.GetEvent<RefreshListEvent>().Subscribe(OnRefreshList);

            ComboPriority = new ObservableCollection<LookupItem>();
            ComboStatus = new ObservableCollection<LookupItem>();
            ComboSystemList = new ObservableCollection<LookupItem>();
            ProjectSubtasks = new ObservableCollection<ProjectSubtaskWrapper>();

        }

        public ObservableCollection<ProjectWrapper> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        public ObservableCollection<LookupItem> ComboPriority { get; }
        public ObservableCollection<LookupItem> ComboStatus { get; }
        public ObservableCollection<LookupItem> ComboSystemList { get; }
        public ObservableCollection<ProjectSubtaskWrapper> ProjectSubtasks { get; }

        public ProjectSubtaskWrapper SelectedProjectSubtask
        {
            get { return _SelectedProjectSubtask; }
            set {
                SetProperty(ref _SelectedProjectSubtask, value);
                RemoveProjectSubtaskCommand.RaiseCanExecuteChanged();
            }
        }

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
                    if(SelectedProject.Comment == null)
                    {
                        SelectedProject.Comment = "";
                    }
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
            set { SetProperty(ref _hasChanges, value);

                SaveDetailCommand.RaiseCanExecuteChanged();
            }
        }

        public bool InEditMode
        {
            get { return _inEditMode; }
            set
            {
                SetProperty(ref _inEditMode, value);
            }
        }


        private async void OnAddDetailExecute()
        {
            await  LoadDetailByIdAsync(0);
            
            ShowPopup = true;
            SaveDetailCommand.RaiseCanExecuteChanged();
        }

        private Project CreateNewDetail()
        {
            Project myProject = new Project
            {
                Id = 0,
                ProjectName = "<New Project 1>",
                SystemId = 1,
                Priority = 1,
                Deadline = System.DateTime.Now,
                StatusId = 1,
                Complete = false,
                Comment = "-"
            };

            return myProject;
        }

        private void OnAddProjectSubtask()
        {

            var newProjectSubtask = new ProjectSubtaskWrapper(new ProjectSubtask());
            newProjectSubtask.PropertyChanged += MyProjectSubtaskWrapper_PropertyChanged;

            ProjectSubtasks.Add(newProjectSubtask);
            SelectedProject.Model.ProjectSubtasks.Add(newProjectSubtask.Model);
            newProjectSubtask.Subtask = ""; //trigger validation
        }

        private void OnRemoveProjectSubtaskExecute()
        {
            SelectedProjectSubtask.PropertyChanged -= MyProjectSubtaskWrapper_PropertyChanged;
            //SelectedProject.Model.ProjectSubtasks.Remove(SelectedProjectSubtask.Model);
            _projectRepository.RemoveSubtask(SelectedProjectSubtask.Model);

            ProjectSubtasks.Remove(SelectedProjectSubtask);
            SelectedProjectSubtask = null;
            HasChanges = _projectRepository.HasChanges();
            SaveDetailCommand.RaiseCanExecuteChanged();

        }

        private bool CanRemoveProjectSubtaskExecute()
        {
            return _SelectedProjectSubtask != null;
        }

        private async void OnRefreshList()
        {
            var lookup = await _projectRepository.GetAllAsync();
            Projects.Clear();
            foreach (var item in lookup)
            {
                Projects.Add(new ProjectWrapper(item));
            }
        }

        private async void OnEditDetailExecute()
        { 
            if (SelectedProject != null)
            {
                await LoadDetailByIdAsync(SelectedProject.Id);

                InEditMode = true;
                ShowPopup = true;
            }
            //await LoadDetailByIdAsync(Sele);
        }

        private async void OnDeleteDetailExecute()
        {
            if (SelectedProject != null)
            {
                var value = _messageDialogService.ShowOKCancelDialog($"Do you really wish to delete the project {SelectedProject.ProjectName}?", "Delete Project?");
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
            if (SelectedProject != null && !SelectedProject.HasErrors)
            {
                // Projects.Add(SelectedProject);
                if (!InEditMode)
                {
                    _projectRepository.Add(SelectedProject.Model);
                }
                await _projectRepository.SaveAsync();
                HasChanges = _projectRepository.HasChanges();
                SelectedProject = null;
                ShowPopup = false;
                await LoadAsync();

            }
        }

        private bool CanSaveDetailExecute()
        {
            // return true;
            return SelectedProject != null 
                && !SelectedProject.HasErrors
                && HasChanges 
                && ProjectSubtasks.All(ps => !ps.HasErrors)
                ;
        }

        private void OnCancelPopup()
        {
            SelectedProject = null;
            InEditMode = false;
            ShowPopup = false;
        }

        #region "Load Methods"
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


            IntialiseProjectSubtasks(SelectedProject.Model.ProjectSubtasks);



            SaveDetailCommand.RaiseCanExecuteChanged();
        }
        private void IntialiseProjectSubtasks(ICollection<ProjectSubtask> projectSubtasks)
        {
            foreach (var wrapper in ProjectSubtasks)
            {
                wrapper.PropertyChanged -= MyProjectSubtaskWrapper_PropertyChanged;
            }
            ProjectSubtasks.Clear();
            foreach (var subtask in projectSubtasks)
            {
                var myProjectSubtaskWrapper = new ProjectSubtaskWrapper(subtask);
                ProjectSubtasks.Add(myProjectSubtaskWrapper);
                myProjectSubtaskWrapper.PropertyChanged += MyProjectSubtaskWrapper_PropertyChanged;
            }

        }

        private void MyProjectSubtaskWrapper_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _projectRepository.HasChanges();

            }
            if (e.PropertyName == nameof(ProjectSubtaskWrapper.HasErrors))
            {
                SaveDetailCommand.RaiseCanExecuteChanged();
            }


        }

        #endregion "Load Methods"

    }
}
