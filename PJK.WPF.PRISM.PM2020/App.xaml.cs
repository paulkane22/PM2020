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

            moduleCatalog.AddModule<Module.Projects.ProjectsModule>();
            moduleCatalog.AddModule<Module.ExampleControls.ExampleControlsModule>();


            //moduleCatalog.AddModule<StockModule.StockModuleModule>();
            //moduleCatalog.AddModule<ProjectsModule.ProjectsModuleModule>();
            //moduleCatalog.AddModule<ExampleControls.ExampleControlsModule>();
        }

        private void PrismApplication_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            MessageBox.Show("Unexpected error occurred. Please inform Paul" + Environment.NewLine + e.Exception.Message, "This is not the error you are looking for");

            e.Handled = true;

        }
    }
}
