using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;
using System.Windows;

namespace QuanLyTiecCuoi.ViewModel
{
    class CT_PhieuDatBanViewModel:BaseViewModel
    {
        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }
        private bool _IsReadOnly;
        public bool IsReadOnly { get => _IsReadOnly; set { _IsReadOnly = value; IsEnable = !_IsReadOnly; OnPropertyChanged(); } }
        private static int _CurrentMaPDB;
        public static int CurrentMaPDB { get => _CurrentMaPDB; set { _CurrentMaPDB = value;} }
        private decimal _DonGiaBan;
        public decimal DonGiaBan { get => _DonGiaBan; set { if (value != _DonGiaBan) OnPropertyChanged(); _DonGiaBan = value; OnPropertyChanged(); } }
        private static ObservableCollection<CT_PHIEUDATBAN> _ListCTPhieuDatBan;
        public static ObservableCollection<CT_PHIEUDATBAN> ListCTPhieuDatBan { get => _ListCTPhieuDatBan; set { _ListCTPhieuDatBan = value; } }
        private static ObservableCollection<MONAN> _ListMonAn;
        public static ObservableCollection<MONAN> ListMonAn { get => _ListMonAn; set { _ListMonAn = value; } }
        private CT_PHIEUDATBAN _SelectedCTPDB;
        public CT_PHIEUDATBAN SelectedCTPDB
        {
            get => _SelectedCTPDB;
            set
            {
                _SelectedCTPDB = value;
                OnPropertyChanged();
                if (SelectedCTPDB != null)
                {
                    MaMonAn = SelectedCTPDB.MaMonAn;
                    CTPDB_SoLuong = SelectedCTPDB.SoLuong;
                    CTPDB_ThanhTien = SelectedCTPDB.ThanhTien;
                    CTPDB_GhiChu = SelectedCTPDB.GhiChu;
                }
            }
        }
        private MONAN _SelectedMA;
        public MONAN SelectedMA
        {
            get => _SelectedMA;
            set
            {
                _SelectedMA = value;
                OnPropertyChanged();
                if (SelectedMA != null)
                {
                    MaMonAn = SelectedMA.MaMonAn;
                    MA_SoLuong = 0;
                    MA_ThanhTien = 0;
                    MA_GhiChu = String.Empty;
                }
            }
        }
        //private bool _IsClicked;
        //public bool IsClicked { get => _IsClicked; set { _IsClicked = value; OnPropertyChanged(); } }
        private int _MaMonAn;
        public int MaMonAn { get => _MaMonAn; set { _MaMonAn = value; OnPropertyChanged(); } }
        private string _CTPDB_GhiChu;
        public string CTPDB_GhiChu { get => _CTPDB_GhiChu; set { _CTPDB_GhiChu = value; OnPropertyChanged(); } }
        private int _CTPDB_SoLuong;
        public int CTPDB_SoLuong { get => _CTPDB_SoLuong; set
            {
                if (SelectedCTPDB != null)
                {
                    _CTPDB_SoLuong = value;
                    if (_CTPDB_SoLuong < 1)
                    {
                        _CTPDB_SoLuong = 1;
                        MessageBox.Show("Số lượng phải lớn hơn 0", "Lưu ý", MessageBoxButton.OK);
                    }
  
                    CTPDB_ThanhTien = CTPDB_SoLuong * SelectedCTPDB.MONAN.DonGia;
                }
                OnPropertyChanged();
            }
        }
        private decimal _CTPDB_ThanhTien;
        public decimal CTPDB_ThanhTien { get => _CTPDB_ThanhTien; set { _CTPDB_ThanhTien = value; OnPropertyChanged(); } }
        private int _MA_SoLuong = 0;
        public int MA_SoLuong { get => _MA_SoLuong; set {
                if(SelectedMA!=null)
                {
                    _MA_SoLuong = value;
                    if (MA_SoLuong < 0)
                    {
                        MA_SoLuong = 0;
                        MessageBox.Show("Số lượng không được âm", "Lưu ý", MessageBoxButton.OK);
                    }
                    MA_ThanhTien = MA_SoLuong * SelectedMA.DonGia;
                }
                OnPropertyChanged(); } }
        private decimal _MA_ThanhTien = 0;
        public decimal MA_ThanhTien { get => _MA_ThanhTien; set { _MA_ThanhTien = value; OnPropertyChanged(); } }
        private string _MA_GhiChu;
        public string MA_GhiChu { get => _MA_GhiChu; set { _MA_GhiChu = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        //public ICommand PopupCommand { get; set; }
        bool Addable()
        {
            if (SelectedMA == null)
                return false;
            var CheckExist = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB && x.MaMonAn == SelectedMA.MaMonAn).FirstOrDefault();
            if (CheckExist != null)
                return false;
            return true;
        }
        public CT_PhieuDatBanViewModel()
        {
            IsReadOnly = !LoginViewModel.ThayDoiTiec;
            int count = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB).Count();
            if (count != 0)
                DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB).Sum(ct => ct.ThanhTien);
            else
                DonGiaBan = 0;
            ListMonAn = new ObservableCollection<MONAN>(DataProvider.Ins.DataBase.MONANs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return Addable();
            }, (p) =>
            {
                if (MA_SoLuong == 0)
                    MessageBox.Show("Số lượng phải lớn hơn 0", "Lưu ý", MessageBoxButton.OK);
                else
                {
                    try
                    {
                        var CT_PhieuDatBan = new CT_PHIEUDATBAN()
                        {
                            MaPhieuDatBan = CurrentMaPDB,
                            MaMonAn = MaMonAn,
                            SoLuong = MA_SoLuong,
                            ThanhTien = MA_ThanhTien,
                            GhiChu = MA_GhiChu,
                        };
                        //MessageBox.Show(CT_PhieuDatBan.MaPhieuDatBan + " " + CT_PhieuDatBan.MaMonAn + " " + CT_PhieuDatBan.SoLuong + " " + CT_PhieuDatBan.ThanhTien);
                        DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Add(CT_PhieuDatBan);
                        DataProvider.Ins.DataBase.SaveChanges();
                        ListCTPhieuDatBan.Add(CT_PhieuDatBan);
                        SelectedCTPDB = CT_PhieuDatBan;
                        DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB).Sum(ct => ct.ThanhTien);
                        MessageBox.Show("Thêm chi tiết phiếu đặt bàn thành công", "Thông báo", MessageBoxButton.OK);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Thêm chi tiết phiếu đặt bàn không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                    }
                }

            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedCTPDB == null || (CTPDB_SoLuong == SelectedCTPDB.SoLuong && CTPDB_GhiChu == SelectedCTPDB.GhiChu ))
                    return false;
                var displayList = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == SelectedCTPDB.MaPhieuDatBan && x.MaMonAn == SelectedCTPDB.MaMonAn);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                try
                {
                    var CT_PhieuDatBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == SelectedCTPDB.MaPhieuDatBan && x.MaMonAn == SelectedCTPDB.MaMonAn).SingleOrDefault();
                    CT_PhieuDatBan.MaMonAn = SelectedCTPDB.MaMonAn;
                    CT_PhieuDatBan.MaPhieuDatBan = SelectedCTPDB.MaPhieuDatBan;
                    CT_PhieuDatBan.SoLuong = CTPDB_SoLuong;
                    CT_PhieuDatBan.ThanhTien = CTPDB_ThanhTien;
                    CT_PhieuDatBan.GhiChu = CTPDB_GhiChu;
                    DataProvider.Ins.DataBase.SaveChanges();
                    DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB).Sum(ct => ct.ThanhTien);
                    MessageBox.Show("Sửa chi tiết phiếu đặt bàn thành công", "Thông báo", MessageBoxButton.OK);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Sửa chi tiết phiếu đặt bàn không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    var CT_PhieuDatBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == SelectedCTPDB.MaPhieuDatBan && x.MaMonAn == SelectedCTPDB.MaMonAn).First();
                    DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Remove(CT_PhieuDatBan);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListCTPhieuDatBan.Remove(CT_PhieuDatBan);
                    int cc = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB).Count();
                    if (cc != 0)
                        DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB).Sum(ct => ct.ThanhTien);
                    else
                        DonGiaBan = 0;
                    MessageBox.Show("Xóa chi tiết phiếu đặt bàn thành công", "Thông báo", MessageBoxButton.OK);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Xóa chi tiết phiếu đặt bàn không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }
            });
            LoadedWindowCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SelectedCTPDB = null;
                SelectedMA = null;
                CTPDB_SoLuong = MA_SoLuong = 0;
                CTPDB_GhiChu = MA_GhiChu = String.Empty;
            });
        }
    }
}
