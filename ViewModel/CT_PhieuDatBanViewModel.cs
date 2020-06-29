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
        private static int _CurrentMaPDB;
        public static int CurrentMaPDB { get => _CurrentMaPDB; set => _CurrentMaPDB = value; }
        private decimal _DonGiaBanToiThieu;
        public decimal DonGiaBanToiThieu { get => _DonGiaBanToiThieu; set { _DonGiaBanToiThieu = value; OnPropertyChanged(); } }
        private decimal _DonGiaBan = 0;
        public decimal DonGiaBan { get => _DonGiaBan; set { _DonGiaBan = value; OnPropertyChanged(); } }
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
                    DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBANs.Where(x => x.MaPhieuDatBan == CurrentMaPDB && x.MaMonAn == MaMonAn).Sum(ct => ct.ThanhTien);
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
                    DMA_SoLuong = 0;
                    DMA_ThanhTien = 0;
                    DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBANs.Where(x => x.MaPhieuDatBan == CurrentMaPDB && x.MaMonAn == MaMonAn).Sum(ct => ct.ThanhTien);
                }
            }
        }
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
                    DonGiaBan += CTPDB_ThanhTien;
                }
                OnPropertyChanged();
            }
        }
        private decimal _CTPDB_ThanhTien;
        public decimal CTPDB_ThanhTien { get => _CTPDB_ThanhTien; set { _CTPDB_ThanhTien = value; OnPropertyChanged(); } }
        private int _DMA_SoLuong = 0;
        public int DMA_SoLuong { get => _DMA_SoLuong; set {
                if(SelectedMA!=null)
                {
                    _DMA_SoLuong = value;
                    if (DMA_SoLuong < 0)
                        DMA_SoLuong = 0;
                    DMA_ThanhTien = DMA_SoLuong * SelectedMA.DonGia;
                    DonGiaBan += DMA_ThanhTien;
                }
                OnPropertyChanged(); } }
        private decimal _DMA_ThanhTien = 0;
        public decimal DMA_ThanhTien { get => _DMA_ThanhTien; set { _DMA_ThanhTien = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public CT_PhieuDatBanViewModel()
        {
            var CurrentTiecCuoi = DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == TiecViewModel.CurrentMaTiecCuoi).SingleOrDefault();
            DonGiaBanToiThieu = CurrentTiecCuoi.SANH.LOAISANH.DonGiaBanToiThieu;
            ListCTPhieuDatBan = new ObservableCollection<CT_PHIEUDATBAN>(DataProvider.Ins.DataBase.CT_PHIEUDATBANs);
            ListMonAn = new ObservableCollection<MONAN>(DataProvider.Ins.DataBase.MONANs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var CT_PhieuDatBan = new CT_PHIEUDATBAN()
                {
                    MaPhieuDatBan = CurrentMaPDB,
                    MaMonAn = MaMonAn,
                    SoLuong = DMA_SoLuong,
                    ThanhTien = DMA_ThanhTien,
                };
                DataProvider.Ins.DataBase.CT_PHIEUDATBANs.Add(CT_PhieuDatBan);
                DataProvider.Ins.DataBase.SaveChanges();
                ListCTPhieuDatBan.Add(CT_PhieuDatBan);
                DonGiaBan = DataProvider.Ins.DataBase.CT_PHIEUDATBANs.Where(x=> x.MaPhieuDatBan == CurrentMaPDB && x.MaMonAn == MaMonAn).Sum(ct => ct.ThanhTien);
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
