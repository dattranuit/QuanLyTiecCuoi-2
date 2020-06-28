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
using QuanLyTiecCuoi.Model;

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
                    MaMonAn = SelectedItem.MaMonAn;
                    TenMonAn = SelectedItem.TenMonAn;
                    DonGia = SelectedItem.DonGia;
                    MoTa = SelectedItem.MoTa;
                    GhiChu = SelectedItem.GhiChu;
                }
            }
        }

<<<<<<< HEAD
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void NotifyPropertyChanged(string property)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}
=======
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
>>>>>>> c33ecad1244dfa3420b1f0a4f4e6606838f57024
        //biến bên trang chính
        private int _MaMonAn { get; set; }
        public int MaMonAn { get => _MaMonAn; set { _MaMonAn = value; OnPropertyChanged(); } }
        private string _TenMonAn { get; set; }
        public string TenMonAn { get => _TenMonAn; set { _TenMonAn = value; OnPropertyChanged(); } }
        private decimal _DonGia { get; set; }
        public decimal DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        private string _MoTa { get; set; }
        public string MoTa { get => _MoTa; set { _MoTa = value; OnPropertyChanged(); } }
        public string HinhAnh { get; set; }
        private string _GhiChu { get; set; }
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }
        private int _MaLoaiMonAn { get; set; }
        public int MaLoaiMonAn { get => _MaLoaiMonAn; set { _MaLoaiMonAn = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClickCommand { get; set; }


        public MonanViewModel()
        {
            List = new ObservableCollection<MONAN>(DataProvider.Ins.DataBase.MONANs);
            DataGridCollection = CollectionViewSource.GetDefaultView(List);
            DataGridCollection.Filter = new Predicate<object>(Filter);
            AddCommand = new RelayCommand<object>((p) =>
            {
                //if (string.IsNullOrEmpty(TenMonAn))
                //    return false;
                //var displayList = DataProvider.Ins.DataBase.MONANs.Where(x => x.TenMonAn == TenMonAn);
                //if (displayList == null && displayList.Count() != 0)
                //    return false;
                return true;

            }, (p) =>
            {
                var MonAn = new MONAN()
                {
                    TenMonAn = TenMonAn,
                    DonGia = DonGia,
                    MoTa = MoTa,
                    GhiChu = GhiChu
                };
                DataProvider.Ins.DataBase.MONANs.Add(MonAn);
                DataProvider.Ins.DataBase.SaveChanges();
                List.Add(MonAn);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenMonAn) || SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.MONANs.Where(x => x.TenMonAn == TenMonAn);
                if (displayList == null && displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var MonAn = DataProvider.Ins.DataBase.MONANs.Where(x => x.MaMonAn == SelectedItem.MaMonAn).SingleOrDefault();
                //MonAn.MaMonAn = SelectedItem.MaMonAn;
                //MonAn.TenMonAn = SelectedItem.TenMonAn;
                //MonAn.DonGia = SelectedItem.DonGia;
                //MonAn.MoTa = SelectedItem.MoTa;
                //MonAn.GhiChu = SelectedItem.GhiChu;
                DataProvider.Ins.DataBase.SaveChanges();

                SelectedItem.TenMonAn = TenMonAn;
                SelectedItem.MaMonAn = MaMonAn;
                SelectedItem.DonGia = DonGia;
                SelectedItem.MoTa = MoTa;
                SelectedItem.GhiChu = GhiChu;
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenMonAn) || SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.MONANs.Where(x => x.TenMonAn == SelectedItem.TenMonAn);
                if (displayList == null && displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var MonAn = DataProvider.Ins.DataBase.MONANs.Where(x => x.MaMonAn == SelectedItem.MaMonAn).SingleOrDefault();
                DataProvider.Ins.DataBase.MONANs.Remove(MonAn);
                DataProvider.Ins.DataBase.SaveChanges();
                MessageBox.Show("Xóa thành công!");
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
