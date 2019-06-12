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
            _ribbonViewModel = ribbonViewModel;
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;

            ProjectDetailViewModel = projectDetailViewModel;

            _eventAggregator.GetEvent<AddDetailEvent>().Subscribe(OnAddDetailExecute);
            _eventAggregator.GetEvent<CancelDetailEvent>().Subscribe(OnCancelAddDetail);
            _eventAggregator.GetEvent<EditDetailEvent>().Subscribe(OnEditDetailExecute);

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(OnAfterDetailSaved);


        }

        private async void OnEditDetailExecute(EditDetailEventArgs args)
        {
            int myId = _navigationViewModel.SelectedDetailId;
            if(myId > 0)
            {
                await _projectDetailViewModel.LoadAsync(myId);
                _projectDetailViewModel.InEditMode = true;
                ShowAddProject = true;
            }
        }

        private void OnAfterDetailSaved()
        {
            ShowAddProject = false;
        }

        private void OnCancelAddDetail()
        {
            ShowAddProject = false;
        }

        private async void OnAddDetailExecute()
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
