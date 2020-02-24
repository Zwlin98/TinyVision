using System.Windows;
using AppEvents;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace WorkSpace.ViewModels
{
    public class WorkSpaceViewModel:BindableBase
    {
        private string _button;
        private IEventAggregator _eventAggregator;

        public string Button
        {
            get { return _button; }
            set { SetProperty(ref _button, value); }
        }

        public WorkSpaceViewModel(IEventAggregator eventAggregator)
        {
            Button = "TEST";
            _eventAggregator = eventAggregator;
            TabChangedCommand = new DelegateCommand(TabChanged);
        }

        public DelegateCommand TabChangedCommand { get; private set; }
        
        private void TabChanged()
        {
            _eventAggregator.GetEvent<CanSaveImage>().Publish();
        }
    }
}