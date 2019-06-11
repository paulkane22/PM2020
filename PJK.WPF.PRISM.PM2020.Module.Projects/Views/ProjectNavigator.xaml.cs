using PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels;
using System.Windows.Controls;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Views
{
    /// <summary>
    /// Interaction logic for ProjectNavigator
    /// </summary>
    public partial class ProjectNavigator : UserControl
    {
        private ProjectNavigatorViewModel _viewModel;

        public ProjectNavigator()
        {
            InitializeComponent();
            
            _viewModel = (ProjectNavigatorViewModel)this.DataContext;
            this.Loaded += ProjectNavigator_Loaded;
        }

        private async void ProjectNavigator_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(_viewModel != null)
            {
                await _viewModel.LoadAsync();
            }

        }
    }
}
