using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyTiecCuoi.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace QuanLyTiecCuoi.ViewModel
{
    class SanhViewModel:BaseViewModel
    {
        private ObservableCollection<SANH> _ListSanh;
        private ObservableCollection<LOAISANH> _ListLoaiSanh;
        public ObservableCollection<SANH> ListSanh { get => _ListSanh; set { _ListSanh = value; OnPropertyChanged(); } }
        public ObservableCollection<LOAISANH> ListLoaiSanh { get => _ListLoaiSanh; set { _ListLoaiSanh = value; OnPropertyChanged(); } }

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
        public ICommand PhieuDatBanCommand { get; set; }
        public SanhViewModel()
        {
            ListSanh = new ObservableCollection<SANH>(DataProvider.Ins.DataBase.SANHs);
            ListLoaiSanh = new ObservableCollection<LOAISANH>(DataProvider.Ins.DataBase.LOAISANHs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var Sanh = new SANH()
                {
                    MaSanh = MaSanh,
                    TenSanh = TenSanh,
                    SoLuongBanToiDa = SoLuongBanToiDa,
                    GhiChu = GhiChu,
                    MaLoaiSanh = MaLoaiSanh,
            };
                DataProvider.Ins.DataBase.SANHs.Add(Sanh);
                DataProvider.Ins.DataBase.SaveChanges();
                ListSanh.Add(Sanh);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.SANHs.Where(x => x.MaSanh == SelectedItem.MaSanh);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Sanh = DataProvider.Ins.DataBase.SANHs.Where(x => x.MaSanh == SelectedItem.MaSanh).SingleOrDefault();
                Sanh.MaSanh = SelectedItem.MaSanh;
                Sanh.TenSanh = SelectedItem.TenSanh;
                Sanh.SoLuongBanToiDa = SelectedItem.SoLuongBanToiDa;
                Sanh.GhiChu = SelectedItem.GhiChu;
                Sanh.MaLoaiSanh = SelectedItem.MaLoaiSanh;
                DataProvider.Ins.DataBase.SaveChanges();
            });
        }
    }
}
