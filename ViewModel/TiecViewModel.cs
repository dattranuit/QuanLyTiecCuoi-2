using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;
using System.Windows.Controls;
using System.Data;

namespace QuanLyTiecCuoi.ViewModel
{
    class TiecViewModel : BaseViewModel
    {
        private ObservableCollection<TIECCUOI> _ListTiecCuoi;
        public ObservableCollection<TIECCUOI> ListTiecCuoi { get => _ListTiecCuoi; set { _ListTiecCuoi = value; OnPropertyChanged(); } }
        private ObservableCollection<CA> _ListCa;
        public ObservableCollection<CA> ListCa { get => _ListCa; set { _ListCa = value; OnPropertyChanged(); } }
        private ObservableCollection<SANH> _ListSanh;
        public ObservableCollection<SANH> ListSanh { get => _ListSanh; set { _ListSanh = value; OnPropertyChanged(); } }
        private TIECCUOI _SelectedItem;
        public TIECCUOI SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaTiecCuoi = Convert.ToInt32(SelectedItem.MaTiecCuoi);
                    TenChuRe = SelectedItem.TenChuRe;
                    TenCoDau = SelectedItem.TenCoDau;
                    SoDienThoai = SelectedItem.SoDienThoai;
                    NgayDatTiec = SelectedItem.NgayDatTiec;
                    NgayDaiTiec = SelectedItem.NgayDaiTiec;
                    TienDatCoc = SelectedItem.TienDatCoc;
                    GhiChu = SelectedItem.GhiChu;
                    MaSanh = Convert.ToInt32(SelectedItem.MaSanh);
                    MaCa = Convert.ToInt32(SelectedItem.MaCa);
                }
            }
        }
        private int _MaTiecCuoi;
        public int MaTiecCuoi { get => _MaTiecCuoi; set { _MaTiecCuoi = value; OnPropertyChanged(); } }
        private int _TongSoBan;
        public int TongSoBan { get => _TongSoBan; set { _TongSoBan = value; OnPropertyChanged(); } }
        private string _TenChuRe;
        public string TenChuRe { get => _TenChuRe; set { _TenChuRe = value; OnPropertyChanged(); } }
        private string _TenCoDau;
        public string TenCoDau { get => _TenCoDau; set { _TenCoDau = value; OnPropertyChanged(); } }
        private string _SoDienThoai;
        public string SoDienThoai { get => _SoDienThoai; set { _SoDienThoai = value; OnPropertyChanged(); } }
        private System.DateTime _NgayDatTiec = DateTime.Now;
        public System.DateTime NgayDatTiec { get => _NgayDatTiec; set { _NgayDatTiec = value; OnPropertyChanged(); } }
        private System.DateTime _NgayDaiTiec = DateTime.Now;
        public System.DateTime NgayDaiTiec { get => _NgayDaiTiec; set { _NgayDaiTiec = value; OnPropertyChanged(); } }
        private decimal _TienDatCoc;
        public decimal TienDatCoc { get => _TienDatCoc; set { _TienDatCoc = value; OnPropertyChanged(); } }
        private string _GhiChu;
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }
        private int? _MaSanh;
        public int? MaSanh { get => _MaSanh; set { _MaSanh = value; OnPropertyChanged(); } }
        private int? _MaCa;
        public int? MaCa { get => _MaCa; set { _MaCa = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DatBanvaDichVuCommand { get; set; }
        public ICommand LapHoaDonCommand { get; set; }
        bool Addable()
        {
            if (String.IsNullOrEmpty(TenChuRe))
                return false;
            if (String.IsNullOrEmpty(TenCoDau))
                return false;
            if (String.IsNullOrEmpty(SoDienThoai))
                return false;
            if (MaSanh == null)
                return false;
            if (MaCa == null)
                return false;
            return true;           
        }
        public TiecViewModel()
        {
            ListTiecCuoi = new ObservableCollection<TIECCUOI>(DataProvider.Ins.DataBase.TIECCUOIs);
            ListCa = new ObservableCollection<CA>(DataProvider.Ins.DataBase.CAs);
            ListSanh = new ObservableCollection<SANH>(DataProvider.Ins.DataBase.SANHs);
            AddCommand = new RelayCommand<object>((p) => Addable() , (p) =>
            {
                SelectedItem = new TIECCUOI()
                {
                    TenChuRe = TenChuRe,
                    TenCoDau = TenCoDau,
                    SoDienThoai = SoDienThoai,
                    NgayDatTiec = NgayDatTiec,
                    NgayDaiTiec = NgayDaiTiec,
                    TienDatCoc = TienDatCoc,
                    GhiChu = GhiChu,
                    MaSanh = Convert.ToInt32(MaSanh),
                    MaCa = Convert.ToInt32(MaCa)
                };
                try
                {
                    DataProvider.Ins.DataBase.TIECCUOIs.Add(SelectedItem);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListTiecCuoi.Add(SelectedItem);
                }
                catch (Exception e)
                {
                    throw e;
                }

            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == SelectedItem.MaTiecCuoi);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var TiecCuoi = DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == SelectedItem.MaTiecCuoi).SingleOrDefault();
                TiecCuoi.TenChuRe = TenChuRe;
                TiecCuoi.TenCoDau = TenCoDau;
                TiecCuoi.SoDienThoai = SoDienThoai;
                TiecCuoi.NgayDatTiec = NgayDatTiec;
                TiecCuoi.NgayDaiTiec = NgayDaiTiec;
                TiecCuoi.TienDatCoc = TienDatCoc;
                TiecCuoi.GhiChu = GhiChu;
                TiecCuoi.MaSanh = Convert.ToInt32(MaSanh);
                TiecCuoi.MaCa = Convert.ToInt32(MaCa);
                DataProvider.Ins.DataBase.SaveChanges();
            });
            DatBanvaDichVuCommand = new RelayCommand<object>((p) => { return true; }, (p) => { DatBanvaDichVuWindow wd = new DatBanvaDichVuWindow(); wd.ShowDialog(); });
            LapHoaDonCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HoaDon wd = new HoaDon(); wd.ShowDialog(); });
        }
   

    }
}