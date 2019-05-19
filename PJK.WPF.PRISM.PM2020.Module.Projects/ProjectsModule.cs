using PJK.WPF.PRISM.PM2020.Module.Projects.Model;
using PJK.WPF.PRISM.PM2020.Module.Projects.Services;
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
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ProjectDetail));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IProject>(new Project());
            containerRegistry.RegisterInstance<IProjectDataService>(new ProjectDataService());
        }
    }
}