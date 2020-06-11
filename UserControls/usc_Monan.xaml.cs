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

namespace QuanLyTiecCuoi.UserControls
{
    /// <summary>
    /// Interaction logic for usc_Monan.xaml
    /// </summary>
    public partial class usc_Monan : UserControl
    {
        public usc_Monan()
        {
            InitializeComponent();
        }

        private void btn_ThemMonAn_Click(object sender, RoutedEventArgs e)
        {
            btn_SuaMonAn.IsEnabled = false;
            btn_XoaMonAn.IsEnabled = false;
            tbx_TKMonAn.IsEnabled = false;
            cbb_LoaiMonAn.IsEnabled = false;
            SetActiveUserControl(pptFood);
        }
        public void SetActiveUserControl(UserControl control)
        {
            //Collapsed usercontrol
            pptFood.Visibility = Visibility.Collapsed;
            grb_ListMenu.Visibility = Visibility.Collapsed;

            //Show usercontrol
            control.Visibility = Visibility.Visible;
        }
    }
}
