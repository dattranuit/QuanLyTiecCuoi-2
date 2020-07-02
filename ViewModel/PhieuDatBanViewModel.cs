using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;

namespace QuanLyTiecCuoi.ViewModel
{
    class PhieuDatBanViewModel : BaseViewModel
    {
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
            if (DonGiaBan != SelectedPDB.DonGiaBan)
                return false;
            if (GhiChu != SelectedPDB.GhiChu)
                return false;
            return true;
        }
        public PhieuDatBanViewModel()
        {
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
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
                DichVu.MaTiecCuoi = CurrentMaTiecCuoi;
                DichVu.LoaiBan = SelectedPDB.LoaiBan;
                DichVu.SoLuong = SelectedPDB.SoLuong;
                DichVu.SoLuongDuTru = SelectedPDB.SoLuongDuTru;
                DichVu.DonGiaBan = SelectedPDB.DonGiaBan;
                DichVu.GhiChu = SelectedPDB.GhiChu;
                DataProvider.Ins.DataBase.SaveChanges();
            });
            CT_PhieuDatBanCommand = new RelayCommand<object>((p) => { return Enable(); }, (p) => {
                CT_PhieuDatBanViewModel.CurrentMaPDB = SelectedPDB.MaPhieuDatBan;
                CT_PhieuDatBanViewModel.ListCTPhieuDatBan = new ObservableCollection<CT_PHIEUDATBAN>(DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == SelectedPDB.MaPhieuDatBan));
                //foreach (CT_PHIEUDATBAN item in CT_PhieuDatBanViewModel.ListCTPhieuDatBan)
                //    MessageBox.Show(item.MaPhieuDatBan+"");
                CT_PhieuDatBanWindow wd = new CT_PhieuDatBanWindow();
                wd.ShowDialog();
            });
        }

    }
}
