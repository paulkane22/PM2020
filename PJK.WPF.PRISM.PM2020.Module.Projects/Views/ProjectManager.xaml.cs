using PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.Views
{
    /// <summary>
    /// Interaction logic for ProjectManager
    /// </summary>
    public partial class ProjectManager : UserControl
    {
        private ProjectManagerViewModel _viewModel;



        public ProjectManager()
        {
            InitializeComponent();
            _viewModel = (ProjectManagerViewModel)this.DataContext;
            this.Loaded += ProjectManager_Loaded;
        }

        private async void ProjectManager_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
           await  _viewModel.LoadProjectsAsync();
        }
    }
}
