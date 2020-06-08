using QuanLyTiecCuoi.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyTiecCuoi.ViewModel
{
    class BaoCaoThangViewModel : BaseViewModel
    {
        private ObservableCollection<BAOCAOTHANG> _List;
        public ObservableCollection<BAOCAOTHANG> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private BAOCAOTHANG _SelectedItem;

        public BAOCAOTHANG SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Thang = SelectedItem.Thang;
                    Nam = SelectedItem.Nam;
                    TongDoanhThu = SelectedItem.TongDoanhThu;
                }
            }
        }

        private int? _Thang;
        public int? Thang { get => _Thang; set { _Thang = value; OnPropertyChanged(); } }


        private int? _Nam;
        public int? Nam { get => _Nam; set { _Nam = value; OnPropertyChanged(); } }


        private decimal? _TongDoanhThu;
        public decimal? TongDoanhThu { get => _TongDoanhThu; set { _TongDoanhThu = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public BaoCaoThangViewModel()
        {
            List = new ObservableCollection<BAOCAOTHANG>(DataProvider.Ins.DataBase.BAOCAOTHANGs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var BaoCaoThang = new BAOCAOTHANG() { Thang = Thang, Nam = Nam, TongDoanhThu = TongDoanhThu };

                DataProvider.Ins.DataBase.BAOCAOTHANGs.Add(BaoCaoThang);
                DataProvider.Ins.DataBase.SaveChanges();

                List.Add(BaoCaoThang);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DataBase.BAOCAOTHANGs.Where(x => x.MaBaoCaoThang == SelectedItem.MaBaoCaoThang);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var BaoCaoNgay = DataProvider.Ins.DataBase.BAOCAOTHANGs.Where(x => x.MaBaoCaoThang == SelectedItem.MaBaoCaoThang).SingleOrDefault();
                BaoCaoNgay.Thang = SelectedItem.Thang;
                BaoCaoNgay.Nam = SelectedItem.Nam;
                BaoCaoNgay.TongDoanhThu = SelectedItem.TongDoanhThu;
                DataProvider.Ins.DataBase.SaveChanges();
                SelectedItem.Thang = Thang;
            });
        }
    }
}
