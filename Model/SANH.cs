//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyTiecCuoi.Model
{
    using System;
    using System.Collections.Generic;
    using QuanLyTiecCuoi.ViewModel;
    public partial class SANH:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANH()
        {
            this.TIECCUOIs = new HashSet<TIECCUOI>();
        }
        private int _MaSanh;
        private string _TenSanh;
        private int _SoLuongBanToiDa;
        private string _GhiChu;
        private int _MaLoaiSanh;
        public int MaSanh { get => _MaSanh; set { _MaSanh = value; OnPropertyChanged(); } }
        public string TenSanh { get => _TenSanh; set { _TenSanh = value; OnPropertyChanged(); } }
        public int SoLuongBanToiDa { get => _SoLuongBanToiDa; set { _SoLuongBanToiDa = value; OnPropertyChanged(); } }
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }
        public int MaLoaiSanh { get => _MaLoaiSanh; set { _MaLoaiSanh = value; OnPropertyChanged(); } }
        public virtual LOAISANH LOAISANH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIECCUOI> TIECCUOIs { get; set; }
    }
}
