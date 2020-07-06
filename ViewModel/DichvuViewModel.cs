using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using QuanLyTiecCuoi.Model;

namespace QuanLyTiecCuoi.ViewModel
{
    class DichVuViewModel : BaseViewModel
    {
        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }
        private bool _IsReadOnly;
        public bool IsReadOnly { get => _IsReadOnly; set { _IsReadOnly = value; IsEnable = !_IsReadOnly; OnPropertyChanged(); } }
        private ObservableCollection<DICHVU> _List;
        public ObservableCollection<DICHVU> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private DICHVU _SelectedItem;
        public DICHVU SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaDichVu = SelectedItem.MaDichVu;
                    TenDichVu = SelectedItem.TenDichVu;
                    DonGia = SelectedItem.DonGia;
                    MoTa = SelectedItem.MoTa;
                    GhiChu = SelectedItem.GhiChu;
                    HinhAnh = SelectedItem.HinhAnh;
                }
            }
        }
        private int _MaDichVu;
        public int MaDichVu { get => _MaDichVu; set { _MaDichVu = value; OnPropertyChanged(); }  }
        private string _TenDichVu;
        public string TenDichVu { get => _TenDichVu; set { _TenDichVu = value; OnPropertyChanged(); } }
        private decimal _DonGia;
        public decimal DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        private string _MoTa;
        public string MoTa { get => _MoTa; set { _MoTa = value; OnPropertyChanged(); } }
        private string _GhiChu;
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }
        private string _HinhAnh;
        public string HinhAnh { get => _HinhAnh; set { _HinhAnh = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ChonAnhCommmand { get; set; }
        public ICommand XoaAnhCommmand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public DichVuViewModel()
        {
            IsReadOnly = !LoginViewModel.ThayDoiDichVu;
            List = new ObservableCollection<DICHVU>(DataProvider.Ins.DataBase.DICHVUs);
            //
            DataGridCollection = CollectionViewSource.GetDefaultView(List);
            DataGridCollection.Filter = new Predicate<object>(Filter);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if(string.IsNullOrEmpty(TenDichVu) || string.IsNullOrEmpty(MoTa) || string.IsNullOrEmpty(GhiChu))
                return false;
                return true;

            }, (p) =>
            {
                var DichVu = new DICHVU()
                {
                    TenDichVu = TenDichVu,
                    DonGia = DonGia,
                    MoTa = MoTa,
                    GhiChu = GhiChu,
                    HinhAnh = HinhAnh
                };
                DataProvider.Ins.DataBase.DICHVUs.Add(DichVu);
                DataProvider.Ins.DataBase.SaveChanges();
                List.Add(DichVu);
                SelectedItem = DichVu;
                MessageBox.Show("Thêm Dịch vụ thành công !");
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.DICHVUs.Where(x => x.MaDichVu == SelectedItem.MaDichVu);
                if (displayList == null && displayList.Count() == 0)
                    return false;
                if (SelectedItem.TenDichVu == TenDichVu &&
                SelectedItem.MoTa == MoTa &&
                SelectedItem.GhiChu == GhiChu &&
                SelectedItem.DonGia == DonGia &&
                SelectedItem.HinhAnh == HinhAnh)
                    return false;
                return true;
            }, (p) =>
            {
                var DichVu = DataProvider.Ins.DataBase.DICHVUs.Where(x => x.MaDichVu == SelectedItem.MaDichVu).SingleOrDefault();
                DichVu.TenDichVu = SelectedItem.TenDichVu;
                DichVu.DonGia = SelectedItem.DonGia;
                DichVu.MoTa = SelectedItem.MoTa;
                DichVu.GhiChu = SelectedItem.GhiChu;
                DichVu.HinhAnh = SelectedItem.HinhAnh;
                DataProvider.Ins.DataBase.SaveChanges();
                MessageBox.Show("Sửa thông tin Dịch vụ thành công !");
                SelectedItem.TenDichVu = TenDichVu;
                SelectedItem.DonGia = DonGia;
                SelectedItem.MoTa = MoTa;
                SelectedItem.GhiChu = GhiChu;
                SelectedItem.HinhAnh = HinhAnh;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                var DichVu = DataProvider.Ins.DataBase.DICHVUs.Where(x => x.MaDichVu == SelectedItem.MaDichVu).First();
                var PhieuDatDichVu = DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaDichVu == SelectedItem.MaDichVu);
                if(PhieuDatDichVu.Count() != 0)
                {
                    MessageBox.Show("Không thể xóa vì có tồn tại Tiệc Cưới đặt Dịch Vụ này !");
                    return;
                }
                DataProvider.Ins.DataBase.DICHVUs.Remove(DichVu);
                DataProvider.Ins.DataBase.SaveChanges();
                List.Remove(DichVu);
                MessageBox.Show("Xóa Dịch vụ thành công !");
                //refresh nhap
                TenDichVu = "";
                MoTa = "";
                GhiChu = "";
                HinhAnh = "";
                DonGia = 0;
            });

            ChonAnhCommmand = new RelayCommand<Image>((p) => { return true; }, (p) =>
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(.jpg; *.png)|.jpg; *.png";
                if (open.ShowDialog() == true)
                {
                    HinhAnh = open.FileName;
                };
            });

            XoaAnhCommmand = new RelayCommand<Image>((p) => { if (string.IsNullOrWhiteSpace(HinhAnh)) return false; if (SelectedItem == null) return false; return true; }, (p) =>
            {
                HinhAnh = string.Empty;
            });

            RefreshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TenDichVu = "";
                MoTa = "";
                DonGia = 0;
                GhiChu = "";
                HinhAnh = string.Empty;
            });
        }
        //search
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
            var data = obj as DICHVU;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(_filterString))
                {
                    return data.TenDichVu.ToLower().Contains(_filterString.ToLower());
                }
                return true;
            }
            return false;
        }

    }
}

