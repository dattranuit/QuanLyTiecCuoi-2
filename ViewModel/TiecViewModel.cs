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
    class TiecViewModel:BaseViewModel
    {
        private ObservableCollection<TIECCUOI> _List;
        public ObservableCollection<TIECCUOI> List { get => _List; set { _List = value; OnPropertyChanged(); } }

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
                    MaTiecCuoi = SelectedItem.MaTiecCuoi;
                    TenChuRe = SelectedItem.TenChuRe;
                    TenCoDau = SelectedItem.TenCoDau;
                    SoDienThoai = SelectedItem.SoDienThoai;
                    NgayDatTiec = SelectedItem.NgayDatTiec;
                    NgayDaiTiec = SelectedItem.NgayDaiTiec;
                    TienDatCoc = SelectedItem.TienDatCoc;
                    GhiChu = SelectedItem.GhiChu;
                    MaSanh = SelectedItem.MaSanh;
                    MaCa = SelectedItem.MaCa;
                }
            }
        }
        private string _MaTiecCuoi { get; set; }
        public string MaTiecCuoi { get; set; }
        private string _TenChuRe { get; set; }
        public string TenChuRe { get; set; }
        private string _TenCoDau { get; set; }
        public string TenCoDau { get; set; }
        private string _SoDienThoai { get; set; }
        public string SoDienThoai { get; set; }
        private Nullable<System.DateTime> _NgayDatTiec { get; set; }
        public Nullable<System.DateTime> NgayDatTiec { get; set; }
        private Nullable<System.DateTime> _NgayDaiTiec { get; set; }
        public Nullable<System.DateTime> NgayDaiTiec { get; set; }
        private Nullable<decimal> _TienDatCoc { get; set; }
        public Nullable<decimal> TienDatCoc { get; set; }
        private string _GhiChu { get; set; }
        public string GhiChu { get; set; }
        private string _MaSanh { get; set; }
        public string MaSanh { get; set; }
        private string _MaCa { get; set; }
        public string MaCa { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand PhieuDatBanCommand { get; set; }
        public TiecViewModel()
        {
            List = new ObservableCollection<TIECCUOI>(DataProvider.Ins.DataBase.TIECCUOIs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var TiecCuoi = new TIECCUOI()
                {
                    TenChuRe = TenChuRe,
                    TenCoDau = TenCoDau,
                    SoDienThoai = SoDienThoai,
                    NgayDatTiec = NgayDatTiec,
                    NgayDaiTiec = NgayDaiTiec,
                    TienDatCoc = TienDatCoc,
                    GhiChu = GhiChu,
                    MaSanh = MaSanh,
                    MaCa = MaCa
                };
                DataProvider.Ins.DataBase.TIECCUOIs.Add(TiecCuoi);
                DataProvider.Ins.DataBase.SaveChanges();
                List.Add(TiecCuoi);
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
                TiecCuoi.MaTiecCuoi = SelectedItem.MaTiecCuoi;
                TiecCuoi.TenChuRe = SelectedItem.TenChuRe;
                TiecCuoi.TenCoDau = SelectedItem.TenCoDau;
                TiecCuoi.SoDienThoai = SelectedItem.SoDienThoai;
                TiecCuoi.NgayDatTiec = SelectedItem.NgayDatTiec;
                TiecCuoi.NgayDaiTiec = SelectedItem.NgayDaiTiec;
                TiecCuoi.TienDatCoc = SelectedItem.TienDatCoc;
                TiecCuoi.GhiChu = SelectedItem.GhiChu;
                TiecCuoi.MaSanh = SelectedItem.MaSanh;
                TiecCuoi.MaCa = SelectedItem.MaCa;
                DataProvider.Ins.DataBase.SaveChanges();
            });
            PhieuDatBanCommand = new RelayCommand<object>((p) => { return true; }, (p) => { PhieuDatBanWindow wd = new PhieuDatBanWindow(); wd.ShowDialog(); });
        }
        
    }
}
