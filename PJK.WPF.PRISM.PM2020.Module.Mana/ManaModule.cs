using PJK.WPF.PRISM.PM2020.Module.Mana.Services;
using PJK.WPF.PRISM.PM2020.Module.Mana.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace PJK.WPF.PRISM.PM2020.Module.Mana
{
    public class ManaModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(Main));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }
    }
}