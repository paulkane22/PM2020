using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectNavigatorViewModel : BindableBase, INavigationViewModel
    {
        private IEventAggregator _eventAggregator;
        private IProjectRepository _projectRepository;
        private IMessageDialogService _messageDialogService;
        private ProjectWrapper _selectedProject;

        public DelegateCommand LoadDataCommand { get; private set; }

        public ProjectNavigatorViewModel(IProjectRepository projectRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _projectRepository = projectRepository;
            _messageDialogService = messageDialogService;

            Projects = new ObservableCollection<ProjectWrapper>();

            _eventAggregator.GetEvent<EditDetailEvent>().Subscribe(OnEditDetail);
            _eventAggregator.GetEvent<DeleteDetailEvent>().Subscribe(OnDeleteDetailExecute);
            _eventAggregator.GetEvent<RefreshListEvent>().Subscribe(OnRefreshList);

            LoadDataCommand = new DelegateCommand(OnLoadDataExecute);

        }

        private async void OnLoadDataExecute()
        {
            await LoadAsync();
        }

        private async void OnDeleteDetailExecute()
        {
            var value = _messageDialogService.ShowOKCancelDialog($"Do you really wish to delete {SelectedProject.ProjectName}?", "Delete Project?");
            if(SelectedProject != null && value == MessageDialogResult.OK)
            {
                _projectRepository.Remove(SelectedProject.Model);
                await _projectRepository.SaveAsync();
                OnRefreshList();
            }
        }


        private void OnEditDetail(EditDetailEventArgs args)
        {
            //if(_selectedProject != null)
            //_eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(new OpenDetailViewEventArgs { Id = _selectedProject.Id });
        }

        public async Task LoadAsync()
        {
            var lookup = await _projectRepository.GetAllAsync();
            Projects.Clear();
            foreach (var item in lookup)
            {
                Projects.Add(new ProjectWrapper(item));
            }
        }

        public ObservableCollection<ProjectWrapper> Projects { get; set; }

        public ProjectWrapper SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); }
        }




        private async void OnRefreshList()
        {
            await LoadAsync();
        }
    }
}
