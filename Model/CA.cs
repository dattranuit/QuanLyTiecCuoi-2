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
    public partial class CA:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CA()
        {
            this.TIECCUOIs = new HashSet<TIECCUOI>();
        }

        private int _MaCa;
        private string _TenCa;
        private System.TimeSpan _BatDau;
        private System.TimeSpan _KetThuc;
        public int MaCa { get => _MaCa; set { _MaCa = value; OnPropertyChanged(); } }
        public string TenCa { get => _TenCa; set { _TenCa = value; OnPropertyChanged(); } }
        public System.TimeSpan BatDau { get => _BatDau; set { _BatDau = value; OnPropertyChanged(); } }
        public System.TimeSpan KetThuc { get => _KetThuc; set { _KetThuc = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIECCUOI> TIECCUOIs { get; set; }
    }
}
