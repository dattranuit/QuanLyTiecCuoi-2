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
        private ObservableCollection<NGUOIDUNG> _ListNguoiDung;
        public ObservableCollection<NGUOIDUNG> ListNguoiDung { get => _ListNguoiDung; set { _ListNguoiDung = value; OnPropertyChanged(); } }
       
        private ObservableCollection<CHUCNANG> _ListChucNang;
        public ObservableCollection<CHUCNANG> ListChucNang { get => _ListChucNang; set { _ListChucNang = value; OnPropertyChanged(); } }

        private ObservableCollection<NHOMNGUOIDUNG> _ListNhomNguoiDung;
        public ObservableCollection<NHOMNGUOIDUNG> ListNhomNguoiDung { get => _ListNhomNguoiDung; set { _ListNhomNguoiDung = value; OnPropertyChanged(); } }


        private NGUOIDUNG _SelectedNguoiDung;
        public NGUOIDUNG SelectedNguoiDung
        {
            get => _SelectedNguoiDung;
            set
            {
                _SelectedNguoiDung = value;
                OnPropertyChanged();

                // click vô bảng sẽ hiển thị trên textbox
                if (SelectedNguoiDung != null)
                {
                    Username = SelectedNguoiDung.Username;
                    Password = SelectedNguoiDung.Password;
                    TenNguoiDung = SelectedNguoiDung.TenNguoiDung;
                }
            }
        }

        private NHOMNGUOIDUNG _SelectedNhomNguoiDung;
        public NHOMNGUOIDUNG SelectedNhomNguoiDung
        {
            get => _SelectedNhomNguoiDung;
            set
            {
                _SelectedNhomNguoiDung = value;
                OnPropertyChanged();
            }
        }

        private NHOMNGUOIDUNG _SelectedNhomNguoiDung2;
        public NHOMNGUOIDUNG SelectedNhomNguoiDung2
        {
            get => _SelectedNhomNguoiDung2;
            set
            {
                _SelectedNhomNguoiDung2 = value;
                OnPropertyChanged();
            }
        }

        private string _Username;
        private string _Password;
        private string _TenNguoiDung;
        private int _MaNhomNguoiDung;
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public string TenNguoiDung { get => _TenNguoiDung; set { _TenNguoiDung = value; OnPropertyChanged(); } }
        public int MaNhomNguoiDung { get => _MaNhomNguoiDung; set { _MaNhomNguoiDung = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClickCommand { get; set; }


        public PhanQuyenViewModel()
        {
            ListNguoiDung = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DataBase.NGUOIDUNGs);
            ListChucNang = new ObservableCollection<CHUCNANG>(DataProvider.Ins.DataBase.CHUCNANGs);
            ListNhomNguoiDung = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DataBase.NHOMNGUOIDUNGs);
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
                //var PhanQuyen = new CHUCNANG()
                //{
                //    MaChucNang = MaChucNang,
                //    TenChucNang = TenChucNang
                //};
                //DataProvider.Ins.DataBase.CHUCNANGs.Add(PhanQuyen);
                //DataProvider.Ins.DataBase.SaveChanges();
            });

            //EditCommand = new RelayCommand<object>((p) =>
            //{
            //    if (string.IsNullOrEmpty(TenChucNang) || SelectedChucNang == null)
            //        return false;
            //    var displayList = DataProvider.Ins.DataBase.CHUCNANGs.Where(x => x.TenChucNang == SelectedChucNang.TenChucNang);
            //    if (displayList == null && displayList.Count() != 0)
            //        return false;
            //    return true;
            //}, (p) =>
            //{
            //    //CheckBox chk = new CheckBox();
            //    var MonAn = DataProvider.Ins.DataBase.CHUCNANGs.Where(x => x.MaChucNang == SelectedChucNang.MaChucNang).SingleOrDefault();
            //    MonAn.MaChucNang = SelectedChucNang.MaChucNang;
            //    MonAn.TenChucNang = SelectedChucNang.TenChucNang;

            //    DataProvider.Ins.DataBase.SaveChanges();

            //    //SelectedChucNang.TenMonAn = TenMonAn;
            //    //SelectedChucNang.MaMonAn = MaMonAn;
            //    //SelectedChucNang.DonGia = DonGia;
            //    //SelectedChucNang.MoTa = MoTa;
            //    //SelectedChucNang.GhiChu = GhiChu;
            //});
            //DeleteCommand = new RelayCommand<object>((p) =>
            //{



            //});


        }

    }
}
