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
    using QuanLyTiecCuoi.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class LOAISANH: BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAISANH()
        {
            this.SANHs = new HashSet<SANH>();
        }

        private int _MaLoaiSanh;
        private string _TenLoaiSanh;
        private decimal _DonGiaBanToiThieu;

        public int MaLoaiSanh { get => _MaLoaiSanh; set { _MaLoaiSanh = value; OnPropertyChanged(); } }
        public string TenLoaiSanh { get => _TenLoaiSanh; set { _TenLoaiSanh = value; OnPropertyChanged(); } }
        public decimal DonGiaBanToiThieu { get => _DonGiaBanToiThieu; set { _DonGiaBanToiThieu = value; OnPropertyChanged(); } }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANH> SANHs { get; set; }
    }
}
