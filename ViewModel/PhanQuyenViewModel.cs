using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;

namespace QuanLyTiecCuoi.ViewModel
{
    class PhanQuyenViewModel : BaseViewModel
    {
        private ObservableCollection<NGUOIDUNG> _List1;
        public ObservableCollection<NGUOIDUNG> List1 { get => _List1; set { _List1 = value; OnPropertyChanged(); } }
       
        private ObservableCollection<CHUCNANG> _List2;
        public ObservableCollection<CHUCNANG> List2 { get => _List2; set { _List2 = value; OnPropertyChanged(); } }

        private CHUCNANG _SelectedItem;
        public CHUCNANG SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();

                // click vô bảng sẽ hiển thị trên textbox
                if (SelectedItem != null)
                {
                    MaChucNang = SelectedItem.MaChucNang;
                    TenChucNang = SelectedItem.TenChucNang;
                }
            }
        }

        //private ICollectionView _dataGridCollection;
        //private string _filterString;
        //public ICollectionView DataGridCollection
        //{
        //    get { return _dataGridCollection; }
        //    set { _dataGridCollection = value; OnPropertyChanged("DataGridCollection"); }
        //}
        //public string FilterString
        //{
        //    get { return _filterString; }
        //    set
        //    {
        //        _filterString = value;
        //        OnPropertyChanged("FilterString");
        //        FilterCollection();
        //    }
        //}
        //private void FilterCollection()
        //{
        //    if (_dataGridCollection != null)
        //    {
        //        _dataGridCollection.Refresh();
        //    }
        //}
        //public bool Filter(object obj)
        //{
        //    var data = obj as MONAN;
        //    if (data != null)
        //    {
        //        if (!string.IsNullOrEmpty(_filterString))
        //        {
        //            return data.TenMonAn.Contains(_filterString);
        //        }
        //        return true;
        //    }
        //    return false;
        //}
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void NotifyPropertyChanged(string property)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}

        ////biến bên trang chính
        private int _MaChucNang { get; set; }
        public int MaChucNang { get => _MaChucNang; set { _MaChucNang = value; OnPropertyChanged(); } }
        private string _TenChucNang { get; set; }
        public string TenChucNang { get => _TenChucNang; set { _TenChucNang = value; OnPropertyChanged(); } }
        private string _TenManHinhDuocLoad { get; set; }
        public string TenManHinhDuocLoad { get => _TenManHinhDuocLoad; set { _TenManHinhDuocLoad = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClickCommand { get; set; }


        public PhanQuyenViewModel()
        {
            List1 = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DataBase.NGUOIDUNGs);
            List2 = new ObservableCollection<CHUCNANG>(DataProvider.Ins.DataBase.CHUCNANGs);
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
                var PhanQuyen = new CHUCNANG()
                {
                    MaChucNang = MaChucNang,
                    TenChucNang = TenChucNang
                };
                DataProvider.Ins.DataBase.CHUCNANGs.Add(PhanQuyen);
                DataProvider.Ins.DataBase.SaveChanges();
                List2.Add(PhanQuyen);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenChucNang) || SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.CHUCNANGs.Where(x => x.TenChucNang == SelectedItem.TenChucNang);
                if (displayList == null && displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                //CheckBox chk = new CheckBox();
                var MonAn = DataProvider.Ins.DataBase.CHUCNANGs.Where(x => x.MaChucNang == SelectedItem.MaChucNang).SingleOrDefault();
                MonAn.MaChucNang = SelectedItem.MaChucNang;
                MonAn.TenChucNang = SelectedItem.TenChucNang;

                DataProvider.Ins.DataBase.SaveChanges();

                //SelectedItem.TenMonAn = TenMonAn;
                //SelectedItem.MaMonAn = MaMonAn;
                //SelectedItem.DonGia = DonGia;
                //SelectedItem.MoTa = MoTa;
                //SelectedItem.GhiChu = GhiChu;
            });
            //DeleteCommand = new RelayCommand<object>((p) =>
            //{



            //});


        }

    }
}
