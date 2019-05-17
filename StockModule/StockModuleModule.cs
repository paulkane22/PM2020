using StockModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace StockModule
{
    public class StockModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(StockList));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}