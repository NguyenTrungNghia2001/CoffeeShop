//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeeShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOADONNHAPXUAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADONNHAPXUAT()
        {
            this.CHITIETHOADONNHAPXUATs = new HashSet<CHITIETHOADONNHAPXUAT>();
        }
    
        public int IDHD { get; set; }
        public Nullable<int> IDKHO { get; set; }
        public string DiaChiCH { get; set; }
        public Nullable<System.DateTime> NGAYXUAT { get; set; }
        public Nullable<int> TONGSOLUONG { get; set; }
        public Nullable<bool> NHAPXUAT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHOADONNHAPXUAT> CHITIETHOADONNHAPXUATs { get; set; }
        public virtual CUAHANG CUAHANG { get; set; }
        public virtual KHOHANG KHOHANG { get; set; }
    }
}
