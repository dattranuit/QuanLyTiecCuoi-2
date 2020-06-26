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
using MahApps.Metro.Controls;
using System.Windows.Shapes;

namespace QuanLyTiecCuoi
{
    /// <summary>
    /// Interaction logic for PhieuDatBanWindow.xaml
    /// </summary>
    public partial class DatBanvaDichVuWindow : MetroWindow
    {
        public DatBanvaDichVuWindow()
        {
            InitializeComponent();
        }

        private void btn_PDDV_Them_Click(object sender, RoutedEventArgs e)
        {
            grr_1.Height = new GridLength(0.9, GridUnitType.Star);
            dpn_DanhSachDichVu.Visibility = Visibility.Visible;
        }

        private void btn_PDDV_Sua_Click(object sender, RoutedEventArgs e)
        {
            dpn_DanhSachDichVu.Visibility = Visibility.Hidden;
            grr_1.Height = new GridLength(0.4, GridUnitType.Star);
        }
    }
}
