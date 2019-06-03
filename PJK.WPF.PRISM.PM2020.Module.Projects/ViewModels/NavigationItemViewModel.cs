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
      
        public NavigationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }

        public int Id { get; }


        private string _displayMember;
        public string DisplayMember
        {
            get { return _displayMember; }
            set { SetProperty(ref _displayMember, value); }
        }



    }
}
