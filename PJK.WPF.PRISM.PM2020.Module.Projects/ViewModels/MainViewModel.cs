using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using Prism.Events;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IDetailViewModel _projectDetailViewModel;
        private INavigationViewModel _navigationViewModel;

        private bool _hasChanges;


        public MainViewModel(INavigationViewModel navigationViewModel, IProjectDetailViewModel projectDetailViewModel, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _navigationViewModel = navigationViewModel;
            _projectDetailViewModel = projectDetailViewModel;
            _messageDialogService = messageDialogService;

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


        public bool HasChanges
        {
            get { return _hasChanges; }
            set { SetProperty(ref _hasChanges, value); }
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

    }
}
