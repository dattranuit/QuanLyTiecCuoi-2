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
    /// Interaction logic for PropertiesFood.xaml
    /// </summary>
    public partial class PropertiesFood : UserControl
    {
        public PropertiesFood()
        {
            InitializeComponent();
        }

        private void btn_LuuThayDoi_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem item = new ListViewItem();

            if (tbx_ThemMaMonAn.Text == ""
                || tbx_ThemTenMonAn.Text == ""
                || tbx_ThemDonGia.Text == ""
                || tbx_ThemMoTa.Text == ""
                || tbx_ThemGhiChu.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập dữ liệu", ("Thông báo!"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
                MessageBox.Show("Dữ liệu đã được lưu", ("Thông báo!"), MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void btn_XoaThayDoi_Click(object sender, RoutedEventArgs e)
        {
            tbx_ThemTenMonAn.Clear();
            tbx_ThemMaMonAn.Clear();
            tbx_ThemDonGia.Clear();
            tbx_ThemGhiChu.Clear();
            tbx_ThemMoTa.Clear();
        }
        private void btn_ThoatThayDoi_Click(object sender, RoutedEventArgs e)
        {
            PropertiesFood _e = new PropertiesFood();

        }


    }
}
