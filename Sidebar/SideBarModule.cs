using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Sidebar
{
    public class SidebarModule:IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion("WorkHistory", typeof(Views.WorkHistory));
        }
    }
}
