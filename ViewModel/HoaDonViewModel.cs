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
        private static ObservableCollection<HOADON> _List;
        public static ObservableCollection<HOADON> List { get => _List; set { _List = value; } }

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
        public DateTime NgayThanhToan { get => _NgayThanhToan; set { _NgayThanhToan = DateTime.Now;  } }

        // command
        public ICommand DoubleClickCommand { get; set; }
        public ICommand LuuHoaDon { get; set; }
        public ICommand InHoaDon { get; set; }
        public ICommand DoubleClickCommandCT_Phieu { get; set; }

        // Get data
        private static ObservableCollection<TIECCUOI> _ListTiecCuoi;
        public static  ObservableCollection<TIECCUOI> ListTiecCuoi { get => _ListTiecCuoi; set { _ListTiecCuoi = value; } }

        public ObservableCollection<TIECCUOI> _ListTiecCuoi2;
        public ObservableCollection<TIECCUOI> ListTiecCuoi2 { get => _ListTiecCuoi2; set { _ListTiecCuoi2 = value; OnPropertyChanged(); } }

        public ObservableCollection<PHIEUDATBAN> _ListPhieuDatBan;
        public ObservableCollection<PHIEUDATBAN> ListPhieuDatBan { get => _ListPhieuDatBan; set { _ListPhieuDatBan = value; OnPropertyChanged(); } }

        public ObservableCollection<PHIEUDATDICHVU> _ListPhieuDatDichVu;
        public ObservableCollection<PHIEUDATDICHVU> ListPhieuDatDichVu { get => _ListPhieuDatDichVu; set { _ListPhieuDatDichVu = value; OnPropertyChanged(); } }

        public ObservableCollection<CT_PHIEUDATBAN> _ListCT_PhieuDatBan;
        public ObservableCollection<CT_PHIEUDATBAN> ListCT_PhieuDatBan { get => _ListCT_PhieuDatBan; set { _ListCT_PhieuDatBan = value; OnPropertyChanged(); } }

        public ObservableCollection<THAMSO> _ListThamSo;
        public ObservableCollection<THAMSO> ListThamSo { get => _ListThamSo; set { _ListThamSo = value; OnPropertyChanged(); } }


        public int idTiecCuoi = 0;

        private decimal _TongSoBan;
        public decimal TongSoBan { get => _TongSoBan; set { _TongSoBan = value; OnPropertyChanged(); } }
        //public decimal TongTienBan = 0;
        public int id = 0;

        private string _TenChuRe;
        public string TenChuRe { get => _TenChuRe; set { _TenChuRe = value;  } }

        private string _TenCoDau;
        public string TenCoDau { get => _TenCoDau; set { _TenCoDau = value; OnPropertyChanged(); } }

        private System.DateTime _NgayDaiTiec;// = DateTime.Now;
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
            
            ListTiecCuoi = new ObservableCollection<TIECCUOI>(DataProvider.Ins.DataBase.TIECCUOIs);
            List = new ObservableCollection<HOADON>(DataProvider.Ins.DataBase.HOADONs);

            DataGridCollection = CollectionViewSource.GetDefaultView(ListTiecCuoi);
            DataGridCollection.Filter = new Predicate<object>(Filter);
            DoubleClickCommand = new RelayCommand<DataGrid>((p) => { return true; },
                (p) => {
                    idTiecCuoi = _getMaTiecCuoi(p);
                    if(DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaTiecCuoi == idTiecCuoi).Count() == 0)
                    {
                        MessageBox.Show("Tiệc cưới chưa hoàn thành, vui lòng kiểm tra lại");
                    }
                    else
                    {
                        //MessageBox.Show(idTiecCuoi.ToString());
                        HoaDon hd = new HoaDon();
                        clear();
                        data();

                        hd.DataContext = List;
                        hd.DataContext = ListTiecCuoi2;
                        hd.DataContext = ListTiecCuoi;
                        hd.DataContext = ListPhieuDatBan;
                        hd.DataContext = ListPhieuDatDichVu;
                        hd.ShowDialog();
                    }
                    
                });
            LuuHoaDon = new RelayCommand<HoaDon>((p) =>
            {
                var hoadon = DataProvider.Ins.DataBase.HOADONs.Where(x => x.MaTiecCuoi == idTiecCuoi);
                if (hoadon == null || hoadon.Count() != 0)
                    return false;
                return true;

            }, (p) =>
            {
                MessageBox.Show("Done");
                var hoadon = new HOADON() { MaTiecCuoi = idTiecCuoi, TongTienBan = TongTienBan,
                    NgayThanhToan = NgayDaiTiec, ConLai = ConLai, TongTienDichVu = TongTienDichVu, TongTienHoaDon = TongTienHoaDon };
                DataProvider.Ins.DataBase.HOADONs.Add(hoadon);
                DataProvider.Ins.DataBase.SaveChanges();
                TinhLaiBaoCaoThang();
                TinhLaiBaoCaoNgay();
            });
            InHoaDon = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) => 
            {
                
            });
            DoubleClickCommandCT_Phieu = new RelayCommand<DataGrid>((p) => { return true; },
                (p) =>
                {
                    id = _getMaPhieuDatBan(p);
                    CT_PhieuDatBanxaml ct_Phieu = new CT_PhieuDatBanxaml();
                    ListCT_PhieuDatBan = new ObservableCollection<CT_PHIEUDATBAN>(DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaPhieuDatBan == id));
                    DataProvider.Ins.DataBase.SaveChanges();
                    if (ListCT_PhieuDatBan == null || ListCT_PhieuDatBan.Count() == 0) { MessageBox.Show("Rỗng"); return; }
                    ct_Phieu.DataContext = ListCT_PhieuDatBan;
                    ct_Phieu.ShowDialog();

                });
        }


        // Search box
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


        private void clear()
        {
            TenChuRe = TenCoDau = String.Empty;
            SoLuong = 0;
            DonGia = 0;
        }
        private void data()
        {
            MessageBox.Show("Gio moi vao");
            if(ListPhieuDatDichVu != null) ListPhieuDatDichVu.Clear();
            
            ListPhieuDatDichVu = new ObservableCollection<PHIEUDATDICHVU>(DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaTiecCuoi == idTiecCuoi));
            DataProvider.Ins.DataBase.SaveChanges();

                if (ListPhieuDatDichVu == null || ListPhieuDatDichVu.Count() == 0) return;

                if (ListPhieuDatDichVu != null)
                {
                    SoLuong = ListPhieuDatDichVu.FirstOrDefault().SoLuong;
                    DonGia = ListPhieuDatDichVu.FirstOrDefault().ThanhTien;

                    //var sum = ListPhieuDatDichVu.FirstOrDefault().SoLuong
                    //MessageBox.Show(ListPhieuDatDichVu[1].DICHVU.TenDichVu.ToString());
                    ////TenDichVu = ListPhieuDatDichVu.SingleOrDefault().MaDichVu.ToString();
                    //SoLuong = ListPhieuDatDichVu.SingleOrDefault().SoLuong;
                    //DonGia = ListPhieuDatDichVu.SingleOrDefault().DonGia;
                    ThanhTien = SoLuong * DonGia;
                    TongTienDichVu = ListPhieuDatDichVu.Sum(x => x.ThanhTien);
                    //MessageBox.Show(tongtiendichvu);
                }

            if (ListTiecCuoi2 != null)  ListTiecCuoi2.Clear();
            ListTiecCuoi2 = new ObservableCollection<TIECCUOI>(DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaTiecCuoi == idTiecCuoi));
            DataProvider.Ins.DataBase.SaveChanges();
            if (ListTiecCuoi2 == null || ListTiecCuoi2.Count() == 0) return;
            if (ListTiecCuoi2 != null)
            {
                TenChuRe = ListTiecCuoi2.SingleOrDefault().TenChuRe;
                TenCoDau = ListTiecCuoi2.SingleOrDefault().TenCoDau;
                //NgayDaiTiec = ListTiecCuoi2.SingleOrDefault().NgayDaiTiec; //Ngay thanh toan trung ngay dai tiec, qua han tinh phat (Neu co)
                //NgayDaiTiec = DateTime.Now;
                MessageBox.Show(NgayDaiTiec.ToString());
                TienDatCoc = Convert.ToString(ListTiecCuoi2.SingleOrDefault().TienDatCoc);
            }
            if (ListPhieuDatBan != null)  ListPhieuDatBan.Clear();
            ListPhieuDatBan = new ObservableCollection<PHIEUDATBAN>(DataProvider.Ins.DataBase.PHIEUDATBANs.Where(x => x.MaTiecCuoi == idTiecCuoi));
            DataProvider.Ins.DataBase.SaveChanges();
            if (ListPhieuDatBan == null || ListPhieuDatBan.Count() == 0) return;
            if (ListPhieuDatBan != null)
            {
                //TongSoBan = Convert.ToString(ListPhieuDatBan.FirstOrDefault().SoLuong + ListPhieuDatBan.FirstOrDefault().SoLuongDuTru); // Tong so ban =  So luong ban + So luong du tru
                //TongTienBan = ListPhieuDatBan.FirstOrDefault().DonGiaBan * Convert.ToInt32(TongSoBan);
                TongTienBan = ListPhieuDatBan.Sum(x => x.DonGiaBan);
                DonGiaBan = ListPhieuDatBan.FirstOrDefault().DonGiaBan;
                //   LoaiBan = ListPhieuDatBan.FirstOrDefault().LoaiBan;
                TongSoBan = ListPhieuDatBan.Sum(x => x.SoLuong) + ListPhieuDatBan.Sum(x => x.SoLuongDuTru);


            }
            //ListCT_PhieuDatBan = new ObservableCollection<CT_PHIEUDATBAN>(DataProvider.Ins.DataBase.CT_PHIEUDATBANs.Where(x => x.PHIEUDATBAN.MaTiecCuoi == idTiecCuoi));
            //DataProvider.Ins.DataBase.SaveChanges();
            //if (ListCT_PhieuDatBan == null || ListCT_PhieuDatBan.Count() == 0) return;
            //if (ListCT_PhieuDatBan != null)
            //{

            //    SoLuong = ListCT_PhieuDatBan.FirstOrDefault().SoLuong;
            //    //MessageBox.Show(SoLuongMon.ToString());
            //    DonGia = ListCT_PhieuDatBan.FirstOrDefault().ThanhTien;
            //}
            if(ListThamSo != null )ListThamSo.Clear();
            ListThamSo = new ObservableCollection<THAMSO>(DataProvider.Ins.DataBase.THAMSOes.ToList());
            DataProvider.Ins.DataBase.SaveChanges();
            var TiLePhat = 0.0d;
            int IsPhat = 0;
            if (ListThamSo == null || ListThamSo.Count() == 0) return;
            if (ListThamSo != null)
            {
                IsPhat = (int)ListThamSo[0].GiaTri;
                //MessageBox.Show("Su dung phat: " + IsPhat);
                TiLePhat = ListThamSo[1].GiaTri;
                //MessageBox.Show(TiLePhat.ToString());
            }
            
            
            TongTienHoaDon = TongTienBan + TongTienDichVu;
            TienPhat = TongTienHoaDon * (decimal)IsPhat * (decimal)TiLePhat;// * (decimal)(DateTime.Now.Subtract(NgayDaiTiec).TotalDays);
            TimeSpan da = DateTime.Now - NgayDaiTiec;
            MessageBox.Show("So ngay: " + da.TotalDays.ToString());
            ConLai = TongTienHoaDon + TienPhat - Convert.ToDecimal(TienDatCoc);
            //MessageBox.Show(TienDatCoc.ToString());
            

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

        public int _getMaPhieuDatBan(DataGrid dataGrid)
        {
            if (dataGrid.SelectedItem != null)
            {
                PHIEUDATBAN MaPhieu = dataGrid.SelectedItem as PHIEUDATBAN;
                return MaPhieu.MaPhieuDatBan;

            }
            else
            {
                MessageBox.Show("Lỗi");
                return -1;
            }
        }

        private void TinhLaiBaoCaoThang()
        {
            var BaoCaoThang = DataProvider.Ins.DataBase.BAOCAOTHANGs.Where(x => x.Thang == NgayDaiTiec.Month && x.Nam == NgayDaiTiec.Year).ToList();
            if(BaoCaoThang.Count() != 0)
            {
                BaoCaoThang[0].TongDoanhThu += TongTienHoaDon;
            }
            else
            {
                DataProvider.Ins.DataBase.BAOCAOTHANGs.Add(new BAOCAOTHANG()
                {
                    Thang = NgayDaiTiec.Month,
                    Nam = NgayDaiTiec.Year,
                    TongDoanhThu = TongTienHoaDon
                }) ;
                DataProvider.Ins.DataBase.SaveChanges();
            }
        }
        private void TinhLaiBaoCaoNgay()
        {
            var BaoCaoThang = DataProvider.Ins.DataBase.BAOCAOTHANGs.Where(x => x.Thang == NgayDaiTiec.Month && x.Nam == NgayDaiTiec.Year).ToList();
            if (BaoCaoThang.Count() != 0)
            {
                int id = BaoCaoThang[0].MaBaoCaoThang;
                var BaoCaoNgay = DataProvider.Ins.DataBase.BAOCAONGAYs.Where(x => x.MaBaoCaoThang == id && x.Ngay == NgayDaiTiec.Day).ToList();
                if(BaoCaoNgay.Count() != 0)
                {
                    BaoCaoNgay[0].SoLuongTiecCuoi += 1;
                    BaoCaoNgay[0].DoanhThu += TongTienHoaDon;
                    BaoCaoNgay[0].TiLe = Convert.ToDouble(BaoCaoNgay[0].DoanhThu) / Convert.ToDouble(BaoCaoThang[0].TongDoanhThu);
                }
                else
                {
                    DataProvider.Ins.DataBase.BAOCAONGAYs.Add(new BAOCAONGAY()
                    {
                        MaBaoCaoThang = id,
                        Ngay = NgayDaiTiec.Day,
                        SoLuongTiecCuoi = 1,
                        DoanhThu = TongTienHoaDon,
                        TiLe = 1
                    });
                    DataProvider.Ins.DataBase.SaveChanges();
                }
            }
            else
                return;
                
        }
        
        
         //public static  CollectionViewSource.GetDefaultView(ListTiecCuoi).Refresh();
        
    }
}
