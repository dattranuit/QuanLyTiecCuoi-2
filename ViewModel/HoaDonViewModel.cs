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
        public ICommand LuuHoaDon { get; set; }
        private ObservableCollection<TIECCUOI> _List2;
        public ObservableCollection<TIECCUOI> List2 { get => _List2; set { _List2 = value; OnPropertyChanged(); } }
        public ObservableCollection<TIECCUOI> _List3;
        public ObservableCollection<TIECCUOI> List3 { get => _List3; set { _List3 = value; OnPropertyChanged(); } }

        public ObservableCollection<PHIEUDATBAN> _List4;
        public ObservableCollection<PHIEUDATBAN> List4 { get => _List4; set { _List4 = value; OnPropertyChanged(); } }

        public ObservableCollection<PHIEUDATDICHVU> _List5;
        public ObservableCollection<PHIEUDATDICHVU> List5 { get => _List5; set { _List5 = value; OnPropertyChanged(); } }

        public ObservableCollection<CT_PHIEUDATBAN> _List6;
        public ObservableCollection<CT_PHIEUDATBAN> List6 { get => _List6; set { _List6 = value; OnPropertyChanged(); } }

        public ObservableCollection<THAMSO> _List7;
        public ObservableCollection<THAMSO> List7 { get => _List7; set { _List7 = value; OnPropertyChanged(); } }

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


        private int _MaDichVu { get; set; }
        public int MaDichVu { get; set; }
        private string _TenDichVu { get; set; }
        public string TenDichVu { get; set; }
        private decimal _DonGia { get; set; }
        public decimal DonGia { get; set; }

        private int _MaTiecCuoi;
        public int MaTiecCuoi { get => _MaTiecCuoi; set { _MaTiecCuoi = value; OnPropertyChanged(); } }
        private decimal _ThanhTien;
        public decimal ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }

        private string _TenMonAn { get; set; }
        public string TenMonAn { get; set; }
        private string _DonGiaMonAn { get; set; }
        public string DonGiaMonAn { get; set; }

        private string _SoLuongMon;
        public string SoLuongMon { get => _SoLuongMon; set { _SoLuongMon = value; OnPropertyChanged(); } }
       

        //public static string tongtiendichvu = "";
        //public decimal TienConLai = 0;
        //public static decimal TienPhat = 1000;

        private decimal _TienPhat;
        public decimal TienPhat { get => _TienPhat; set { _TienPhat = value; OnPropertyChanged(); } }

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
                    
                    hd.DataContext = List;
                    hd.DataContext = List3;
                    hd.DataContext = List2;
                    hd.DataContext = List4;
                    hd.DataContext = List5;
                    hd.DataContext = List6;
                    //var join = (from u in DataProvider.Ins.DataBase.PHIEUDATDICHVUs
                    //            join b in DataProvider.Ins.DataBase.DICHVUs
                    //            on u.MaDichVu equals b.MaDichVu
                    //            select u.MaTiecCuoi + u.MaDichVu + b.TenDichVu + u.SoLuong + u.DonGia
                    //    ).ToList();
                    //hd.DataContext = join;
                    
                    hd.ShowDialog();
                });
            LuuHoaDon = new RelayCommand<HoaDon>((p) =>
            {
                return true;

            }, (p) =>
            {
                MessageBox.Show("Done");
                
                
            });

        }

        private void join()
        {
            
            
        }

        private ICollectionView _dataGridCollection;
        private string _filterString;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
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
        
        private void data()
        {
            List5 = new ObservableCollection<PHIEUDATDICHVU>(DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaTiecCuoi == idTiecCuoi));

            //List5 = new ObservableCollection<object>(DataProvider.Ins.DataBase.PHIEUDATBANs.Join());
            DataProvider.Ins.DataBase.SaveChanges();
            if (List5 != null)
            {
                SoLuong = List5.FirstOrDefault().SoLuong;
                DonGia = List5.FirstOrDefault().ThanhTien;
                //var sum = List5.FirstOrDefault().SoLuong
                //MessageBox.Show(List5[1].DICHVU.TenDichVu.ToString());
                ////TenDichVu = List5.SingleOrDefault().MaDichVu.ToString();
                //SoLuong = List5.SingleOrDefault().SoLuong;
                //DonGia = List5.SingleOrDefault().DonGia;
                ThanhTien = SoLuong * DonGia;
                TongTienDichVu = List5.Sum(x => x.ThanhTien);
                //MessageBox.Show(tongtiendichvu);
            }
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
            List6 = new ObservableCollection<CT_PHIEUDATBAN>(DataProvider.Ins.DataBase.CT_PHIEUDATBANs.Where(x => x.PHIEUDATBAN.MaTiecCuoi == idTiecCuoi));
            DataProvider.Ins.DataBase.SaveChanges();
            if (List6 != null)
            {
                
                SoLuong = List6.FirstOrDefault().SoLuong;
                //MessageBox.Show(SoLuongMon.ToString());
                DonGia = List6.FirstOrDefault().ThanhTien;
            }

            List7 = new ObservableCollection<THAMSO>(DataProvider.Ins.DataBase.THAMSOs.ToList());
            DataProvider.Ins.DataBase.SaveChanges();
            var TiLePhat = 0.0d;
            int IsPhat = 0;
            if(List7 != null)
            {
                IsPhat = (int)List7[0].GiaTri;
                MessageBox.Show("Su dung phat: " + IsPhat);
                TiLePhat = List7[1].GiaTri;
                //MessageBox.Show(TiLePhat.ToString());
            }
            
            //MessageBox.Show(TienPhat.ToString());
            TongTienHoaDon = TongTienBan + TongTienDichVu;
            TienPhat = TongTienHoaDon * (decimal)IsPhat * (decimal)TiLePhat; // Con tinh ngay
            ConLai = TongTienHoaDon + TienPhat - Convert.ToDecimal(TienDatCoc);
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
