using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyTiecCuoi.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data.Entity.Migrations.Model;
using Microsoft.Office.Interop.Excel;

namespace QuanLyTiecCuoi.ViewModel
{
    class PhieuDatDichVuViewModel:BaseViewModel
    {
        private ObservableCollection<PHIEUDATDICHVU> _ListPhieuDatDichVu;
        public ObservableCollection<PHIEUDATDICHVU> ListPhieuDatDichVu { get => _ListPhieuDatDichVu; set { _ListPhieuDatDichVu = value; OnPropertyChanged(); } }
        private ObservableCollection<DICHVU> _ListDichVu;
        public ObservableCollection<DICHVU> ListDichVu { get => _ListDichVu; set { _ListDichVu = value; OnPropertyChanged(); } }
        private PHIEUDATDICHVU _SelectedPDDV;
        public PHIEUDATDICHVU SelectedPDDV
        {
            get => _SelectedPDDV;
            set
            {
                _SelectedPDDV = value;
                OnPropertyChanged();
                if (SelectedPDDV != null)
                {
                    MaDichVu = SelectedPDDV.MaDichVu;
                    Sua_PDDV_SoLuong = SelectedPDDV.SoLuong;
                    Sua_PDDV_ThanhTien = SelectedPDDV.ThanhTien;
                }
            }
        }
        private DICHVU _SelectedDV;
        public DICHVU SelectedDV
        {
            get => _SelectedDV;
            set
            {
                _SelectedDV = value;
                OnPropertyChanged();
                if(SelectedDV != null)
                {
                    MaDichVu = SelectedDV.MaDichVu;
                    DDV_SoLuong = 0;
                    DDV_ThanhTien = 0;
                }
            }
        }
        private int _MaTiecCuoi = TiecViewModel.CurrentMaTiecCuoi;
        public int MaTiecCuoi { get => _MaTiecCuoi; set { _MaTiecCuoi = value; OnPropertyChanged(); } }
        private int _MaDichVu;
        public int MaDichVu { get => _MaDichVu; set { _MaDichVu = value; OnPropertyChanged(); } }
        private int _Sua_PDDV_SoLuong = 0;
        public int Sua_PDDV_SoLuong
        { get => _Sua_PDDV_SoLuong; set { 
                if(SelectedPDDV !=null) _Sua_PDDV_SoLuong = value; OnPropertyChanged(); 
                if(SelectedPDDV != null) Sua_PDDV_ThanhTien = Sua_PDDV_SoLuong * SelectedPDDV.DICHVU.DonGia;} }

        private decimal _Sua_PDDV_ThanhTien;
        public decimal Sua_PDDV_ThanhTien { get => _Sua_PDDV_ThanhTien; set { _Sua_PDDV_ThanhTien = value; OnPropertyChanged(); } }


        private int _DDV_SoLuong = 0;
        public int DDV_SoLuong
        {
            get => _DDV_SoLuong; set
            {
                if (SelectedDV != null) _DDV_SoLuong = value; OnPropertyChanged();
                if (SelectedDV != null) DDV_ThanhTien = DDV_SoLuong * SelectedDV.DonGia;
            }
        }
        private decimal _DDV_ThanhTien;
        public decimal DDV_ThanhTien { get => _DDV_ThanhTien; set { _DDV_ThanhTien = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public PhieuDatDichVuViewModel()
        {
            ListDichVu = new ObservableCollection<DICHVU>(DataProvider.Ins.DataBase.DICHVUs);
            ListPhieuDatDichVu = new ObservableCollection<PHIEUDATDICHVU>(DataProvider.Ins.DataBase.PHIEUDATDICHVUs);
            AddCommand = new RelayCommand<object>((p) =>
            {
                var CheckExist = DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaDichVu == SelectedDV.MaDichVu).FirstOrDefault();
                if (CheckExist != null)
                    return false;
                return true;
            }, (p) =>
            {
                if (DDV_SoLuong == 0)
                    MessageBox.Show("Số lượng phải lớn hơn 0", "Lưu ý", MessageBoxButton.OK);
                else
                {
                    var PhieuDatDichVu = new PHIEUDATDICHVU()
                    {
                        MaTiecCuoi = MaTiecCuoi,
                        MaDichVu = MaDichVu,
                        SoLuong = DDV_SoLuong,
                        ThanhTien = DDV_ThanhTien
                    };
                    DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Add(PhieuDatDichVu);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListPhieuDatDichVu.Add(PhieuDatDichVu);
                }
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedPDDV == null || Sua_PDDV_SoLuong == SelectedPDDV.SoLuong)
                    return false;
                var displayList = DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaDichVu == SelectedPDDV.MaDichVu && x.MaTiecCuoi == SelectedPDDV.MaTiecCuoi);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var PhieuDatDichVu = DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaDichVu == SelectedPDDV.MaDichVu && x.MaTiecCuoi == SelectedPDDV.MaTiecCuoi).SingleOrDefault();
                PhieuDatDichVu.MaDichVu = SelectedPDDV.MaDichVu;
                PhieuDatDichVu.MaTiecCuoi = SelectedPDDV.MaTiecCuoi;
                PhieuDatDichVu.SoLuong = Sua_PDDV_SoLuong;
                PhieuDatDichVu.ThanhTien = Sua_PDDV_ThanhTien;
                DataProvider.Ins.DataBase.SaveChanges();
            });
        }
    }
}
