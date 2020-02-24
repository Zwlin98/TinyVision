using System.ComponentModel;
using Prism.Ioc;
using System.Windows;
using Prism.Modularity;
using Prism.Unity;
using TinyVision.Views;
using WorkSpace.Views;

namespace TinyVision
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ImageTab>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<WorkSpace.WorkSpaceModule>();
            moduleCatalog.AddModule<Sidebar.SidebarModule>();
        }
    }
}