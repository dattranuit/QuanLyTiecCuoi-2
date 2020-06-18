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
        private int _MaCa;
        public int MaCa { get => _MaCa; set { _MaCa = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand PhieuDatBanCommand { get; set; }
        public CaViewModel()
        {
            ListCa = new ObservableCollection<CA>(DataProvider.Ins.DataBase.CAs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var Ca = new CA()
                {
                    TenCa = TenCa,
                    MaCa = MaCa                    
                };
                DataProvider.Ins.DataBase.CAs.Add(Ca);
                DataProvider.Ins.DataBase.SaveChanges();
                ListCa.Add(Ca);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DataBase.CAs.Where(x => x.MaCa == SelectedItem.MaCa);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Ca = DataProvider.Ins.DataBase.CAs.Where(x => x.MaCa == SelectedItem.MaCa).SingleOrDefault();
                Ca.TenCa = SelectedItem.TenCa;
                Ca.MaCa = SelectedItem.MaCa;
                DataProvider.Ins.DataBase.SaveChanges();
            });
        }
    }
}
