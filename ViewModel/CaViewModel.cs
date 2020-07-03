using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyTiecCuoi.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace QuanLyTiecCuoi.ViewModel
{
    class CaViewModel:BaseViewModel
    {
        private ObservableCollection<CA> _ListCa;
        public ObservableCollection<CA> ListCa { get => _ListCa; set { _ListCa = value; OnPropertyChanged(); } }

        private CA _SelectedItem;
        public CA SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenCa = SelectedItem.TenCa;
                    MaCa = SelectedItem.MaCa;
                }
            }
        }
        private string _TenCa;
        public string TenCa { get => _TenCa; set { _TenCa = value; OnPropertyChanged(); } }
        private System.TimeSpan _BatDau;
        public System.TimeSpan BatDau { get => _BatDau; set { _BatDau = value; OnPropertyChanged(); } }
        private System.TimeSpan _KetThuc;
        public System.TimeSpan KetThuc { get => _KetThuc; set { _KetThuc = value; OnPropertyChanged(); } }
        private int _MaCa;
        public int MaCa { get => _MaCa; set { _MaCa = value; OnPropertyChanged(); } }
        private bool _ApDungQuiDinhPhat { get; set; }
        private double _TiLePhat { get; set; }
        public bool ApDungQuiDinhPhat { get => _ApDungQuiDinhPhat; set { _ApDungQuiDinhPhat = value; OnPropertyChanged(); } }
        public double TiLePhat { get => _TiLePhat; set { _TiLePhat = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditQuiDinhCommand { get; set; }

        //hàm được gọi mỗi khi bool ApDungQuiDinhPhat được set. Tạm bỏ
        public void ThayDoiApDungQuiDinh()
        {
            var ApDungQD = DataProvider.Ins.DataBase.THAMSOes.Where(x => x.TenThamSo == "ApDungQuiDinhPhat").First();
            if (ApDungQuiDinhPhat)
                ApDungQD.GiaTri = 1;
            else
                ApDungQD.GiaTri = 0;
            DataProvider.Ins.DataBase.SaveChanges();
        }

        //hàm được gọi mỗi khi double TiLePhat được set. Tạm bỏ
        public void ThayDoiTiLePhat()
        {
            var TiLeP = DataProvider.Ins.DataBase.THAMSOes.Where(x => x.TenThamSo == "TiLePhat").First();
            TiLeP.GiaTri = TiLePhat / 100;
            DataProvider.Ins.DataBase.SaveChanges();

        }
        public CaViewModel()
        {
            ListCa = new ObservableCollection<CA>(DataProvider.Ins.DataBase.CAs);
            //load tỉ lệ phạt lên từ database
            var TLPhat = DataProvider.Ins.DataBase.THAMSOes.Where(x => x.TenThamSo == "TiLePhat").First();
            TiLePhat = TLPhat.GiaTri*100;
            //load áp dụng qui định hay không từ database
            var ADQDP = DataProvider.Ins.DataBase.THAMSOes.Where(x => x.TenThamSo == "ApDungQuiDinhPhat").First();
            if (ADQDP.GiaTri == 1)
                ApDungQuiDinhPhat = true;
            else
                ApDungQuiDinhPhat = false;
            /////////////////////////////////////////
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var Ca = new CA()
                {
                    TenCa = TenCa,
                    MaCa = MaCa,
                    BatDau=BatDau,
                    KetThuc=KetThuc,
                };
                DataProvider.Ins.DataBase.CAs.Add(Ca);
                DataProvider.Ins.DataBase.SaveChanges();
                ListCa.Add(Ca);
                MessageBox.Show("Thêm Ca thành công !");
                SelectedItem = Ca;
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.CAs.Where(x => x.MaCa == SelectedItem.MaCa);
                if (displayList == null && displayList.Count() == 0)
                    return false;
                if (SelectedItem.TenCa == TenCa && SelectedItem.BatDau == BatDau && SelectedItem.KetThuc == KetThuc)
                    return false;
                return true;
            }, (p) =>
            {
                var Ca = DataProvider.Ins.DataBase.CAs.Where(x => x.MaCa == SelectedItem.MaCa).SingleOrDefault();
                Ca.TenCa = SelectedItem.TenCa;
                Ca.BatDau = SelectedItem.BatDau;
                Ca.KetThuc = SelectedItem.KetThuc;
                DataProvider.Ins.DataBase.SaveChanges();
                MessageBox.Show("Sửa thông tin Ca thành công !");
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                var Ca = DataProvider.Ins.DataBase.CAs.Where(x => x.MaCa == SelectedItem.MaCa).First();
                var TiecCuoi = DataProvider.Ins.DataBase.TIECCUOIs.Where(x => x.MaCa == SelectedItem.MaCa);
                if (TiecCuoi.Count() != 0)
                {
                    MessageBox.Show("Không thể xóa vì có tồn tại Tiệc Cưới được đặt ở Ca này !");
                    return;
                }
                DataProvider.Ins.DataBase.CAs.Remove(Ca);
                DataProvider.Ins.DataBase.SaveChanges();
                ListCa.Remove(Ca);
                MessageBox.Show("Xóa Ca thành công !");
                //refresh nhap
                TenCa = "";
            });

            EditQuiDinhCommand = new RelayCommand<object>((p) =>
            {
                var ApDungQDP = DataProvider.Ins.DataBase.THAMSOes.Where(x => x.TenThamSo == "ApDungQuiDinhPhat").First();
                var TLP = DataProvider.Ins.DataBase.THAMSOes.Where(x => x.TenThamSo == "TiLePhat").First();
                bool test = true;
                if (ApDungQDP.GiaTri == 0)
                    test = false;
                if (test == ApDungQuiDinhPhat && TLP.GiaTri == TiLePhat/100)
                    return false;
                return true;
            }, (p) =>
            {
                ThayDoiApDungQuiDinh();
                ThayDoiTiLePhat();
                MessageBox.Show("Sửa Qui Định thành công !");
            });
        }
    }
}
