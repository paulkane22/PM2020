using PJK.WPF.PRISM.PM2020.Module.Mana.ViewModels;
using System.Windows.Controls;

namespace PJK.WPF.PRISM.PM2020.Module.Mana.Views
{
    /// <summary>
    /// Interaction logic for Main
    /// </summary>
    public partial class Main : UserControl
    {
        private MainViewModel  _viewModel;

        public Main()
        {
            InitializeComponent();
            _viewModel = (MainViewModel)this.DataContext;
            this.Loaded += Main_Loaded;
        }

        private async void Main_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(_viewModel != null)
            {
            await _viewModel.LoadAsync();
            }
        }
    }
}
