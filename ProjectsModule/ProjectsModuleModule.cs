using ProjectsModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ProjectsModule.Model;
using ProjectsModule.Services;

namespace ProjectsModule
{
    public class ProjectsModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ProjectList));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IProject>(new Project());
            containerRegistry.RegisterInstance<IProjectDataService>(new ProjectDataService());
        }
    }
}