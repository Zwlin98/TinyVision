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
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;

        private string _title;
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
            // 菜单命令初始化
            // 文件部分
            OpenPicture = new DelegateCommand(OpenPictureExecute);
            OpenVideo = new DelegateCommand(OpenVideoExecute);
            Save = new DelegateCommand(SaveExecute,CanSave);
            SaveAs = new DelegateCommand(SaveAsExecute).ObservesCanExecute(()=>CanEdit);
            CloseByMenu = new DelegateCommand<MainWindow>(CloseByMenuExecute);
            // 图像部分
            ToGray = new DelegateCommand(ToGrayExecute).ObservesCanExecute(()=>CanEdit);
            Rotate = new DelegateCommand<string>(RotateExecute).ObservesCanExecute(() => CanEdit);
            ChangeBriAndCon = new DelegateCommand(ChangeBriAndConExecute).ObservesCanExecute(()=>CanEdit);
            Crop = new DelegateCommand(CropExecute).ObservesCanExecute(()=>CanEdit);
            // 事件监听
            _eventAggregator.GetEvent<CanSaveImage>().Subscribe(RaiseCanSaveChanged);
            _eventAggregator.GetEvent<CanEditImage>().Subscribe(RaiseCanEditChanged);
        }

        // 获得当前工作区图片的ViewModel
        private ImageTabViewModel GetCurrentTabViewModel()
        {
            var first = _regionManager.Regions["ImageTabs"].ActiveViews.First() as ImageTab;
            return first?.DataContext as ImageTabViewModel;
        }

        private bool _canEdit=false;

        public bool CanEdit
        {
            get { return _canEdit; }
            set { SetProperty(ref _canEdit,value); }
        }

        private void RaiseCanEditChanged()
        {
            if (_regionManager.Regions["ImageTabs"].ActiveViews.Any())
            {
                CanEdit = true;
            }
            else
            {
                CanEdit = false;
            }
        }
        // 菜单栏的命令

        //确认可以修改

        // 文件部分
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

                CanEdit = true;
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
            GetCurrentTabViewModel()?.SaveImage();
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

        //另存为
        public DelegateCommand SaveAs { get; private set; }

        private void SaveAsExecute()
        {
            GetCurrentTabViewModel().SaveImageAs();
        }

        // 退出
        public DelegateCommand<MainWindow> CloseByMenu { get; private set; }

        private void CloseByMenuExecute(MainWindow mainWindow)
        {
            mainWindow.Close();
        }


        //图像部分

        // 模式

        // 颜色位数

        // 灰度化
        public DelegateCommand ToGray { get; private set; }

        public void ToGrayExecute()
        {
            GetCurrentTabViewModel().ToGray();
        }
        //对比度保留去色

        // 调整
        // 亮度对比度
        public  DelegateCommand ChangeBriAndCon {get; private set; }

        private void ChangeBriAndConExecute()
        {
            GetCurrentTabViewModel().ChangeBriAndCon();
        }

        // 旋转
        public DelegateCommand<string> Rotate { get; private set; }

        private void RotateExecute(string angel)
        {
            GetCurrentTabViewModel().Rotate(angel);
        }

        // 裁剪
        public   DelegateCommand Crop { get; private set; }

        private void CropExecute()
        {
            GetCurrentTabViewModel().Crop();
        }
    }


}