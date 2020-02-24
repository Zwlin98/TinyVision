using System;
using System.Linq;
using System.Windows;
using AppEvents;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using TinyVision.Views;
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
            SaveAs = new DelegateCommand(SaveAsExecute,CanSaveAs);
            Close = new DelegateCommand<MainWindow>(CloseExecute);

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
            var first = _regionManager.Regions["ImageTabs"].ActiveViews.First() as ImageTab;
            var viewModels = first?.DataContext as ImageTabViewModel;
            viewModels.SaveImage();
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
            SaveAs.RaiseCanExecuteChanged();
        }

        //另存为
        public DelegateCommand SaveAs { get; private set; }

        private void SaveAsExecute()
        {
            var first = _regionManager.Regions["ImageTabs"].ActiveViews.First() as ImageTab;
            var viewModels = first?.DataContext as ImageTabViewModel;
            viewModels.SaveImageAs();
        }

        private bool CanSaveAs()
        {
            try
            {
                var views = _regionManager.Regions["ImageTabs"].ActiveViews;
                if (views.Any())
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return false;
        }

        // 关闭
        public DelegateCommand<MainWindow> Close { get; set; }

        private void CloseExecute(MainWindow mainWindow)
        {
            try
            {
                var views = _regionManager.Regions["ImageTabs"].ActiveViews;
                if (views.Any())
                {
                    foreach (ImageTab view  in views)
                    {
                        var viewModel = view.DataContext as ImageTabViewModel;
                        if (viewModel.CanSave)
                        {
                            if (MessageBox.Show("有未保存的文件，是否要关闭？", "确认", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                mainWindow.Close();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    mainWindow.Close();
                }
            }
            catch (Exception e)
            {
                mainWindow?.Close();
            }
            
        }
    }


}