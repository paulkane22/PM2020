using PJK.WPF.PRISM.PM2020.DataAccess;
using PJK.WPF.PRISM.PM2020.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Lookups;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services.Repositories;
using PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels;
using PJK.WPF.PRISM.PM2020.Module.Projects.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace PJK.WPF.PRISM.PM2020.Module.Projects
{
    public class ProjectsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(ProjectManager));
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(Main));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<INavigationViewModel, ProjectNavigatorViewModel>();
            //containerRegistry.Register<INavigationViewModel, NavigationViewModel>();
            //containerRegistry.Register<IProjectDetailViewModel, ProjectDetailViewModel>();
            containerRegistry.Register<IMessageDialogService, MessageDialogService>();

            containerRegistry.Register<IProjectLookupDataService, LookupDataService>();
            containerRegistry.Register<ISystemItemLookupDataService, LookupDataService>();
            containerRegistry.RegisterInstance<IProject>(new Project());
            containerRegistry.RegisterInstance<IProjectRepository>(new ProjectRepository(new PM202DbContext()));

        }
    }
}