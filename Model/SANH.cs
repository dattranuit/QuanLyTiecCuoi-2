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
    
    public partial class SANH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANH()
        {
            this.TIECCUOIs = new HashSet<TIECCUOI>();
        }
    
        public int MaSanh { get; set; }
        public string TenSanh { get; set; }
        public int SoLuongBanToiDa { get; set; }
        public string GhiChu { get; set; }
        public int MaLoaiSanh { get; set; }
    
        public virtual LOAISANH LOAISANH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIECCUOI> TIECCUOIs { get; set; }
    }
}
