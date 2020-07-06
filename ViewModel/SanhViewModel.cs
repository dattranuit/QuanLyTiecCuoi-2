using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyTiecCuoi.Model;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace QuanLyTiecCuoi.ViewModel
{
    class SanhViewModel:BaseViewModel
    {
        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }
        private bool _IsReadOnly;
        public bool IsReadOnly { get => _IsReadOnly; set { _IsReadOnly = value; IsEnable = !_IsReadOnly; OnPropertyChanged(); } }
        private ObservableCollection<SANH> _ListSanh;
        private ObservableCollection<LOAISANH> _ListLoaiSanh;
        public ObservableCollection<SANH> ListSanh { get => _ListSanh; set { _ListSanh = value; OnPropertyChanged(); } }
        public ObservableCollection<LOAISANH> ListLoaiSanh { get => _ListLoaiSanh; set { _ListLoaiSanh = value; OnPropertyChanged(); } }

        
        public int MaSanh { get => _MaSanh; set { _MaSanh = value; OnPropertyChanged(); } }
        public string TenSanh { get => _TenSanh; set { _TenSanh = value; OnPropertyChanged(); } }
        public int SoLuongBanToiDa { get => _SoLuongBanToiDa; set { _SoLuongBanToiDa = value; OnPropertyChanged(); } }
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }
        public int MaLoaiSanh { get => _MaLoaiSanh; set { _MaLoaiSanh = value; OnPropertyChanged(); } }
        private int _MaSanh;
        private string _TenSanh;
        private int _SoLuongBanToiDa;
        private string _GhiChu;
        private int _MaLoaiSanh;
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshSanhCommand { get; set; }
        //Loai sanh
        public string TenLoaiSanh { get => _TenLoaiSanh; set { _TenLoaiSanh = value; OnPropertyChanged(); } }
        public int DonGiaBanToiThieu { get => _DonGiaBanToiThieu; set { _DonGiaBanToiThieu = value; OnPropertyChanged(); } }
        public int MaLoaiSanh2 { get => _MaLoaiSanh2; set { _MaLoaiSanh2 = value; OnPropertyChanged(); } }
        private string _TenLoaiSanh;
        private int _DonGiaBanToiThieu;
        private int _MaLoaiSanh2;
        public ICommand AddCommandLoaiSanh { get; set; }
        public ICommand EditLoaiSanhCommand { get; set; }
        public ICommand DeleteLoaiSanhCommand { get; set; }
        public ICommand RefreshLoaiSanhCommand { get; set; }
        private LOAISANH _SelectedLoaiSanh;
        public LOAISANH SelectedLoaiSanh { get => _SelectedLoaiSanh;set { _SelectedLoaiSanh = value ; OnPropertyChanged(); } }

        private SANH _SelectedItem;
        public SANH SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaSanh = SelectedItem.MaSanh;
                    TenSanh = SelectedItem.TenSanh;
                    SoLuongBanToiDa = SelectedItem.SoLuongBanToiDa;
                    GhiChu = SelectedItem.GhiChu;
                    MaLoaiSanh = SelectedItem.MaLoaiSanh;
                }
            }
        }

        private LOAISANH _SelectedItem2;
        public LOAISANH SelectedItem2
        {
            get => _SelectedItem2;
            set
            {
                _SelectedItem2 = value;
                OnPropertyChanged();
                if (SelectedItem2 != null)
                {
                    MaLoaiSanh2 = SelectedItem2.MaLoaiSanh;
                    TenLoaiSanh = SelectedItem2.TenLoaiSanh;
                    DonGiaBanToiThieu = Convert.ToInt32(SelectedItem2.DonGiaBanToiThieu);
                }
            }
        }

        public SanhViewModel()
        {
            IsReadOnly = !LoginViewModel.ThayDoiSanh;
            ListSanh = new ObservableCollection<SANH>(DataProvider.Ins.DataBase.SANHs);
            ListLoaiSanh = new ObservableCollection<LOAISANH>(DataProvider.Ins.DataBase.LOAISANHs);
            //
            DataGridCollection = CollectionViewSource.GetDefaultView(ListSanh);
            DataGridCollection.Filter = new Predicate<object>(Filter);
            //
            DataGridCollection2 = CollectionViewSource.GetDefaultView(ListLoaiSanh);
            DataGridCollection2.Filter = new Predicate<object>(Filter2);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenSanh) || string.IsNullOrEmpty(SoLuongBanToiDa.ToString()) || SelectedLoaiSanh == null || string.IsNullOrEmpty(GhiChu))
                    return false;
                return true;

            }, (p) =>
            {
                var Sanh = new SANH()
                {
                    TenSanh = TenSanh,
                    SoLuongBanToiDa = SoLuongBanToiDa,
                    GhiChu = GhiChu,
                    MaLoaiSanh = SelectedLoaiSanh.MaLoaiSanh,
            };
                try
                {
                    DataProvider.Ins.DataBase.SANHs.Add(Sanh);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListSanh.Add(Sanh);
                    SelectedItem = Sanh;
                    MessageBox.Show("Thêm Sảnh thành công !");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Thêm Sảnh không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }
                
            });

            AddCommandLoaiSanh = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenLoaiSanh) || string.IsNullOrEmpty(DonGiaBanToiThieu.ToString())  )
                    return false;
                return true;

            }, (p) =>
            {
                var LoaiSanh = new LOAISANH()
                {
                    TenLoaiSanh = TenLoaiSanh,
                    DonGiaBanToiThieu = DonGiaBanToiThieu,
                };
                try
                {
                    DataProvider.Ins.DataBase.LOAISANHs.Add(LoaiSanh);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListLoaiSanh.Add(LoaiSanh);
                    SelectedItem2 = LoaiSanh;
                    MessageBox.Show("Thêm loại Sảnh thành công !");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Thêm loại Sảnh không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }

            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.SANHs.Where(x => x.MaSanh == SelectedItem.MaSanh);
                if (displayList == null && displayList.Count() == 0)
                    return false;
                if (SelectedItem.TenSanh == TenSanh
                && SelectedItem.GhiChu == GhiChu
                && SelectedItem.SoLuongBanToiDa == SoLuongBanToiDa
                && SelectedItem.MaLoaiSanh == SelectedLoaiSanh.MaLoaiSanh)
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    var Sanh = DataProvider.Ins.DataBase.SANHs.Where(x => x.MaSanh == SelectedItem.MaSanh).SingleOrDefault();
                    Sanh.TenSanh = SelectedItem.TenSanh;
                    Sanh.SoLuongBanToiDa = SelectedItem.SoLuongBanToiDa;
                    Sanh.GhiChu = SelectedItem.GhiChu;
                    Sanh.MaLoaiSanh = SelectedLoaiSanh.MaLoaiSanh;
                    DataProvider.Ins.DataBase.SaveChanges();
                    SelectedItem.TenSanh = TenSanh;
                    SelectedItem.SoLuongBanToiDa = SoLuongBanToiDa;
                    SelectedItem.GhiChu = GhiChu;
                    SelectedItem.MaLoaiSanh = SelectedLoaiSanh.MaLoaiSanh;
                    MessageBox.Show("Sửa thông tin Sảnh thành công !");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Sửa thông tin Sảnh không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }
                
            });

            EditLoaiSanhCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem2 == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.LOAISANHs.Where(x => x.MaLoaiSanh == SelectedItem2.MaLoaiSanh);
                if (displayList == null && displayList.Count() == 0)
                    return false;
                if (SelectedItem2.TenLoaiSanh == TenLoaiSanh
                && SelectedItem2.DonGiaBanToiThieu == DonGiaBanToiThieu)
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    var LoaiSanh = DataProvider.Ins.DataBase.LOAISANHs.Where(x => x.MaLoaiSanh == SelectedItem2.MaLoaiSanh).SingleOrDefault();
                    LoaiSanh.TenLoaiSanh = SelectedItem2.TenLoaiSanh;
                    LoaiSanh.DonGiaBanToiThieu = SelectedItem2.DonGiaBanToiThieu;
                    DataProvider.Ins.DataBase.SaveChanges();
                    SelectedItem2.TenLoaiSanh = TenLoaiSanh;
                    SelectedItem2.DonGiaBanToiThieu = DonGiaBanToiThieu;
                    MessageBox.Show("Sửa thông tin loại Sảnh thành công !");
                }
                catch(Exception e)
                {
                    MessageBox.Show("Sửa thông tin loại Sảnh không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }
                
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                var Sanh = DataProvider.Ins.DataBase.SANHs.Where(x => x.MaSanh == SelectedItem.MaSanh).First();
                var TiecCuoi = DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaSanh == SelectedItem.MaSanh);
                if (TiecCuoi.Count() != 0)
                {
                    MessageBox.Show("Không thể xóa vì có tồn tại Tiệc Cưới đã đặt Sảnh này!");
                    return;
                }
                try
                {
                    DataProvider.Ins.DataBase.SANHs.Remove(Sanh);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListSanh.Remove(Sanh);
                    MessageBox.Show("Xóa Sảnh thành công !");
                    //refresh nhap
                    TenSanh = "";
                    SoLuongBanToiDa = 0;
                    GhiChu = "";
                }
                catch(Exception e)
                {
                    MessageBox.Show("Xóa Sảnh không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }
                
            });

            DeleteLoaiSanhCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem2 == null)
                    return false;
                return true;
            }, (p) =>
            {
                var LoaiSanh = DataProvider.Ins.DataBase.LOAISANHs.Where(x => x.MaLoaiSanh == SelectedItem2.MaLoaiSanh).First();
                var Sanh = DataProvider.Ins.DataBase.SANHs.Where(x => x.MaLoaiSanh == SelectedItem2.MaLoaiSanh);
                if( Sanh.Count()!=0)
                {
                    MessageBox.Show("Không thể xóa vì có tồn tại Sảnh thuộc loại Sảnh này!");
                    return;
                }
                try
                {
                    DataProvider.Ins.DataBase.LOAISANHs.Remove(LoaiSanh);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListLoaiSanh.Remove(LoaiSanh);
                    MessageBox.Show("Xóa loại Sảnh thành công !");
                    //refresh nhap
                    TenLoaiSanh = "";
                    DonGiaBanToiThieu = 0;
                }
                catch(Exception e)
                {
                    MessageBox.Show("Xóa loại Sảnh không thành công\n" + e.ToString(), "Thông báo", MessageBoxButton.OK);
                }
                
            });

            RefreshSanhCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TenSanh = "";
                GhiChu = "";
                SoLuongBanToiDa = 0;
            });

            RefreshLoaiSanhCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TenLoaiSanh = "";
                DonGiaBanToiThieu = 0;
            });
        }
        //search Sanh
        private ICollectionView _dataGridCollection;
        private string _filterString;
        public ICollectionView DataGridCollection
        {
            get { return _dataGridCollection; }
            set { OnPropertyChanged(); _dataGridCollection = value; OnPropertyChanged("DataGridCollection"); }
        }
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                if(value != _filterString)
                    OnPropertyChanged("FilterString");
                _filterString = value;
                OnPropertyChanged("FilterString");
                FilterCollection();
            }
        }
        private void FilterCollection()
        {
            if (_dataGridCollection != null)
            {
             //   OnPropertyChanged();
                _dataGridCollection.Refresh();
            }
        }
        public bool Filter(object obj)
        {
            var data = obj as SANH;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(_filterString))
                {
                    return data.TenSanh.ToLower().Contains(_filterString.ToLower());
                }
                return true;
            }
            return false;
        }
        //Search Loai Sanh
        private ICollectionView _dataGridCollection2;
        private string _filterStringLoaiSanh;
        public ICollectionView DataGridCollection2
        {
            get { return _dataGridCollection2; }
            set { _dataGridCollection2 = value; OnPropertyChanged("DataGridCollection2"); }
        }
        public string FilterStringLoaiSanh
        {
            get { return _filterStringLoaiSanh; }
            set
            {
                _filterStringLoaiSanh = value;
                OnPropertyChanged("FilterStringLoaiSanh");
                FilterCollection2();
            }
        }
        private void FilterCollection2()
        {
            if (_dataGridCollection2 != null)
            {
                _dataGridCollection2.Refresh();
            }
        }
        public bool Filter2(object obj)
        {
            var data = obj as LOAISANH;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(_filterStringLoaiSanh))
                {
                    return data.TenLoaiSanh.ToLower().Contains(_filterStringLoaiSanh.ToLower());
                }
                return true;
            }
            return false;
        }
    }
}
