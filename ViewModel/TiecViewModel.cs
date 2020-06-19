using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;

namespace QuanLyTiecCuoi.ViewModel
{
    class TiecViewModel : BaseViewModel
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
        private int _MaTiecCuoi { get; set; }
        public int MaTiecCuoi { get; set; }
        private string _TenChuRe { get; set; }
        public string TenChuRe { get; set; }
        private string _TenCoDau { get; set; }
        public string TenCoDau { get; set; }
        private string _SoDienThoai { get; set; }
        public string SoDienThoai { get; set; }
        private System.DateTime _NgayDatTiec { get; set; }
        public System.DateTime NgayDatTiec { get; set; }
        private System.DateTime _NgayDaiTiec { get; set; }
        public System.DateTime NgayDaiTiec { get; set; }
        private decimal _TienDatCoc { get; set; }
        public decimal TienDatCoc { get; set; }
        private string _GhiChu { get; set; }
        public string GhiChu { get; set; }
        private int _MaSanh { get; set; }
        public int MaSanh { get; set; }
        private int _MaCa { get; set; }
        public int MaCa { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand PhieuDatBanCommand { get; set; }
        public ICommand DoubleClickCommand { get; set; }
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
            DoubleClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HoaDon hd = new HoaDon(); hd.ShowDialog(); });

            DataGridCollection = CollectionViewSource.GetDefaultView(List);
            DataGridCollection.Filter = new Predicate<object>(Filter);

        }
        private ICollectionView _dataGridCollection;
        private string _filterString;
        public ICollectionView DataGridCollection
        {
            get { return _dataGridCollection; }
            set { _dataGridCollection = value; NotifyPropertyChanged("DataGridCollection"); }
        }
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                NotifyPropertyChanged("FilterString");
                FilterCollection();
            }
        }
        private void FilterCollection()
        {
            if (_dataGridCollection != null)
            {
                _dataGridCollection.Refresh();
            }
        }
        public bool Filter(object obj)
        {
            var data = obj as TIECCUOI;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(_filterString))
                {
                    return data.SoDienThoai.Contains(_filterString) || data.TenChuRe.Contains(_filterString) || data.TenCoDau.Contains(_filterString);
                }
                return true;
            }
            return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        
    }
}
