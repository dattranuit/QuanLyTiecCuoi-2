using System;
using QuanLyTiecCuoi.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;

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

        private decimal _TongTienBan;
        public decimal TongTienBan { get => _TongTienBan; set { _TongTienBan = value; OnPropertyChanged(); } }


        private decimal _TongTienDichVu;
        public decimal TongTienDichVu { get => _TongTienDichVu; set { _TongTienDichVu = value; OnPropertyChanged(); } }


        private decimal _TongTienHoaDon;
        public decimal TongTienHoaDon { get => _TongTienHoaDon; set { _TongTienHoaDon = value; OnPropertyChanged(); } }


        private decimal _ConLai;
        public decimal ConLai { get => _ConLai; set { _ConLai = value; OnPropertyChanged(); } }


        private DateTime _NgayThanhToan;
        public DateTime NgayThanhToan { get => _NgayThanhToan; set { _NgayThanhToan = value; OnPropertyChanged(); } }


        //public ICommand AddCommand { get; set; }
        //public ICommand EditCommand { get; set; }
        public ICommand DoubleClickCommand { get; set; }
        private ObservableCollection<TIECCUOI> _List2;
        public ObservableCollection<TIECCUOI> List2 { get => _List2; set { _List2 = value; OnPropertyChanged(); } }
        public ObservableCollection<TIECCUOI> _List3;
        public ObservableCollection<TIECCUOI> List3 { get => _List3; set { _List3 = value; OnPropertyChanged(); } }
        public int idTiecCuoi = 0;
        public string tenchure = "AAA";
        private string _TenChuRe;
        public string TenChuRe { get => _TenChuRe; set { _TenChuRe = value; OnPropertyChanged(); } }
        public HoaDonViewModel()
        {
            List2 = new ObservableCollection<TIECCUOI>(DataProvider.Ins.DataBase.TIECCUOIs);
            List = new ObservableCollection<HOADON>(DataProvider.Ins.DataBase.HOADONs);

            DataGridCollection = CollectionViewSource.GetDefaultView(List);
            DataGridCollection.Filter = new Predicate<object>(Filter);
            DoubleClickCommand = new RelayCommand<DataGrid>((p) => { return true; },
                (p) => {
                    idTiecCuoi = _getMaTiecCuoi(p);
                    HoaDon hd = new HoaDon();
                    data();
                    hd.DataContext = List3; hd.ShowDialog();
                });
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
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void NotifyPropertyChanged(string property)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}
        
        private void data()
        {
            List3 = new ObservableCollection<TIECCUOI>(DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == idTiecCuoi));
            DataProvider.Ins.DataBase.SaveChanges();
            if (List3 != null)
            {
                TenChuRe = List3.SingleOrDefault().TenChuRe;
            }
        }
        public int _getMaTiecCuoi(DataGrid dataGrid)
        {
            if (dataGrid.SelectedItem != null)
            {
                TIECCUOI IdTiecCuoi = dataGrid.SelectedItem as TIECCUOI;
                return IdTiecCuoi.MaTiecCuoi;

            }
            else
            {
                MessageBox.Show("Lỗi");
                return -1;
            }
        }
    }
}
