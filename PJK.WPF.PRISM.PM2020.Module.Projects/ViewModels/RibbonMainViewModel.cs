using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class RibbonMainViewModel : BindableBase, IRibbonViewModel
    {
        private IEventAggregator _eventAggregator;




        public DelegateCommand AddProjectCommand { get; private set; }
        public DelegateCommand EditProjectCommand { get; private set; }
        public DelegateCommand RefreshListCommand { get; private set; }


        public RibbonMainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
            AddProjectCommand = new DelegateCommand(OnAddProjectExecute);
            EditProjectCommand = new DelegateCommand(OnEditProjectExecute);
            RefreshListCommand = new DelegateCommand(OnRefreshExecute);
        }

        private void OnRefreshExecute()
        {
            _eventAggregator.GetEvent<RefreshListEvent>().Publish();
        }

        private void OnAddProjectExecute()
        {
            _eventAggregator.GetEvent<AddDetailEvent>().Publish();
        }

        private void OnEditProjectExecute()
        {
            _eventAggregator.GetEvent<EditDetailEvent>().Publish(new EditDetailEventArgs { Id = 0 });
        }
    }
}
