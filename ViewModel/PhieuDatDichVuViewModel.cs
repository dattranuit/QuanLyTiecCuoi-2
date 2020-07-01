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
        private static ObservableCollection<PHIEUDATDICHVU> _ListPhieuDatDichVu;
        public static ObservableCollection<PHIEUDATDICHVU> ListPhieuDatDichVu { get => _ListPhieuDatDichVu; set { _ListPhieuDatDichVu = value; } }
        private static ObservableCollection<DICHVU> _ListDichVu;
        public static ObservableCollection<DICHVU> ListDichVu { get => _ListDichVu; set { _ListDichVu = value; } }
        private static int _CurrentMaTiecCuoi;
        public static int CurrentMaTiecCuoi { get => _CurrentMaTiecCuoi; set =>_CurrentMaTiecCuoi = value;}
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
                    PDDV_SoLuong = SelectedPDDV.SoLuong;
                    PDDV_ThanhTien = SelectedPDDV.ThanhTien;
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
                }
                DV_SoLuong = 0;
                DV_ThanhTien = 0;
            }
        }
        private int _MaDichVu;
        public int MaDichVu { get => _MaDichVu; set { _MaDichVu = value; OnPropertyChanged(); } }
        private int _PDDV_SoLuong = 0;
        public int PDDV_SoLuong
        { get => _PDDV_SoLuong; 
            set { 
                if(SelectedPDDV !=null) _PDDV_SoLuong = value;
                OnPropertyChanged();
                if (_PDDV_SoLuong < 0)
                {
                    _PDDV_SoLuong = 0;
                    MessageBox.Show("Số lượng không được âm");
                }
                if (SelectedPDDV != null) PDDV_ThanhTien = PDDV_SoLuong * SelectedPDDV.DICHVU.DonGia;
            } 
        }

        private decimal _PDDV_ThanhTien;
        public decimal PDDV_ThanhTien { get => _PDDV_ThanhTien; set { _PDDV_ThanhTien = value; OnPropertyChanged(); } }


        private int _DV_SoLuong = 0;
        public int DV_SoLuong
        {
            get => _DV_SoLuong; set
            {
                if (SelectedDV != null) _DV_SoLuong = value;
                if (_DV_SoLuong <0)
                {
                    _DV_SoLuong = 0;
                    MessageBox.Show("Số lượng không được âm");
                }
                OnPropertyChanged();
                if (SelectedDV != null) DV_ThanhTien = DV_SoLuong * SelectedDV.DonGia;
            }
        }
        private decimal _DV_ThanhTien;
        public decimal DV_ThanhTien { get => _DV_ThanhTien; set { _DV_ThanhTien = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public PhieuDatDichVuViewModel()
        {
            ListDichVu = new ObservableCollection<DICHVU>(DataProvider.Ins.DataBase.DICHVUs);
            ListPhieuDatDichVu = new ObservableCollection<PHIEUDATDICHVU>(DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaTiecCuoi == CurrentMaTiecCuoi));
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedDV == null)
                    return false;
                var CheckExist = DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Where(x => x.MaDichVu == SelectedDV.MaDichVu && x.MaTiecCuoi == CurrentMaTiecCuoi).FirstOrDefault();
                if (CheckExist != null)
                    return false;
                return true;
            }, (p) =>
            {
                if (DV_SoLuong == 0)
                    MessageBox.Show("Số lượng phải lớn hơn 0", "Lưu ý", MessageBoxButton.OK);
                else
                {
                    var PhieuDatDichVu = new PHIEUDATDICHVU()
                    {
                        MaTiecCuoi = CurrentMaTiecCuoi,
                        MaDichVu = MaDichVu,
                        SoLuong = DV_SoLuong,
                        ThanhTien = DV_ThanhTien
                    };
                    DataProvider.Ins.DataBase.PHIEUDATDICHVUs.Add(PhieuDatDichVu);
                    DataProvider.Ins.DataBase.SaveChanges();
                    ListPhieuDatDichVu.Add(PhieuDatDichVu);
                }
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedPDDV == null || PDDV_SoLuong == SelectedPDDV.SoLuong)
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
                PhieuDatDichVu.SoLuong = PDDV_SoLuong;
                PhieuDatDichVu.ThanhTien = PDDV_ThanhTien;
                DataProvider.Ins.DataBase.SaveChanges();
            });
        }
    }
}
