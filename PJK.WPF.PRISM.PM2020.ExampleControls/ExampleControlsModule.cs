using PJK.WPF.PRISM.PM2020.ExampleControls.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace PJK.WPF.PRISM.PM2020.ExampleControls
{
    public class ExampleControlsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ControlsDemo));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}