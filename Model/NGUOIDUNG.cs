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
    
    public partial class NGUOIDUNG
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string TenNguoiDung { get; set; }
        public int MaNhomNguoiDung { get; set; }
    
        public virtual NHOMNGUOIDUNG NHOMNGUOIDUNG { get; set; }
    }
}
