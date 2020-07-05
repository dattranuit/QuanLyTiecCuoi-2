using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using QuanLyTiecCuoi.Model;

namespace QuanLyTiecCuoi.ViewModel
{
    class PhieuDatBanViewModel : BaseViewModel
    {
        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }
        private bool _IsReadOnly;
        public bool IsReadOnly { get => _IsReadOnly; set { _IsReadOnly = value; IsEnable = !_IsReadOnly; OnPropertyChanged(); } }
        private static int _CurrentMaTiecCuoi;
        public static int CurrentMaTiecCuoi { get => _CurrentMaTiecCuoi; set { _CurrentMaTiecCuoi = value; } }
        private static ObservableCollection<PHIEUDATBAN> _ListPhieuDatBan;
        public static ObservableCollection<PHIEUDATBAN> ListPhieuDatBan { get => _ListPhieuDatBan; set => _ListPhieuDatBan = value; }
        private static decimal _DonGiaBanToiThieu;
        public static decimal DonGiaBanToiThieu { get => _DonGiaBanToiThieu; set { _DonGiaBanToiThieu = value; } }
        private PHIEUDATBAN _SelectedPDB;
        public PHIEUDATBAN SelectedPDB
        {
            get => _SelectedPDB;
            set
            {
                _SelectedPDB = value;
                OnPropertyChanged();
                if (SelectedPDB != null)
                {
                    MaPhieuDatBan = CT_PhieuDatBanViewModel.CurrentMaPDB = SelectedPDB.MaPhieuDatBan;
                    SoLuong = SelectedPDB.SoLuong;
                    LoaiBan = SelectedPDB.LoaiBan;
                    SoLuongDuTru = SelectedPDB.SoLuongDuTru;
                    DonGiaBan = SelectedPDB.DonGiaBan;
                    GhiChu = SelectedPDB.GhiChu;
                }
            }
        }
        private int _MaPhieuDatBan;
        private int _SoLuong;
        private int _SoLuongDuTru;
        private decimal _DonGiaBan;
        private string _GhiChu;
        private string _LoaiBan;
        public int MaPhieuDatBan { get => _MaPhieuDatBan; set { _MaPhieuDatBan = value; OnPropertyChanged(); } }
        public string LoaiBan { get => _LoaiBan; set { _LoaiBan = value; OnPropertyChanged(); } }
        public int SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }
        public int SoLuongDuTru { get => _SoLuongDuTru; set { _SoLuongDuTru = value; OnPropertyChanged(); } }
        public decimal DonGiaBan { get => _DonGiaBan; set { _DonGiaBan = value; OnPropertyChanged(); } }
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand CT_PhieuDatBanCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        bool Enable()
        {
            if (SelectedPDB == null)
                return false;
            if (LoaiBan != SelectedPDB.LoaiBan)
                return false;
            if (SoLuong != SelectedPDB.SoLuong)
                return false;
            if (SoLuongDuTru != SelectedPDB.SoLuongDuTru)
                return false;
            if (GhiChu != SelectedPDB.GhiChu)
                return false;
            return true;
        }
        bool Addable()
        {
            if (String.IsNullOrEmpty(LoaiBan))
                return false;
            if (SoLuong == 0)
                return false;
            return true;
        }
        public PhieuDatBanViewModel()
        {
            IsReadOnly = !LoginViewModel.ThayDoiTiec;
            AddCommand = new RelayCommand<object>((p) =>
            {
                return Addable();

            }, (p) =>
            {
                try
                {
                    var PhieuDatBan = new PHIEUDATBAN()
                    {
                        MaTiecCuoi = CurrentMaTiecCuoi,
                        LoaiBan = LoaiBan,
                        SoLuong = Convert.ToInt32(SoLuong),
                        SoLuongDuTru = Convert.ToInt32(SoLuongDuTru),
                        DonGiaBan = DonGiaBan,
                        GhiChu = GhiChu
                    };
                    //MessageBox.Show(PhieuDatBan.MaTiecCuoi + " " + PhieuDatBan.LoaiBan+ " " + PhieuDatBan.SoLuong + " " + PhieuDatBan.SoLuongDuTru + " " + PhieuDatBan.DonGiaBan);
                    DataProvider.Ins.DataBase.PHIEUDATBANs.Add(PhieuDatBan);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListPhieuDatBan.Add(PhieuDatBan);
                    SelectedPDB = PhieuDatBan;
                    MessageBox.Show("Thêm phiếu đặt bàn thành công", "Thông báo", MessageBoxButton.OK);
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("Thêm phiếu đặt bàn không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }
            });
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (Enable())
                    return false;
                var displayList = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return Addable();
            }, (p) =>
            {
                try
                {
                    var PhieuDatBan = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan).SingleOrDefault();
                    PhieuDatBan.MaPhieuDatBan = SelectedPDB.MaPhieuDatBan;
                    PhieuDatBan.MaTiecCuoi = CurrentMaTiecCuoi;
                    PhieuDatBan.LoaiBan = SelectedPDB.LoaiBan;
                    PhieuDatBan.SoLuong = SelectedPDB.SoLuong;
                    PhieuDatBan.SoLuongDuTru = SelectedPDB.SoLuongDuTru;
                    PhieuDatBan.DonGiaBan = SelectedPDB.DonGiaBan;
                    PhieuDatBan.GhiChu = SelectedPDB.GhiChu;
                    DataProvider.Ins.DataBase.SaveChanges();
                    MessageBox.Show("Sửa phiếu đặt bàn thành công", "Thông báo", MessageBoxButton.OK);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Sửa phiếu đặt bàn không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }
            });
            CT_PhieuDatBanCommand = new RelayCommand<object>((p) => { return Enable(); }, (p) => {
                CT_PhieuDatBanViewModel.CurrentMaPDB = SelectedPDB.MaPhieuDatBan;
                CT_PhieuDatBanViewModel.ListCTPhieuDatBan = new ObservableCollection<CT_PHIEUDATBAN>(DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan));
                CT_PhieuDatBanWindow wd = new CT_PhieuDatBanWindow();
                wd.ShowDialog();
                int count = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == MaPhieuDatBan).Count();
                if (count != 0)
                    DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == MaPhieuDatBan).Sum(ct => ct.ThanhTien);
                var PhieuDatBan = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan).SingleOrDefault();
                PhieuDatBan.DonGiaBan = DonGiaBan;
                DataProvider.Ins.DataBase.SaveChanges();
                while (DonGiaBan < DonGiaBanToiThieu)
                {
                    MessageBoxResult result = MessageBox.Show("Đơn giá bàn thấp hơn đơn giá bàn tối thiểu !!!\n" +
                        "Đơn giá bàn sẽ được thay đổi giá trị bằng đơn giá bàn tối thiểu", "Cảnh báo", MessageBoxButton.YesNo);
                    if(result == MessageBoxResult.Yes)
                    {
                        DonGiaBan = DonGiaBanToiThieu;
                        try
                        {
                            var DichVu = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan).SingleOrDefault();
                            DichVu.DonGiaBan = DonGiaBan;
                            DataProvider.Ins.DataBase.SaveChanges();
                            MessageBox.Show("Đơn giá bàn đã được thay đổi giá trị bằng đơn giá bàn tối thiểu", "Thông báo", MessageBoxButton.OK);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Thay đổi phiếu đặt bàn không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                        }
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Xin thay đổi chi tiết đặt bàn cho đến khi Đơn giá bàn lớn hơn hoặc bằng Đơn giá bàn tối thiểu", "Thông báo", MessageBoxButton.OK);
                        CT_PhieuDatBanWindow ct = new CT_PhieuDatBanWindow();
                        ct.ShowDialog();
                    }
                    int cc = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == MaPhieuDatBan).Count();
                    if (cc != 0)
                        DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == MaPhieuDatBan).Sum(ct => ct.ThanhTien);
                    else
                        DonGiaBan = 0;
                    if (DonGiaBan >= DonGiaBanToiThieu)
                    {
                        var temp = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan).SingleOrDefault();
                        temp.DonGiaBan = DonGiaBan;
                      //  MessageBox.Show(PhieuDatBan.DonGiaBan.ToString());
                        DataProvider.Ins.DataBase.SaveChanges();
                    }
                }

            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa \n Phiếu đặt bàn này không", "Cảnh báo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int count = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan).Count();
                        for(int i =0; i< count; i++)
                        {
                            CT_PHIEUDATBAN temp = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan).First();
                            DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Remove(temp);
                            DataProvider.Ins.DataBase.SaveChanges();
                        }
                        var PhieuDatBan = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan).First();
                        DataProvider.Ins.DataBase.PHIEUDATBANs.Remove(PhieuDatBan);
                        DataProvider.Ins.DataBase.SaveChanges();
                        ListPhieuDatBan.Remove(PhieuDatBan);
                        // Refresh
                        LoaiBan = GhiChu =  String.Empty;
                        SoLuong = SoLuongDuTru = 0;
                        DonGiaBan = DonGiaBanToiThieu;                       
                        MessageBox.Show("Xóa phiếu đặt bàn thành công", "Thông báo", MessageBoxButton.OK);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Xóa phiếu đặt bàn không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                    }
                }
                
            });
        }

    }
}
