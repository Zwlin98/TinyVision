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
using System.Windows.Shapes;

namespace WorkSpace.Views
{
    /// <summary>
    /// RotateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RotateWindow : Window
    {
        public bool ClockWise { get; set; }
        public double AngelValue { get; set; }
        public RotateWindow()
        {
            InitializeComponent();
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsClockWise.IsChecked != null) ClockWise = (bool) IsClockWise.IsChecked;
            try
            {
                AngelValue = Convert.ToDouble(Angel.Text);
            }
            catch
            {
                MessageBox.Show(" 输入角度格式错误,请输入 0-360 区间的角度");
                return;
            }

            if (AngelValue >= 0 && AngelValue <= 360)
            {
                DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(" 输入角度格式错误,请输入 0-360 区间的角度");
                return;
            }
        }

    }
}
