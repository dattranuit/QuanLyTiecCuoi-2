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

        private ObservableCollection<HOADON> _ListHoaDon;
        public ObservableCollection<HOADON> ListHoaDon { get => _ListHoaDon; set { _ListHoaDon = value; OnPropertyChanged(); } }

        public BaoCaoThangViewModel()
        {
            List = new ObservableCollection<BAOCAOTHANG>(DataProvider.Ins.DataBase.BAOCAOTHANGs);
            data();
            DataProvider.Ins.DataBase.SaveChanges();
            
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

        public void data()
        {
            ListHoaDon = new ObservableCollection<HOADON>(DataProvider.Ins.DataBase.HOADONs);
            DataProvider.Ins.DataBase.SaveChanges();
            try
            {
                var query = from x in ListHoaDon
                            where (x.NgayThanhToan.Year >= 2000 && x.NgayThanhToan.Year <= 2020)
                            let sum = (from b in ListHoaDon select b.TongTienHoaDon).Sum()
                            select new
                            {
                                thang = x.NgayThanhToan.Month,
                                nam = x.NgayThanhToan.Year,
                                doanhthu = sum
                            };
                //if (query == null) MessageBox.Show("err");
                //Thang = query.FirstOrDefault().thang;
                //MessageBox.Show(query.Count().ToString());
                //Nam = query.FirstOrDefault().nam;
                //TongDoanhThu = query.FirstOrDefault().doanhthu;
                //List.Add(new BAOCAOTHANG() { Thang = Thang, Nam = Nam, TongDoanhThu = TongDoanhThu });
                foreach(var q in query)
                {
                    List.Add(new BAOCAOTHANG() { Thang = q.thang, Nam = q.nam, TongDoanhThu = q.doanhthu });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

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
