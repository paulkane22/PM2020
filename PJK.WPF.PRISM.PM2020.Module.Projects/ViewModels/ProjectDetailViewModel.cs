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
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectDetailViewModel : BindableBase, IProjectDetailViewModel
    {
        IProjectRepository _projectRepository;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private ISystemItemLookupDataService _systemItemLookupDataService;

        private ProjectWrapper _project;
        private bool _hasChanges;

        public DelegateCommand SaveProjectCommand { get; private set; }
        public DelegateCommand DeleteProjectCommand { get; private set; }
        public DelegateCommand AddSubtaskCommand { get; private set; }
        public DelegateCommand RemoveSubtaskCommand { get; private set; }

        public ObservableCollection<LookupItem> SystemItems { get; }
        public ObservableCollection<SubTaskWrapper> SubTasks { get; }


        public ProjectDetailViewModel(IProjectRepository projectRepository, 
            IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService, 
            ISystemItemLookupDataService systemItemLookupDataService)
        {
            _projectRepository = projectRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _systemItemLookupDataService = systemItemLookupDataService;

            SaveProjectCommand = new DelegateCommand(OnSaveProject, OnSaveCanExecute);
            DeleteProjectCommand = new DelegateCommand(OnDeleteProject, OnDeleteCanExecute);
            AddSubtaskCommand = new DelegateCommand(OnAddSubtask);
            RemoveSubtaskCommand = new DelegateCommand(OnRemoveSubtask, OnRemoveSubtaskCanExecute);

            SystemItems = new ObservableCollection<LookupItem>();
            SubTasks = new ObservableCollection<SubTaskWrapper>();

        }


        private SubTaskWrapper _selectedSubtask;
        public SubTaskWrapper SelectedSubtask
        {
            get { return _selectedSubtask; }
            set {
                SetProperty(ref _selectedSubtask, value);
                RemoveSubtaskCommand.RaiseCanExecuteChanged();
            }
        }






        private async void OnDeleteProject()
        {
            var result = _messageDialogService.ShowOKCancelDialog($"Do you really want to delete this project?", "Question");
            if(result == MessageDialogResult.OK)
            {
                _projectRepository.Remove(Project.Model);
                await _projectRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterProjectDeletedEvent>().Publish(Project.Id);
            }

        }

        private bool OnDeleteCanExecute()
        {
            return true;
        }

        public async Task LoadAsync(int projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);

            InitialiseProject(project);
            InitialiseSubtasksAsync(project.SubTasks);
            await LoadSystemItemsLookupAsync();
        }

        private void InitialiseProject(Project project)
        {
            Project = new ProjectWrapper(project);
            Project.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _projectRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Project.HasErrors))
                {
                    SaveProjectCommand.RaiseCanExecuteChanged();
                }
            };
            SaveProjectCommand.RaiseCanExecuteChanged();
        }


        private void InitialiseSubtasksAsync(ICollection<ProjectSubtask> projectSubtasks)
        {
            SubTasks.Clear();
            foreach(var projectSubtask in projectSubtasks)
            {
                var wrapper = new SubTaskWrapper(projectSubtask);
                SubTasks.Add(wrapper);
                wrapper.PropertyChanged += Wrapper_PropertyChanged1;
            }
        }

        private void Wrapper_PropertyChanged1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           if(!HasChanges)
            {
                HasChanges = _projectRepository.HasChanges();
            }
           if(e.PropertyName == nameof(SubTaskWrapper.HasErrors))
            {
                SaveProjectCommand.RaiseCanExecuteChanged();
            }



        }

        private async Task LoadSystemItemsLookupAsync()
        {
            SystemItems.Clear();
            SystemItems.Add(new NullLookupItem {DisplayMember = " - " });
            var lookup = await _systemItemLookupDataService.GetSystemItemLookupAsync();
            foreach (var lookupItem in lookup)
            {
                SystemItems.Add(lookupItem);
            }
        }


        private void Wrapper_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(!HasChanges)
            {
                HasChanges = _projectRepository.HasChanges();
            }

            if(e.PropertyName == nameof(SubTaskWrapper.HasErrors))
            {
                SaveProjectCommand.RaiseCanExecuteChanged();
            }
        }

        private bool OnRemoveSubtaskCanExecute()
        {
            return SelectedSubtask != null;
        }

        private void OnAddSubtask()
        {
            throw new NotImplementedException();
        }

        private void OnRemoveSubtask()
        {
            SelectedSubtask.PropertyChanged -= Wrapper_PropertyChanged;
            SubTasks.Remove(SelectedSubtask);
            SelectedSubtask = null;
            HasChanges = _projectRepository.HasChanges();
            SaveProjectCommand.RaiseCanExecuteChanged();
        }





        public bool HasChanges
        {
            get { return _hasChanges; }
            set {
                if(_hasChanges != value)
                    {
                        SetProperty(ref _hasChanges, value);
                        SaveProjectCommand.RaiseCanExecuteChanged();
                    }
                }
        }


        public ProjectWrapper Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }

        private async void OnSaveProject()
        {
           await _projectRepository.SaveAsync();
            HasChanges = _projectRepository.HasChanges();
            _eventAggregator.GetEvent<AfterProjectSavedEvent>().Publish(
                new AfterProjectSavedEventArgs
                {
                    Id = Project.Id, 
                    DisplayMember = Project.ProjectName
                }
                );

        }

        private bool OnSaveCanExecute()
        {
            //return false;
            return Project != null && !Project.HasErrors && HasChanges;
        }

    }
}
