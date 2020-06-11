using System;
using QuanLyTiecCuoi.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyTiecCuoi.ViewModel
{
    class HoaDonViewModel : BaseViewModel
    {
        private ObservableCollection<HOADON> _List;
        public ObservableCollection<HOADON> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private HOADON _SelectedItem;
        public HOADON SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TongTienBan = SelectedItem.TongTienBan;
                    TongTienDichVu = SelectedItem.TongTienDichVu;
                    TongTienHoaDon = SelectedItem.TongTienHoaDon;
                    ConLai = SelectedItem.ConLai;
                    NgayThanhToan = SelectedItem.NgayThanhToan;
                }
            }
        }

        private decimal? _TongTienBan;
        public decimal? TongTienBan { get => _TongTienBan; set { _TongTienBan = value; OnPropertyChanged(); } }


        private decimal? _TongTienDichVu;
        public decimal? TongTienDichVu { get => _TongTienDichVu; set { _TongTienDichVu = value; OnPropertyChanged(); } }


        private decimal? _TongTienHoaDon;
        public decimal? TongTienHoaDon { get => _TongTienHoaDon; set { _TongTienHoaDon = value; OnPropertyChanged(); } }


        private decimal? _ConLai;
        public decimal? ConLai { get => _ConLai; set { _ConLai = value; OnPropertyChanged(); } }


        private DateTime? _NgayThanhToan;
        public DateTime? NgayThanhToan { get => _NgayThanhToan; set { _NgayThanhToan = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand LapHoaDonCommand { get; set; }

        public HoaDonViewModel()
        {
            List = new ObservableCollection<HOADON>(DataProvider.Ins.DataBase.HOADONs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var HoaDon = new HOADON() { TongTienBan = TongTienBan, TongTienDichVu = TongTienDichVu, TongTienHoaDon = TongTienHoaDon, ConLai = ConLai, NgayThanhToan = NgayThanhToan };

                DataProvider.Ins.DataBase.HOADONs.Add(HoaDon);
                DataProvider.Ins.DataBase.SaveChanges();

                List.Add(HoaDon);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DataBase.HOADONs.Where(x => x.MaHoaDon == SelectedItem.MaHoaDon);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var HoaDon = DataProvider.Ins.DataBase.HOADONs.Where(x => x.MaHoaDon == SelectedItem.MaHoaDon).SingleOrDefault();
                HoaDon.TongTienBan = SelectedItem.TongTienBan;
                HoaDon.TongTienDichVu = SelectedItem.TongTienDichVu;
                HoaDon.TongTienHoaDon = SelectedItem.TongTienHoaDon;
                HoaDon.ConLai = SelectedItem.ConLai;
                HoaDon.NgayThanhToan = SelectedItem.NgayThanhToan;
                DataProvider.Ins.DataBase.SaveChanges();
                SelectedItem.TongTienBan = TongTienBan;
            });

            LapHoaDonCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HoaDon wd = new HoaDon(); wd.ShowDialog(); });
        }
    }
}
