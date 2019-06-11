using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IDetailViewModel _projectDetailViewModel;
        private INavigationViewModel _navigationViewModel;
        private IRibbonViewModel _ribbonViewModel;

        private bool _hasChanges;
        private bool _showAddProject = false;


        public MainViewModel(INavigationViewModel navigationViewModel, IProjectDetailViewModel projectDetailViewModel, IRibbonViewModel ribbonViewModel , IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _navigationViewModel = navigationViewModel;
            ProjectDetailViewModel = projectDetailViewModel;
            _ribbonViewModel = ribbonViewModel;
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AddDetailEvent>().Subscribe(OnAddDetail);
            _eventAggregator.GetEvent<CancelDetailEvent>().Subscribe(OnCancelAddProject);
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(OnAfterDetailSaved);


        }

        private void OnAfterDetailSaved()
        {
            ShowAddProject = false;
        }

        private void OnCancelAddProject()
        {
            ShowAddProject = false;
        }

        private async void OnAddDetail()
        {
            await _projectDetailViewModel.LoadAsync(0);
            ShowAddProject = true;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public bool ShowAddProject
        {
            get { return _showAddProject; }
            set { SetProperty(ref _showAddProject, value); }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set { SetProperty(ref _hasChanges, value); }
        }

        public IDetailViewModel ProjectDetailViewModel
        {
            get { return _projectDetailViewModel; }
            set { SetProperty(ref _projectDetailViewModel, value); }
        }

        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set { SetProperty(ref _navigationViewModel, value); }
        }
    }
}
