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
        private static int _CurrentMaTiecCuoi;
        public static int CurrentMaTiecCuoi { get => _CurrentMaTiecCuoi; set => _CurrentMaTiecCuoi = value; }
        private ObservableCollection<TIECCUOI> _ListTiecCuoi;
        public ObservableCollection<TIECCUOI> ListTiecCuoi { get => _ListTiecCuoi; set { _ListTiecCuoi = value; OnPropertyChanged(); } }
        private static ObservableCollection<CA> _ListCa;
        public static ObservableCollection<CA> ListCa { get => _ListCa; set { _ListCa = value; } }
        private static ObservableCollection<SANH> _ListSanh;
        public static ObservableCollection<SANH> ListSanh { get => _ListSanh; set { _ListSanh = value;} }
        private CA _SelectedCa;
        public CA SelectedCa { get => _SelectedCa; set { _SelectedCa = value; if(SelectedCa!= null) MaCa = _SelectedCa.MaCa; OnPropertyChanged(); } }
        private SANH _SelectedSanh;
        public SANH SelectedSanh { get => _SelectedSanh; set { _SelectedSanh = value; if (SelectedSanh != null) MaSanh = _SelectedSanh.MaSanh; OnPropertyChanged(); } }
        private TIECCUOI _SelectedTiecCuoi;
        public TIECCUOI SelectedTiecCuoi
        {
            get => _SelectedTiecCuoi;
            set
            {
                _SelectedTiecCuoi = value;
                OnPropertyChanged();
                if (SelectedTiecCuoi != null)
                {
                    MaTiecCuoi = Convert.ToInt32(SelectedTiecCuoi.MaTiecCuoi);
                    TenChuRe = SelectedTiecCuoi.TenChuRe;
                    TenCoDau = SelectedTiecCuoi.TenCoDau;
                    SoDienThoai = SelectedTiecCuoi.SoDienThoai;
                    NgayDatTiec = SelectedTiecCuoi.NgayDatTiec;
                    NgayDaiTiec = SelectedTiecCuoi.NgayDaiTiec;
                    TienDatCoc = SelectedTiecCuoi.TienDatCoc;
                    GhiChu = SelectedTiecCuoi.GhiChu;
                    MaSanh = SelectedTiecCuoi.MaSanh;
                    MaCa = SelectedTiecCuoi.MaCa;
                }
            }
        }
        private int _MaTiecCuoi;
        public int MaTiecCuoi { get => _MaTiecCuoi; set { _MaTiecCuoi = value; OnPropertyChanged(); } }
        private int _TongSoBan;
        public int TongSoBan { get => _TongSoBan; set { _TongSoBan = value; OnPropertyChanged(); } }
        private string _TenChuRe;
        public string TenChuRe { get => _TenChuRe; set { if(value != _TenChuRe) OnPropertyChanged(); _TenChuRe = value; OnPropertyChanged(); } }
        private string _TenCoDau;
        public string TenCoDau { get => _TenCoDau; set { if (value != _TenCoDau) OnPropertyChanged(); _TenCoDau = value; OnPropertyChanged(); } }
        private string _SoDienThoai;
        public string SoDienThoai { get => _SoDienThoai; set { if (value != _SoDienThoai) OnPropertyChanged(); _SoDienThoai = value; OnPropertyChanged(); } }
        private System.DateTime _NgayDatTiec = DateTime.Now;
        public System.DateTime NgayDatTiec { get => _NgayDatTiec; set { if (value != _NgayDatTiec) OnPropertyChanged(); _NgayDatTiec = value; OnPropertyChanged(); } }
        private System.DateTime _NgayDaiTiec = DateTime.Now;
        public System.DateTime NgayDaiTiec { get => _NgayDaiTiec; set { if (value != _NgayDaiTiec) OnPropertyChanged(); _NgayDaiTiec = value; OnPropertyChanged(); } }
        private decimal _TienDatCoc;
        public decimal TienDatCoc { get => _TienDatCoc; 
            set {
                if (value != _TienDatCoc) 
                    OnPropertyChanged();
                _TienDatCoc = value;
                if(_TienDatCoc < 0)
                {
                    _TienDatCoc = 0;
                    MessageBox.Show("Tiền đặt cọc không thể bé hơn 0");
                }    
                OnPropertyChanged(); 
            } 
        }
        private string _GhiChu = "";
        public string GhiChu { get => _GhiChu; set { if (value != _GhiChu) OnPropertyChanged(); _GhiChu = value; OnPropertyChanged(); } }
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
            if (String.IsNullOrEmpty(NgayDaiTiec.ToString()))
                return false;
            if (String.IsNullOrEmpty(NgayDatTiec.ToString()))
                return false;
            if (MaSanh == null)
                return false;
            if (MaCa == null)
                return false;
            return true;           
        }
        bool Enable()
        {
            if (SelectedTiecCuoi == null)
                return false;
            if (TenChuRe != SelectedTiecCuoi.TenChuRe)
                return false;
            if (TenCoDau != SelectedTiecCuoi.TenCoDau)
                return false;
            if (SoDienThoai != SelectedTiecCuoi.SoDienThoai)
                return false;
            if (NgayDatTiec != SelectedTiecCuoi.NgayDatTiec)
                return false;
            if (NgayDaiTiec != SelectedTiecCuoi.NgayDaiTiec)
                return false;
            if (TienDatCoc != SelectedTiecCuoi.TienDatCoc)
                return false;
            if (GhiChu != SelectedTiecCuoi.GhiChu)
                return false;
            if (MaSanh != SelectedTiecCuoi.MaSanh)
                return false;
            if (MaCa != SelectedTiecCuoi.MaCa)
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
                SelectedTiecCuoi = new TIECCUOI()
                {
                    TenChuRe = TenChuRe,
                    TenCoDau = TenCoDau,
                    SoDienThoai = SoDienThoai,
                    NgayDatTiec = NgayDatTiec,
                    NgayDaiTiec = NgayDaiTiec,
                    TienDatCoc = TienDatCoc,
                    GhiChu = GhiChu,
                    MaSanh = SelectedSanh.MaSanh,
                    MaCa = SelectedCa.MaCa
                };
                try
                {
                    DataProvider.Ins.DataBase.TIECCUOIs.Add(SelectedTiecCuoi);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListTiecCuoi.Add(SelectedTiecCuoi);
                }
                catch (Exception e)
                {
                    throw e;
                }

            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedTiecCuoi == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == SelectedTiecCuoi.MaTiecCuoi);
                if (displayList != null && displayList.Count() != 0 && !Enable())
                    return true;
                return false;
            }, (p) =>
            {
                var TiecCuoi = DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == SelectedTiecCuoi.MaTiecCuoi).SingleOrDefault();
                TiecCuoi.TenChuRe = TenChuRe;
                TiecCuoi.TenCoDau = TenCoDau;
                TiecCuoi.SoDienThoai = SoDienThoai;
                TiecCuoi.NgayDatTiec = NgayDatTiec;
                TiecCuoi.NgayDaiTiec = NgayDaiTiec;
                TiecCuoi.TienDatCoc = TienDatCoc;
                TiecCuoi.GhiChu = GhiChu;
                TiecCuoi.MaSanh = SelectedSanh.MaSanh;
                TiecCuoi.MaCa = SelectedCa.MaCa;
                DataProvider.Ins.DataBase.SaveChanges();
            });
            DatBanvaDichVuCommand = new RelayCommand<object>((p) => { return Enable(); }, (p) => {
                PhieuDatDichVuViewModel.CurrentMaTiecCuoi = SelectedTiecCuoi.MaTiecCuoi;
                PhieuDatDichVuViewModel.ListPhieuDatDichVu = new ObservableCollection<PHIEUDATDICHVU>(DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaTiecCuoi == SelectedTiecCuoi.MaTiecCuoi));
                PhieuDatBanViewModel.DonGiaBanToiThieu = SelectedSanh.LOAISANH.DonGiaBanToiThieu;
                PhieuDatBanViewModel.CurrentMaTiecCuoi = SelectedTiecCuoi.MaTiecCuoi;
                PhieuDatBanViewModel.ListPhieuDatBan = new ObservableCollection<PHIEUDATBAN>(DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaTiecCuoi == SelectedTiecCuoi.MaTiecCuoi));
                DatBanvaDichVuWindow wd = new DatBanvaDichVuWindow();
                wd.ShowDialog();
            });
            LapHoaDonCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HoaDon wd = new HoaDon(); wd.ShowDialog(); });
        }
    }
}