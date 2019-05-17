using System;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace PJK.WPF.PRISM.PM2020.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Project Manager 2020";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand ExitSystemCommand { get; private set; }





        public MainWindowViewModel()
        {
            ExitSystemCommand = new DelegateCommand(OnExitSystem);
        }

        private void OnExitSystem()
        {
            Application.Current.Shutdown();
        }
    }
}
