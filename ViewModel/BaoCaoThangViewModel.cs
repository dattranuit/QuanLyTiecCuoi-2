using QuanLyTiecCuoi.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private int _Thang;
        public int Thang { get => _Thang; set { _Thang = value; OnPropertyChanged(); } }


        private int _Nam;
        public int Nam { get => _Nam; set { _Nam = value; OnPropertyChanged(); } }


        private decimal _TongDoanhThu;
        public decimal TongDoanhThu { get => _TongDoanhThu; set { _TongDoanhThu = value; OnPropertyChanged(); } }

        public ICommand DoubleClickCommand { get; set; }

        private ObservableCollection<BAOCAONGAY> _List2;
        public ObservableCollection<BAOCAONGAY> List2 { get => _List2; set { _List2 = value; OnPropertyChanged(); } }

        public BaoCaoThangViewModel()
        {
            List = new ObservableCollection<BAOCAOTHANG>(DataProvider.Ins.DataBase.BAOCAOTHANGs);
           
            DoubleClickCommand = new RelayCommand<DataGrid>((p) =>
            {
                return true;
            }, (p) =>
            {
                int id = _getMaBaoCaoThang(p);
                List2 = new ObservableCollection<BAOCAONGAY>(DataProvider.Ins.DataBase.BAOCAONGAYs.Where(x => x.MaBaoCaoThang == id));
                DataProvider.Ins.DataBase.SaveChanges();
                
            });
            

        }

        public int _getMaBaoCaoThang(DataGrid dataGrid)
        {
            if (dataGrid.SelectedItem != null)
            {
                BAOCAOTHANG IdBaoCaoThang = dataGrid.SelectedItem as BAOCAOTHANG;
                return IdBaoCaoThang.MaBaoCaoThang;

            }
            else
            {
                MessageBox.Show("Lỗi");
                return -1;
            }
        }
    }
}
