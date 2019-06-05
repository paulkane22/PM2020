
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.Wrapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class MainViewModel : BindableBase 
    {
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IProjectRepository _projectRepository;
        private IProjectDetailViewModel _projectDetailViewModel;
        private INavigationViewModel _navigationViewModel;
        private ProjectWrapper _selectedProject;
        private bool _hasChanges;


        public MainViewModel(INavigationViewModel navigationViewModel, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
           _navigationViewModel = navigationViewModel;
           _messageDialogService = messageDialogService;
        }


        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set { SetProperty(ref _navigationViewModel, value);}
        }

        public ProjectWrapper SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); }
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
