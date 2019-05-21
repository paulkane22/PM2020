using System;
using System.ComponentModel;
using System.Windows;
using PJK.WPF.PRISM.PM2020.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

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

        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand ShowAboutCommand { get; private set; }
        public DelegateCommand ExitSystemCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            NavigateCommand = new DelegateCommand<string>(OnNavigate);
            ShowAboutCommand = new DelegateCommand(OnShowAbout);
            ExitSystemCommand = new DelegateCommand(OnExitSystem);

            _regionManager = regionManager;

            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
        }

        private void OnShowAbout()
        {
            //This is a test to see what the about screen will look like. 
            AboutWindow k = new AboutWindow();
            k.ShowDialog();
        }

        private void OnNavigate(string destination)
        {
            switch (destination)
            {
                case "controls":
                    Navigate("ControlsDemo");
                    break;
                case "projects":
                    Navigate("ProjectManager");
                    break;
                case "stock":
                    Navigate("StockList");
                    break;
                default:
                    break;
            }
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }


        private void OnExitSystem()
        {
            Application.Current.Shutdown();
        }
    }
}
