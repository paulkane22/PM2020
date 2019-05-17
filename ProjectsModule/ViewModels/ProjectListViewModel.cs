using Prism.Events;
using Prism.Mvvm;
using ProjectsModule.Services;
using System;
using System.ComponentModel;
using System.Windows.Data;

namespace ProjectsModule.ViewModels
{
    public class ProjectListViewModel : BindableBase
    {

        private readonly IEventAggregator eventAggregator;
        
        public ProjectListViewModel(IProjectDataService dataService, IEventAggregator eventAggregator)
        {
            if (dataService == null) throw new ArgumentNullException("dataService");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            this.eventAggregator = eventAggregator;

            this.Projects = new ListCollectionView(dataService.GetProjects());

        }

        public ICollectionView Projects { get; private set; }

    }
}
