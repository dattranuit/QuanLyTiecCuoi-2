using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;

namespace QuanLyTiecCuoi.ViewModel
{
    class CT_PhieuDatBanViewModel:BaseViewModel
    {
        private ObservableCollection<CT_PHIEUDATBAN> _ListCTPhieuDatBan;
        public ObservableCollection<CT_PHIEUDATBAN> ListCTPhieuDatBan { get => _ListCTPhieuDatBan; set { _ListCTPhieuDatBan = value; OnPropertyChanged(); } }
        private ObservableCollection<MONAN> _ListMonAn;
        public ObservableCollection<MONAN> ListMonAn { get => _ListMonAn; set { _ListMonAn = value; OnPropertyChanged(); } }
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
        private int _MaPhieuDatBan;
        public int MaPhieuDatBan { get => _MaPhieuDatBan; set { _MaPhieuDatBan = value; OnPropertyChanged(); } }
        private int _MaMonAn;
        public int MaMonAn { get => _MaMonAn; set { _MaMonAn = value; OnPropertyChanged(); } }
        private int _CTPDB_SoLuong;
        public int CTPDB_SoLuong { get => _CTPDB_SoLuong; set { _CTPDB_SoLuong = value; OnPropertyChanged(); } }
        private decimal _CTPDB_ThanhTien;
        public decimal CTPDB_ThanhTien { get => _CTPDB_ThanhTien; set { _CTPDB_ThanhTien = value; OnPropertyChanged(); } }
        private int _MA_SoLuong;
        public int MA_SoLuong { get => _MA_SoLuong; set { _MA_SoLuong = value; OnPropertyChanged(); } }
        private decimal _MA_ThanhTien;
        public decimal MA_ThanhTien { get => _MA_ThanhTien; set { _MA_ThanhTien = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public CT_PhieuDatBanViewModel()
        {
            ListCTPhieuDatBan = new ObservableCollection<CT_PHIEUDATBAN>(DataProvider.Ins.DataBase.CT_PHIEUDATBAN);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var CT_PhieuDatBan = new CT_PHIEUDATBAN()
                {
                    MaPhieuDatBan = MaPhieuDatBan,
                    MaMonAn = MaMonAn,
                    SoLuong = MA_SoLuong,
                    ThanhTien = MA_ThanhTien,
                };
                DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Add(CT_PhieuDatBan);
                DataProvider.Ins.DataBase.SaveChanges();
                ListCTPhieuDatBan.Add(CT_PhieuDatBan);
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
        }
    }
}
