using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace WorkSpace
{
    public class WorkSpaceModule:IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
          
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            
            regionManager.RegisterViewWithRegion("WorkSpace", typeof(Views.WorkSpace));
        }
    }
}