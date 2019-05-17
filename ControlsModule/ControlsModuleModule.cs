using ControlsModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;


namespace ControlsModule
{
    public class ControlsModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ControlExample));
        }

                     
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}


