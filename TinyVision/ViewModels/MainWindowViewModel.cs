using System;
using System.Linq;
using System.Windows;
using AppEvents;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Utils;
using WorkSpace.ViewModels;
using WorkSpace.Views;


namespace TinyVision.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager,IEventAggregator eventAggregator)
        {
            Title = "TinyVision";
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            OpenPicture = new DelegateCommand(OpenPictureExecute);
            OpenVideo = new DelegateCommand(OpenVideoExecute);
            Save = new DelegateCommand(SaveExecute,CanSave);

            _eventAggregator.GetEvent<CanSaveImage>().Subscribe(RaiseCanSaveChanged);
        }

        // 菜单栏的命令

        // 打开图像
        public DelegateCommand OpenPicture { get; private set; }

        private void OpenPictureExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = Files.GetOpenFile();

            if (openFileDialog.ShowDialog() == true)
            {
                //TODO:校验文件格式，并显示
                var parameters = new NavigationParameters();
                
                parameters.Add("ImageFilePath",openFileDialog.FileName);
                
                _regionManager.RequestNavigate("ImageTabs","ImageTab",parameters);

            }
        }
        //打开视频
        public DelegateCommand OpenVideo { get; private set; }

        private void OpenVideoExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = Files.GetOpenFile();

            if (openFileDialog.ShowDialog() == true)
            {
                //TODO:校验文件格式，并显示
                // MessageBox.Show(openFileDialog.FileName);

                var parameters = new NavigationParameters();

                parameters.Add("ImageFilePath", openFileDialog.FileName);

                _regionManager.RequestNavigate("ImageTabs", "ImageTab", parameters);
            }
        }

        // 保存

        public DelegateCommand Save { get; private set; }


        private void SaveExecute()
        {
            // _eventAggregator.GetEvent<SaveImage>().Publish();
            var first = _regionManager.Regions["ImageTabs"].ActiveViews.First() as ImageTab;
            MessageBox.Show((first.DataContext as ImageTabViewModel).FileName);
            
        }

        private bool CanSave()
        {
            try
            {
                var views = _regionManager.Regions["ImageTabs"].ActiveViews;
                if (!views.Any())
                {
                    return false;
                }
                return ((ImageTabViewModel)((ImageTab)views.First())?.DataContext).CanSave;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        private void RaiseCanSaveChanged()
        {
            Save.RaiseCanExecuteChanged();
        }
    }

}