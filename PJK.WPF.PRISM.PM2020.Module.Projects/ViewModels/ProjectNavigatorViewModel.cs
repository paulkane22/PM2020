﻿using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
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
        private ProjectWrapper _selectedProject;

        private DelegateCommand GridDoubleClickCommand;

        public ProjectNavigatorViewModel(IProjectRepository projectRepository, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _projectRepository = projectRepository;

            Projects = new ObservableCollection<ProjectWrapper>();

            GridDoubleClickCommand = new DelegateCommand(OnGridDoubleClick);

            _eventAggregator.GetEvent<EditDetailEvent>().Subscribe(OnEditDetail);
            _eventAggregator.GetEvent<DeleteDetailEvent>().Subscribe(OnDeleteDetail);
            _eventAggregator.GetEvent<RefreshListEvent>().Subscribe(OnRefreshList);
        }

        private async void OnDeleteDetail()
        {
            if(SelectedProject != null)
            {
                _projectRepository.Remove(SelectedProject.Model);
                await _projectRepository.SaveAsync();
                OnRefreshList();
            }

        }

        private void OnGridDoubleClick()
        {
            throw new NotImplementedException();
        }

        private async void OnRefreshList()
        {
            await LoadAsync();
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

    }
}
