using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;
using System.Windows;

namespace QuanLyTiecCuoi.ViewModel
{
    class NguoiDungViewModel:BaseViewModel
    {
        private ObservableCollection<NGUOIDUNG> _ListNguoiDung;
        public ObservableCollection<NGUOIDUNG> ListNguoiDung { get => _ListNguoiDung; set { _ListNguoiDung = value; OnPropertyChanged(); } }


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
                if (SelectedNguoiDung != null)
                {
                    Username = SelectedNguoiDung.Username;
                    Password = SelectedNguoiDung.Password;
                    TenNguoiDung = SelectedNguoiDung.TenNguoiDung;
                    MaNhomNguoiDung = SelectedNguoiDung.MaNhomNguoiDung;
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
        public ICommand ClearCommand { get; set; }
        bool Addable()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(TenNguoiDung) || SelectedNhomNguoiDung == null)
                return false;
            return true;
        }
        public NguoiDungViewModel()
        {
            ListNguoiDung = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DataBase.NGUOIDUNGs);
            ListNhomNguoiDung = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DataBase.NHOMNGUOIDUNGs);
            AddCommand = new RelayCommand<object>((p) => Addable(), (p) =>
            {
                var NguoiDung = new NGUOIDUNG()
                {
                    Username = Username,
                    Password = Password,
                    TenNguoiDung = TenNguoiDung,
                    MaNhomNguoiDung = SelectedNhomNguoiDung.MaNhomNguoiDung
                };
                DataProvider.Ins.DataBase.NGUOIDUNGs.Add(NguoiDung);
                DataProvider.Ins.DataBase.SaveChanges();
                ListNguoiDung.Add(NguoiDung);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (Username == SelectedNguoiDung.Username && Password == SelectedNguoiDung.Password &&
                TenNguoiDung == SelectedNguoiDung.TenNguoiDung && MaNhomNguoiDung == SelectedNhomNguoiDung.MaNhomNguoiDung)
                    return false;
                if (!Addable())
                    return false;
                var displayList = DataProvider.Ins.DataBase.NGUOIDUNGs.Where(x => x.Username == SelectedNguoiDung.Username);
                if (displayList == null && displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                if (Username != SelectedNguoiDung.Username)
                {
                    MessageBox.Show("Tên đăng nhập là cố định không được thay đổi", "Thông báo");
                    Username = SelectedNguoiDung.Username;
                }
                else
                {
                    var NguoiDung = DataProvider.Ins.DataBase.NGUOIDUNGs.Where(x => x.Username == SelectedNguoiDung.Username).SingleOrDefault();
                    NguoiDung.TenNguoiDung = TenNguoiDung;
                    NguoiDung.Password = Password;
                    NguoiDung.MaNhomNguoiDung = SelectedNhomNguoiDung.MaNhomNguoiDung;
                    DataProvider.Ins.DataBase.SaveChanges();
                }
            });
            DeleteCommand = new RelayCommand<object>((p) => 
            { 
                if (SelectedNguoiDung == null) 
                    return false; 
                return true; 
            }, 
            (p) =>
            {
                DataProvider.Ins.DataBase.NGUOIDUNGs.Remove(SelectedNguoiDung);
                DataProvider.Ins.DataBase.SaveChanges();
                ListNguoiDung.Remove(SelectedNguoiDung);
                //refresh
                TenNguoiDung = Username = Password = "";
                SelectedNhomNguoiDung = null;
            });
            ClearCommand = new RelayCommand<object>((p) => true,
            (p) =>
            {
                TenNguoiDung = Username = Password = "";
                SelectedNhomNguoiDung = null;
            });
        }
    }
}
