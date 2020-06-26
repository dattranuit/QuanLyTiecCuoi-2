using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;
using System.Collections.ObjectModel;

namespace QuanLyTiecCuoi.ViewModel
{
    class PhieuDatDichVuViewModel:BaseViewModel
    {
        private ObservableCollection<PHIEUDATDICHVU> _ListPhieuDatDichVu;
        public ObservableCollection<PHIEUDATDICHVU> ListPhieuDatDichVu { get => _ListPhieuDatDichVu; set { _ListPhieuDatDichVu = value; OnPropertyChanged(); } }
        private ObservableCollection<DICHVU> _ListDichVu;
        public ObservableCollection<DICHVU> ListDichVu { get => _ListDichVu; set { _ListDichVu = value; OnPropertyChanged(); } }

        private PHIEUDATDICHVU _SelectedItem;
        public PHIEUDATDICHVU SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        private int _MaTiecCuoi;
        public int MaTiecCuoi { get => _MaTiecCuoi; set { _MaTiecCuoi = value; OnPropertyChanged(); } }
        private int _MaDichVu;
        public int MaDichVu { get => _MaDichVu; set { _MaDichVu = value; OnPropertyChanged(); } }
        private int _SoLuong = 0;
        public int SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }
        private decimal _DonGia;
        public decimal DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public PhieuDatDichVuViewModel()
        {
            ListPhieuDatDichVu = new ObservableCollection<PHIEUDATDICHVU>(DataProvider.Ins.DataBase.PHIEUDATDICHVUs);
            ListDichVu = new ObservableCollection<DICHVU>(DataProvider.Ins.DataBase.DICHVUs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var PhieuDatDichVu = new PHIEUDATDICHVU()
                {
                    MaTiecCuoi = MaTiecCuoi,
                    MaDichVu = MaDichVu,
                    SoLuong = SoLuong,
                    DonGia = DonGia
            };
                DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Add(PhieuDatDichVu);
                DataProvider.Ins.DataBase.SaveChanges();
                ListPhieuDatDichVu.Add(PhieuDatDichVu);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaDichVu == SelectedItem.MaDichVu && x.MaTiecCuoi == SelectedItem.MaTiecCuoi);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var DichVu = DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaDichVu == SelectedItem.MaDichVu && x.MaTiecCuoi == SelectedItem.MaTiecCuoi).SingleOrDefault();
                DichVu.MaDichVu = SelectedItem.MaDichVu;
                DichVu.MaTiecCuoi = SelectedItem.MaTiecCuoi;
                DichVu.SoLuong = SelectedItem.SoLuong;
                DichVu.DonGia = SelectedItem.DonGia;
                DataProvider.Ins.DataBase.SaveChanges();
            });
            var ShowRecord = new PhieuDatBanViewModel()
            {

            };

        }
    }
}
