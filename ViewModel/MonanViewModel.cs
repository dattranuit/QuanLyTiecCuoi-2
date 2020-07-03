using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;
using QuanLyTiecCuoi.Model;
using Microsoft.Win32;

namespace QuanLyTiecCuoi.ViewModel
{
    class MonanViewModel : BaseViewModel
    {
        private ObservableCollection<MONAN> _List;
        public ObservableCollection<MONAN> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private MONAN _SelectedItem;
        public MONAN SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();

                // click vô bảng sẽ hiển thị trên textbox
                if (SelectedItem != null)
                {
                    TenMonAn = SelectedItem.TenMonAn;
                    DonGia = SelectedItem.DonGia;
                    MoTa = SelectedItem.MoTa;
                    GhiChu = SelectedItem.GhiChu;
                    HinhAnh = SelectedItem.HinhAnh;
                }
            }
        }

        //biến bên trang 
        private int _MaMonAn { get; set; }
        public int MaMonAn { get => _MaMonAn; set { _MaMonAn = value; OnPropertyChanged(); } }
        private string _TenMonAn { get; set; }
        public string TenMonAn { get => _TenMonAn; set { _TenMonAn = value; OnPropertyChanged(); } }
        private decimal _DonGia { get; set; }
        public decimal DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        private string _MoTa { get; set; }
        public string MoTa { get => _MoTa; set { _MoTa = value; OnPropertyChanged(); } }
        private string _HinhAnh { get; set; }
        public string HinhAnh { get => _HinhAnh; set { _HinhAnh = value; OnPropertyChanged(); } }
        private string _GhiChu { get; set; }
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand DeleteImageCommand { get; set; }
        public ICommand ClickCommand { get; set; }
        public ICommand RefreshCommand { get; set; }


        public MonanViewModel()
        {
            List = new ObservableCollection<MONAN>(DataProvider.Ins.DataBase.MONANs);
            DataGridCollection = CollectionViewSource.GetDefaultView(List);
            DataGridCollection.Filter = new Predicate<object>(Filter);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrWhiteSpace(TenMonAn) ||
                    string.IsNullOrWhiteSpace(MoTa) ||
                    string.IsNullOrWhiteSpace(GhiChu) ||
                    string.IsNullOrWhiteSpace(DonGia.ToString()))
                    return false;
                return true;

            }, (p) =>
            {
                var MonAn = new MONAN()
                {
                    TenMonAn = TenMonAn,
                    DonGia = DonGia,
                    MoTa = MoTa,
                    HinhAnh = HinhAnh,
                    GhiChu = GhiChu
                };
                DataProvider.Ins.DataBase.MONANs.Add(MonAn);
                DataProvider.Ins.DataBase.SaveChanges();
                List.Add(MonAn);
                // 
                SelectedItem = MonAn;
                //
                MessageBox.Show("Thêm món ăn thành công!");
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                if (SelectedItem.TenMonAn == TenMonAn &&
                SelectedItem.MoTa == MoTa &&
                SelectedItem.GhiChu == GhiChu &&
                SelectedItem.HinhAnh == HinhAnh)
                    return false;
                return true;
            }, (p) =>
            {
                var MonAn = DataProvider.Ins.DataBase.MONANs.Where(x => x.MaMonAn == SelectedItem.MaMonAn).SingleOrDefault();
                
                if (string.IsNullOrWhiteSpace(TenMonAn))
                {
                    MessageBox.Show("Chưa nhập tên món ăn");
                    return;
                }
                
                MonAn.TenMonAn = SelectedItem.TenMonAn;
                MonAn.DonGia = SelectedItem.DonGia;
                MonAn.MoTa = SelectedItem.MoTa;
                MonAn.GhiChu = SelectedItem.GhiChu;
                MonAn.HinhAnh = SelectedItem.HinhAnh;
                DataProvider.Ins.DataBase.SaveChanges();

                SelectedItem.TenMonAn = TenMonAn;
                SelectedItem.DonGia = DonGia;
                SelectedItem.MoTa = MoTa;
                SelectedItem.GhiChu = GhiChu;
                SelectedItem.HinhAnh = HinhAnh;


                MessageBox.Show("Sửa thành công!");
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                var MonAn = DataProvider.Ins.DataBase.MONANs.Where(x => x.MaMonAn == SelectedItem.MaMonAn).First();
                var CT_PhieuDatBan = DataProvider.Ins.DataBase.CT_PHIEUDATBAN.Where(x => x.MaMonAn == SelectedItem.MaMonAn);
                if (CT_PhieuDatBan.Count() != 0)
                {
                    MessageBox.Show("Không thể xóa vì có tồn tại Món ăn này trong Chi tiết đặt bàn!");
                    return;
                }
                DataProvider.Ins.DataBase.MONANs.Remove(MonAn);
                DataProvider.Ins.DataBase.SaveChanges();
                List.Remove(MonAn);

                TenMonAn = "";
                DonGia = 0;
                MoTa = "";
                GhiChu = "";
                HinhAnh = string.Empty;

                MessageBox.Show("Xóa thành công!");
            });
            AddImageCommand = new RelayCommand<Image>((p) =>
            {
                return true;
            }, (p) =>
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(.jpg; *.png)|.jpg; *.png";
                if (open.ShowDialog() == true)
                {
                    HinhAnh = open.FileName;
                };
            });
            DeleteImageCommand = new RelayCommand<Image>((p) =>
            {
                if (string.IsNullOrEmpty(HinhAnh))
                    return false;
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                HinhAnh = string.Empty;
            });
            RefreshCommand = new RelayCommand<Image>((p) =>
            {
                return true;
            }, (p) =>
            {
                TenMonAn = "";
                DonGia = 0;
                MoTa = "";
                GhiChu = "";
                HinhAnh = string.Empty;
            });
        }

        // Search DataGrid
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
            var data = obj as MONAN;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(_filterString))
                {
                    return data.TenMonAn.Contains(_filterString);
                }
                return true;
            }
            return false;
        }

    }
}
