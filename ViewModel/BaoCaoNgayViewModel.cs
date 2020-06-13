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
    class BaoCaoNgayViewModel : BaseViewModel
    {
        private ObservableCollection<BAOCAONGAY> _List;
        public ObservableCollection<BAOCAONGAY> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private BAOCAONGAY _SelectedItem;

        public BAOCAONGAY SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Ngay = SelectedItem.Ngay;
                    SoLuongTiecCuoi = SelectedItem.SoLuongTiecCuoi;
                    DoanhThu = SelectedItem.DoanhThu;
                    TiLe = SelectedItem.TiLe;
                }
            }
        }

        private int _Ngay;
        public int Ngay { get => _Ngay; set { _Ngay = value; OnPropertyChanged(); } }


        private int _SoLuongTiecCuoi;
        public int SoLuongTiecCuoi { get => _SoLuongTiecCuoi; set { _SoLuongTiecCuoi = value; OnPropertyChanged(); } }


        private decimal _DoanhThu;
        public decimal DoanhThu { get => _DoanhThu; set { _DoanhThu = value; OnPropertyChanged(); } }


        private double _TiLe;
        public double TiLe { get => _TiLe; set { _TiLe = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public BaoCaoNgayViewModel()
        {
            List = new ObservableCollection<BAOCAONGAY>(DataProvider.Ins.DataBase.BAOCAONGAYs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var BaoCaoNgay = new BAOCAONGAY() { Ngay = Ngay, SoLuongTiecCuoi = SoLuongTiecCuoi, DoanhThu = DoanhThu, TiLe = TiLe };

                DataProvider.Ins.DataBase.BAOCAONGAYs.Add(BaoCaoNgay);
                DataProvider.Ins.DataBase.SaveChanges();

                List.Add(BaoCaoNgay);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DataBase.BAOCAONGAYs.Where(x => x.Ngay == SelectedItem.Ngay);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var BaoCaoNgay = DataProvider.Ins.DataBase.BAOCAONGAYs.Where(x => x.Ngay == SelectedItem.Ngay).SingleOrDefault();
                BaoCaoNgay.Ngay = SelectedItem.Ngay;
                BaoCaoNgay.SoLuongTiecCuoi = SelectedItem.SoLuongTiecCuoi;
                BaoCaoNgay.DoanhThu = SelectedItem.DoanhThu;
                BaoCaoNgay.TiLe = SelectedItem.TiLe;
                DataProvider.Ins.DataBase.SaveChanges();
                SelectedItem.Ngay = Ngay;
            });
        }
    }
}
