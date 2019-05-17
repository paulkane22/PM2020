﻿using PJK.WPF.PRISM.PM2020.Views;
using Prism.Ioc;
using Prism.Modularity;
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

            moduleCatalog.AddModule<ProjectsModule.ProjectsModuleModule>();

        }


    }
}
