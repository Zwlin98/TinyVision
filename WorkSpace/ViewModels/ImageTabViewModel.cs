using System.Windows;
using AppEvents;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Utils;
namespace WorkSpace.ViewModels
{
    public class ImageTabViewModel:BindableBase,INavigationAware
    {
        private string _imageFilePath;
        
        public string ImageFilePath
        {
            get { return _imageFilePath; }
            set { SetProperty(ref _imageFilePath, value); }
        }

        private string _filename;

        public string FileName
        {
            get { return _filename; }
            set { SetProperty(ref _filename, value); }
        }

        public ImageTabViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        // 保存图片
        private void SaveImage()
        {
            MessageBox.Show("Saved");
        }

        private bool _canSave=false;
        private IEventAggregator _eventAggregator;

        public bool CanSave
        {
            get { return _canSave; }
            set
            {
                SetProperty(ref _canSave, value);
                _eventAggregator.GetEvent<CanSaveImage>().Publish();
            }
        }

        //导航接口
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var path = navigationContext.Parameters["ImageFilePath"] as string;
            if (path != null)
            {
                ImageFilePath = path;
                FileName = Files.GetLastPartNameOfPath(path);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return string.IsNullOrEmpty(ImageFilePath);
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}