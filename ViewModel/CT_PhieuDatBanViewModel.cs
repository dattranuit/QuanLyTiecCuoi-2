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
        private static int _CurrentMaPDB;
        public static int CurrentMaPDB { get => _CurrentMaPDB; set { _CurrentMaPDB = value;} }
        private decimal _DonGiaBan = 0;
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
                }
            }
        }
        //private bool _IsClicked;
        //public bool IsClicked { get => _IsClicked; set { _IsClicked = value; OnPropertyChanged(); } }
        private int _MaMonAn;
        public int MaMonAn { get => _MaMonAn; set { _MaMonAn = value; OnPropertyChanged(); } }
        private int _CTPDB_SoLuong;
        public int CTPDB_SoLuong { get => _CTPDB_SoLuong; set
            {
                if (SelectedCTPDB != null)
                {
                    _CTPDB_SoLuong = value;
                    if (_CTPDB_SoLuong < 0)
                        _CTPDB_SoLuong = 0;
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
                        MA_SoLuong = 0;
                    MA_ThanhTien = MA_SoLuong * SelectedMA.DonGia;
                }
                OnPropertyChanged(); } }
        private decimal _MA_ThanhTien = 0;
        public decimal MA_ThanhTien { get => _MA_ThanhTien; set { _MA_ThanhTien = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        //public ICommand PopupCommand { get; set; }
        bool Addable()
        {
            if (SelectedMA == null)
                return false;
            if (MA_SoLuong == 0)
                return false;
            var CheckExist = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB && x.MaMonAn == SelectedMA.MaMonAn).FirstOrDefault();
            if (CheckExist != null)
                return false;
            return true;
        }
        public CT_PhieuDatBanViewModel()
        {
            DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB).Sum(ct => ct.ThanhTien);
            ListMonAn = new ObservableCollection<MONAN>(DataProvider.Ins.DataBase.MONANs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return Addable();
            }, (p) =>
            {
                var CT_PhieuDatBan = new CT_PHIEUDATBAN()
                {
                    MaPhieuDatBan = CurrentMaPDB,
                    MaMonAn = MaMonAn,
                    SoLuong = MA_SoLuong,
                    ThanhTien = MA_ThanhTien,
                };
                //MessageBox.Show(CT_PhieuDatBan.MaPhieuDatBan + " " + CT_PhieuDatBan.MaMonAn + " " + CT_PhieuDatBan.SoLuong + " " + CT_PhieuDatBan.ThanhTien);
                DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Add(CT_PhieuDatBan);
                DataProvider.Ins.DataBase.SaveChanges();
                ListCTPhieuDatBan.Add(CT_PhieuDatBan);
                DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == CurrentMaPDB).Sum(ct => ct.ThanhTien);
            });
            //EditCommand = new RelayCommand<object>((p) =>
            //{
            //    if (SelectedPDB == null)
            //        return false;
            //    var displayList = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan);
            //    if (displayList != null && displayList.Count() != 0)
            //        return true;
            //    return false;
            //}, (p) =>
            //{
            //    var DichVu = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan).SingleOrDefault();
            //    DichVu.MaPhieuDatBan = SelectedPDB.MaPhieuDatBan;
            //    DichVu.MaTiecCuoi = SelectedPDB.MaTiecCuoi;
            //    DichVu.SoLuong = SelectedPDB.SoLuong;
            //    DichVu.SoLuongDuTru = SelectedPDB.SoLuongDuTru;
            //    DichVu.DonGiaBan = SelectedPDB.DonGiaBan;
            //    DichVu.GhiChu = SelectedPDB.GhiChu;
            //    DataProvider.Ins.DataBase.SaveChanges();
            //});
            //PopupCommand = new RelayCommand<object>((p) => true, (p) => { IsClicked = !IsClicked; });
        }
    }
}
