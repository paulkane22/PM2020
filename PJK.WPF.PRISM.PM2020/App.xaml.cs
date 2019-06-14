using PJK.WPF.PRISM.PM2020.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Windows;

namespace PJK.WPF.PRISM.PM2020
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            //moduleCatalog.AddModule<Module.Projects.ProjectsModule>();
            moduleCatalog.AddModule<Module.Mana.ManaModule>();
            moduleCatalog.AddModule<Module.ExampleControls.ExampleControlsModule>();


        }

        private void PrismApplication_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            MessageBox.Show("Unexpected error occurred. Please inform Paul Kane " + Environment.NewLine + e.Exception.Message, "This is not the error you are looking for");

            e.Handled = true;

        }
    }
}
