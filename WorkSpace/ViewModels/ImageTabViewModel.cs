using System.Collections.Generic;
using System.Windows.Media.Imaging;
using AppEvents;
using Microsoft.Win32;
using OpenCvSharp;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Utils;
using WorkSpace.Business;
using WorkSpace.Utils;
using WorkSpace.Views;
using ImageProcessor = Dlls;

namespace WorkSpace.ViewModels
{
    public class ImageTabViewModel : BindableBase, INavigationAware
    {
        //事件聚合器
        private IEventAggregator _eventAggregator;

        private List<IOperation> _operations;

        public List<IOperation> Operations
        {
            get { return _operations; }
            set { SetProperty(ref _operations, value); }
        }

        // 图片路径
        private string _imageFilePath;

        public string ImageFilePath
        {
            get { return _imageFilePath; }
            set { SetProperty(ref _imageFilePath, value); }
        }

        // 图片名
        private string _filename;

        public string FileName
        {
            get { return _filename; }
            set { SetProperty(ref _filename, value); }
        }

        //图片矩阵
        public Mat ImageMat { get; set; }


        //图片BitMap
        private BitmapImage _bitmapImageSource;

        public BitmapImage BitmapImageSource
        {
            get { return _bitmapImageSource; }
            set { SetProperty(ref _bitmapImageSource, value); }
        }

        // 构造函数
        public ImageTabViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Operations = new List<IOperation>();
        }

        //导航接口
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var path = navigationContext.Parameters["ImageFilePath"] as string;
            if (path != null)
            {
                ImageFilePath = path;
                FileName = Files.GetLastPartNameOfPath(path);
                OpenPicture();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return string.IsNullOrEmpty(ImageFilePath);
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }


        //打开图片
        private void OpenPicture()
        {
            ImageMat =   ImageProcessor.Open.OpenImage(ImageFilePath);
            BitmapImageSource = Converter.MatToBitmapImage(ImageMat);
        }

        // 保存图片
        public void SaveImage()
        {
            ImageProcessor.Open.SaveImage(ImageFilePath, ImageMat);
        }

        private bool _canSave = false;

        public bool CanSave
        {
            get { return _canSave; }
            set
            {
                SetProperty(ref _canSave, value);
                _eventAggregator.GetEvent<CanSaveImage>().Publish();
            }
        }

        // 另存为
        public void SaveImageAs()
        {
            var saveFileImageDialog = new SaveFileDialog();
            if (saveFileImageDialog.ShowDialog() == true)
            {
                // TODO:完成另存为
            }
        }


        // 图片处理

        // 旋转
        public void Rotate(string angel)
        {
            if (angel == "90")
            {
                ImageMat = ImageProcessor.Rotate.Rot90(ImageMat);
            }

            if (angel == "180")
            {
                ImageMat = ImageProcessor.Rotate.Rot180(ImageMat);
            }

            if (angel == "270")
            {
                ImageMat = ImageProcessor.Rotate.Rot270(ImageMat);
            }

            if (angel == "Any")
            {
                var rotateDialog = new RotateWindow();
                if (rotateDialog.ShowDialog() == true)
                {
                    var clockWise = rotateDialog.ClockWise;
                    var angelValue = rotateDialog.AngelValue;
                    ImageMat = ImageProcessor.Rotate.ImgRotate(ImageMat, angelValue, !clockWise);
                }
            }

            if (angel == "Vertical")
            {
                ImageMat = ImageProcessor.Rotate.VertRot(ImageMat);
            }

            if (angel == "Horizontal")
            {
                ImageMat = ImageProcessor.Rotate.HoriRot(ImageMat);
            }

            CanSave = true;
            BitmapImageSource = Converter.MatToBitmapImage(ImageMat);
        }
    }

}