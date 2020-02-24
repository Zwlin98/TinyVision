using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Regions;
using WorkSpace.ViewModels;
using WorkSpace.Views;

namespace TinyVision.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private IRegionManager _regionManager;

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            _regionManager = regionManager;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                var views = _regionManager.Regions["ImageTabs"].ActiveViews;
                if (views.Any())
                {
                    foreach (ImageTab view in views)
                    {
                        var viewModel = view.DataContext as ImageTabViewModel;
                        if (viewModel.CanSave)
                        {
                            if (MessageBox.Show("有未保存的文件，是否要关闭？", "确认", MessageBoxButton.YesNo) == MessageBoxResult.No)
                            {
                                e.Cancel = true;
                            }
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }
    }
}
