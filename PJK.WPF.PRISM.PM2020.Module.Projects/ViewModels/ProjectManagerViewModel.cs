using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows.Data;



namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class ProjectManagerViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public ProjectManagerViewModel()
        {
                
        }

        public ProjectManagerViewModel(IProjectDataService dataService, IEventAggregator eventAggregator)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            this.eventAggregator = eventAggregator;
            this.Projects = new ListCollectionView(dataService.GetProjects());
        }

        public ICollectionView Projects { get; private set; }

    }
}

