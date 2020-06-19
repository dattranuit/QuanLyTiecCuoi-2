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

namespace QuanLyTiecCuoi.ViewModel
{
    class TiecViewModel : BaseViewModel
    {
        private ObservableCollection<TIECCUOI> _List;
        public ObservableCollection<TIECCUOI> List { get => _List;  set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<CA> _ListCa;
        public ObservableCollection<CA> ListCa { get => _ListCa; set { _ListCa = value; OnPropertyChanged(); } }
        private ObservableCollection<SANH> _ListSanh;
        public ObservableCollection<SANH> ListSanh { get => _ListSanh; set { _ListSanh = value; OnPropertyChanged(); } }
        private TIECCUOI _Record;
        public TIECCUOI Record
        {
            get => _Record;
            set
            {
                _Record = value;
                OnPropertyChanged();
            }
        }
        private int _MaTiecCuoi;
        public int MaTiecCuoi { get => _MaTiecCuoi; set { _MaTiecCuoi = value; OnPropertyChanged(); } }
        private int _TongSoBan;
        public int TongSoBan{ get => _TongSoBan; set { _TongSoBan = value; OnPropertyChanged(); } }
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
        private int _MaSanh;
        public int MaSanh { get => _MaSanh; set { _MaSanh = value; OnPropertyChanged(); } }
        private int _MaCa;
        public int MaCa { get => _MaCa; set { _MaCa = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand PhieuDatBanCommand { get; set; }
        public ICommand DoubleClickCommand { get; set; }
        public TiecViewModel()
        {
            List = new ObservableCollection<TIECCUOI>(DataProvider.Ins.DataBase.TIECCUOIs);
            ListCa = new ObservableCollection<CA>(DataProvider.Ins.DataBase.CAs);
            ListSanh = new ObservableCollection<SANH>(DataProvider.Ins.DataBase.SANHs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Record = new TIECCUOI()
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
                    //DataProvider.Ins.DataBase.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TIECCUOI] ON");
                    //DataProvider.Ins.DataBase.TIECCUOIs.Attach(Record);
                    DataProvider.Ins.DataBase.TIECCUOIs.Add(Record);
                    //DataProvider.Ins.DataBase.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TIECCUOI] OFF");
                    DataProvider.Ins.DataBase.SaveChanges();
                    List.Add(Record);
                   // string test = DataProvider.Ins.DataBase.TIECCUOIs.ElementAt(1).ToString();
                  //  MessageBox.Show(test);
                }
                catch(Exception e)
                {
                    throw e;
                }

            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (Record == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == Record.MaTiecCuoi);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var TiecCuoi = DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == Record.MaTiecCuoi).SingleOrDefault();
                TiecCuoi.TenChuRe = Record.TenChuRe;
                TiecCuoi.TenCoDau = Record.TenCoDau;
                TiecCuoi.SoDienThoai = Record.SoDienThoai;
                TiecCuoi.NgayDatTiec = Record.NgayDatTiec;
                TiecCuoi.NgayDaiTiec = Record.NgayDaiTiec;
                TiecCuoi.TienDatCoc = Record.TienDatCoc;
                TiecCuoi.GhiChu = Record.GhiChu;
                TiecCuoi.MaSanh = Record.MaSanh;
                TiecCuoi.MaCa = Record.MaCa;
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
