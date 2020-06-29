using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;

namespace QuanLyTiecCuoi.ViewModel
{
    class DichVuViewModel : BaseViewModel
    {
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
        public DichVuViewModel()
        {
            List = new ObservableCollection<DICHVU>(DataProvider.Ins.DataBase.DICHVUs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if(string.IsNullOrEmpty(TenDichVu) || string.IsNullOrEmpty(DonGia.ToString()))
                return false;
                return true;

            }, (p) =>
            {
                var DichVu = new DICHVU()
                {
                    MaDichVu = MaDichVu,
                    TenDichVu = TenDichVu,
                    DonGia = DonGia,
                    MoTa = MoTa,
                    GhiChu = GhiChu,
                    HinhAnh = HinhAnh
                };
                DataProvider.Ins.DataBase.DICHVUs.Add(DichVu);
                DataProvider.Ins.DataBase.SaveChanges();
                List.Add(DichVu);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.DICHVUs.Where(x => x.MaDichVu == SelectedItem.MaDichVu);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var DichVu = DataProvider.Ins.DataBase.DICHVUs.Where(x => x.MaDichVu == SelectedItem.MaDichVu).SingleOrDefault();
                DichVu.MaDichVu = SelectedItem.MaDichVu;
                DichVu.TenDichVu = SelectedItem.TenDichVu;
                DichVu.DonGia = SelectedItem.DonGia;
                DichVu.MoTa = SelectedItem.MoTa;
                DichVu.GhiChu = SelectedItem.GhiChu;
                DichVu.HinhAnh = SelectedItem.HinhAnh;
                DataProvider.Ins.DataBase.SaveChanges();
            });
        }

    }
}
