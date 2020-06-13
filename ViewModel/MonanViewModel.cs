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
        private int _MaMonAn { get; set; }
        public int MaMonAn { get; set; }
        private string _TenMonAn { get; set; }
        public string TenMonAn { get; set; }
        private decimal _DonGia { get; set; }
        public decimal DonGia { get; set; }
        private string _MoTa { get; set; }
        public string MoTa { get; set; }
        private string _GhiChu { get; set; }
        public string GhiChu { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public MonanViewModel()
        {
            List = new ObservableCollection<MONAN>(DataProvider.Ins.DataBase.MONANs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var MonAn = new MONAN()
                {
                    MaMonAn = MaMonAn,
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
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.MONANs.Where(x => x.MaMonAn == SelectedItem.MaMonAn);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var MonAn = DataProvider.Ins.DataBase.MONANs.Where(x => x.MaMonAn == SelectedItem.MaMonAn).SingleOrDefault();
                MonAn.MaMonAn = SelectedItem.MaMonAn;
                MonAn.TenMonAn = SelectedItem.TenMonAn;
                MonAn.DonGia = SelectedItem.DonGia;
                MonAn.MoTa = SelectedItem.MoTa;
                MonAn.GhiChu = SelectedItem.GhiChu;
                DataProvider.Ins.DataBase.SaveChanges();
            });
        }

    }
}
