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
using QuanLyTiecCuoi.Model;

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
            SetAllNull();
        }

        private void SetAllNull()
        {
            //dtg_DanhSachDatBan.SelectedItem = dtg_DanhSachDatDichVu.SelectedItem = dtg_DanhSachDatMonAn.SelectedItem = null;
            nbr_PDB_DonGiaBan.Value = nbr_PDB_SoLuong.Value = nbr_PDB_SoLuongDuTru.Value = nbr_PDDV_SoLuong.Value = nbr_DV_SoLuong.Value = 0;
            tbx_PDB_LoaiBan.Text = tbx_PDB_GhiChu.Text = "";
            tbx_DV_ThanhTien.Text = tbx_PDDV_ThanhTien.Text = "0";
        }

        private void btn_PDDV_Them_Click(object sender, RoutedEventArgs e)
        {
            grr_1.Height = new GridLength(0.9, GridUnitType.Star);
            dpn_DanhSachDichVu.Visibility = Visibility.Visible;
            grb_ChiTietChinhSua.Visibility = Visibility.Hidden;
        }

        private void btn_PDDV_Sua_Click(object sender, RoutedEventArgs e)
        {
            dpn_DanhSachDichVu.Visibility = Visibility.Hidden;
            grb_ChiTietChinhSua.Visibility = Visibility.Visible;
            grr_1.Height = new GridLength(0.25, GridUnitType.Star);
        }

    }
}
