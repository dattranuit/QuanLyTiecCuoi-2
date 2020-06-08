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
        private ObservableCollection<PHIEUDATBAN> _List;
        public ObservableCollection<PHIEUDATBAN> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private PHIEUDATBAN _SelectedItem;
        public PHIEUDATBAN SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaPhieuDatBan = SelectedItem.MaPhieuDatBan;
                    MaTiecCuoi = SelectedItem.MaTiecCuoi;
                    MaLoaiBan = SelectedItem.MaLoaiBan;
                    SoLuong = SelectedItem.SoLuong;
                    SoLuongDuTru = SelectedItem.SoLuongDuTru;
                    DonGiaBan = SelectedItem.DonGiaBan;
                    GhiChu = SelectedItem.GhiChu;
                }
            }
        }
        private string _MaPhieuDatBan { get; set; }
        private string _MaTiecCuoi { get; set; }
        private string _MaLoaiBan { get; set; }
        private Nullable<int> _SoLuong { get; set; }
        private Nullable<int> _SoLuongDuTru { get; set; }
        private Nullable<decimal> _DonGiaBan { get; set; }
        private string _GhiChu { get; set; }
        public string MaPhieuDatBan { get; set; }
        public string MaTiecCuoi { get; set; }
        public string MaLoaiBan { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<int> SoLuongDuTru { get; set; }
        public Nullable<decimal> DonGiaBan { get; set; }
        public string GhiChu { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public PhieuDatBanViewModel()
        {
            List = new ObservableCollection<PHIEUDATBAN>(DataProvider.Ins.DataBase.PHIEUDATBANs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var PhieuDatBan = new PHIEUDATBAN()
                {
                    MaPhieuDatBan = MaPhieuDatBan,
                    MaTiecCuoi = MaTiecCuoi,
                    MaLoaiBan = MaLoaiBan,
                    SoLuong = SoLuong,
                    SoLuongDuTru = SoLuongDuTru,
                    DonGiaBan = DonGiaBan,
                    GhiChu = GhiChu
            };
                DataProvider.Ins.DataBase.PHIEUDATBANs.Add(PhieuDatBan);
                DataProvider.Ins.DataBase.SaveChanges();
                List.Add(PhieuDatBan);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedItem.MaPhieuDatBan);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var DichVu = DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaPhieuDatBan == SelectedItem.MaPhieuDatBan).SingleOrDefault();
                DichVu.MaPhieuDatBan = SelectedItem.MaPhieuDatBan;
                DichVu.MaTiecCuoi = SelectedItem.MaTiecCuoi;
                DichVu.MaLoaiBan = SelectedItem.MaLoaiBan;
                DichVu.SoLuong = SelectedItem.SoLuong;
                DichVu.SoLuongDuTru = SelectedItem.SoLuongDuTru;
                DichVu.DonGiaBan = SelectedItem.DonGiaBan;
                DichVu.GhiChu = SelectedItem.GhiChu;
                DataProvider.Ins.DataBase.SaveChanges();
            });
        }

    }
}
