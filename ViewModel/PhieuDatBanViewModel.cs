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
    class PhieuDatBanViewModel : BaseViewModel
    {
        private ObservableCollection<PHIEUDATBAN> _ListPhieuDatBan;
        public ObservableCollection<PHIEUDATBAN> ListPhieuDatBan { get => _ListPhieuDatBan; set { _ListPhieuDatBan = value; OnPropertyChanged(); } }

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
                    MaPhieuDatBan = SelectedPDB.MaPhieuDatBan;
                    MaTiecCuoi = SelectedPDB.MaTiecCuoi;
                    SoLuong = SelectedPDB.SoLuong;
                    SoLuongDuTru = SelectedPDB.SoLuongDuTru;
                    DonGiaBan = SelectedPDB.DonGiaBan;
                    GhiChu = SelectedPDB.GhiChu;
                }
            }
        }
        private int _MaPhieuDatBan;
        private int _MaTiecCuoi = 0;
        private int _SoLuong = 0;
        private int _SoLuongDuTru = 0;
        private decimal _DonGiaBan = 0;
        private string _GhiChu;
        public int MaPhieuDatBan { get => _MaPhieuDatBan; set { _MaPhieuDatBan = value; OnPropertyChanged(); } }
        public int MaTiecCuoi { get => _MaTiecCuoi; set { _MaTiecCuoi = value; OnPropertyChanged(); } }
        public int SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }
        public int SoLuongDuTru { get => _SoLuongDuTru; set { _SoLuongDuTru = value; OnPropertyChanged(); } }
        public decimal DonGiaBan { get => _DonGiaBan; set { _DonGiaBan = value; OnPropertyChanged(); } }
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public PhieuDatBanViewModel()
        {
            ListPhieuDatBan = new ObservableCollection<PHIEUDATBAN>(DataProvider.Ins.DataBase.PHIEUDATBANs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var PhieuDatBan = new PHIEUDATBAN()
                {
                    MaPhieuDatBan = MaPhieuDatBan,
                    MaTiecCuoi = MaTiecCuoi,
                    SoLuong = SoLuong,
                    SoLuongDuTru = SoLuongDuTru,
                    DonGiaBan = DonGiaBan,
                    GhiChu = GhiChu
            };
                DataProvider.Ins.DataBase.PHIEUDATBANs.Add(PhieuDatBan);
                DataProvider.Ins.DataBase.SaveChanges();
                ListPhieuDatBan.Add(PhieuDatBan);
            });
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedPDB == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var DichVu = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan).SingleOrDefault();
                DichVu.MaPhieuDatBan = SelectedPDB.MaPhieuDatBan;
                DichVu.MaTiecCuoi = SelectedPDB.MaTiecCuoi;
                DichVu.SoLuong = SelectedPDB.SoLuong;
                DichVu.SoLuongDuTru = SelectedPDB.SoLuongDuTru;
                DichVu.DonGiaBan = SelectedPDB.DonGiaBan;
                DichVu.GhiChu = SelectedPDB.GhiChu;
                DataProvider.Ins.DataBase.SaveChanges();
            });
        }

    }
}
