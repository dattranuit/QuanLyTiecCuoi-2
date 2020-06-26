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


        public ICommand DoubleClickCommand { get; set; }
        private ObservableCollection<TIECCUOI> _List2;
        public ObservableCollection<TIECCUOI> List2 { get => _List2; set { _List2 = value; OnPropertyChanged(); } }
        public ObservableCollection<TIECCUOI> _List3;
        public ObservableCollection<TIECCUOI> List3 { get => _List3; set { _List3 = value; OnPropertyChanged(); } }

        public ObservableCollection<PHIEUDATBAN> _List4;
        public ObservableCollection<PHIEUDATBAN> List4 { get => _List4; set { _List4 = value; OnPropertyChanged(); } }

        public int idTiecCuoi = 0;
        public string TongSoBan = "";
        //public decimal TongTienBan = 0;

        private string _TenChuRe;
        public string TenChuRe { get => _TenChuRe; set { _TenChuRe = value; OnPropertyChanged(); } }

        private string _TenCoDau;
        public string TenCoDau { get => _TenCoDau; set { _TenCoDau = value; OnPropertyChanged(); } }

        private System.DateTime _NgayDaiTiec;//= DateTime.Now;
        public System.DateTime NgayDaiTiec { get => _NgayDaiTiec; set { _NgayDaiTiec = value; OnPropertyChanged(); } }


        private int _SoLuong;
        private int _SoLuongDuTru;
        private decimal _DonGiaBan;
    
        public int SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }
        public int SoLuongDuTru { get => _SoLuongDuTru; set { _SoLuongDuTru = value; OnPropertyChanged(); } }
        public decimal DonGiaBan { get => _DonGiaBan; set { _DonGiaBan = value; OnPropertyChanged(); } }

        private string _TienDatCoc;
        public string TienDatCoc { get => _TienDatCoc; set { _TienDatCoc = value; OnPropertyChanged(); } }

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
                    hd.DataContext = List3;
                    hd.DataContext = List2;
                    hd.ShowDialog();
                });
        }

        private ICollectionView _dataGridCollection;
        private string _filterString;
        public ICollectionView DataGridCollection
        {
            get { return _dataGridCollection; }
            set { _dataGridCollection = value; OnPropertyChanged("DataGridCollection"); }
        }
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                OnPropertyChanged("FilterString");
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
        
        private void data()
        {
            List3 = new ObservableCollection<TIECCUOI>(DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == idTiecCuoi));
            DataProvider.Ins.DataBase.SaveChanges();
            if (List3 != null)
            {
                TenChuRe = List3.SingleOrDefault().TenChuRe;
                TenCoDau = List3.SingleOrDefault().TenCoDau;
                NgayDaiTiec = List3.SingleOrDefault().NgayDaiTiec; //Ngay thanh toan trung ngay dai tiec, qua han tinh phat (Neu co)
                TienDatCoc = Convert.ToString(List3.SingleOrDefault().TienDatCoc);
            }
            List4 = new ObservableCollection<PHIEUDATBAN>(DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaTiecCuoi == idTiecCuoi));
            DataProvider.Ins.DataBase.SaveChanges();
            if(List4 != null)
            {
                TongSoBan = Convert.ToString(List4.SingleOrDefault().SoLuong + List4.SingleOrDefault().SoLuongDuTru); // Tong so ban =  So luong ban + So luong du tru
                TongTienBan = List4.SingleOrDefault().DonGiaBan * Convert.ToInt32(TongSoBan);
                DonGiaBan = List4.SingleOrDefault().DonGiaBan;
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
