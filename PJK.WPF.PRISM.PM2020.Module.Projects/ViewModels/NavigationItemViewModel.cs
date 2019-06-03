using PJK.WPF.PRISM.PM2020.Module.Projects.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public class NavigationItemViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Id = id;
            DisplayMember = displayMember;

            OpenProjectDetailViewCommand = new DelegateCommand(OnOpenProjectDetailView);
        }



        public int Id { get; }


        private string _displayMember;
        public string DisplayMember
        {
            get { return _displayMember; }
            set { SetProperty(ref _displayMember, value); }
        }


        public DelegateCommand OpenProjectDetailViewCommand { get; }

        private void OnOpenProjectDetailView()
        {
            _eventAggregator.GetEvent<OpenProjectDetailsViewEvent>().Publish(Id);
        }
    }
}
