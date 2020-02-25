using System.Linq;
using System.Windows;
using AppEvents;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using WorkSpace.Views;

namespace WorkSpace.ViewModels
{
    public class WorkSpaceViewModel:BindableBase
    {
        private string _button;
        private IEventAggregator _eventAggregator;
        private IRegionManager _regionManager;


        public string Button
        {
            get { return _button; }
            set { SetProperty(ref _button, value); }
        }

        public WorkSpaceViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            Button = "TEST";
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            TabChangedCommand = new DelegateCommand(TabChanged);
        }


        public DelegateCommand TabChangedCommand { get; private set; }
        
        private void TabChanged()
        {
            _eventAggregator.GetEvent<CanSaveImage>().Publish();
            _eventAggregator.GetEvent<CanEditImage>().Publish();
            _eventAggregator.GetEvent<TabChanged>().Publish();
        }
    }
}