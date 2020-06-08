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
    class DichvuViewModel : BaseViewModel
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
                }
            }
        }
        private string _MaDichVu { get; set; }
        public string MaDichVu { get; set; }
        private string _TenDichVu { get; set; }
        public string TenDichVu { get; set; }
        private decimal? _DonGia { get; set; }
        public decimal? DonGia { get; set; }
        private string _MoTa { get; set; }
        public string MoTa { get; set; }
        private string _GhiChu { get; set; }
        public string GhiChu { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public DichvuViewModel()
        {
            List = new ObservableCollection<DICHVU>(DataProvider.Ins.DataBase.DICHVUs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var DichVu = new DICHVU()
                {
                    MaDichVu = MaDichVu,
                    TenDichVu = TenDichVu,
                    DonGia = DonGia,
                    MoTa = MoTa,
                    GhiChu = GhiChu
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
                DataProvider.Ins.DataBase.SaveChanges();
            });
        }

    }
}
