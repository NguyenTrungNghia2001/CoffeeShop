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
    
    public partial class NGUYENLIEU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGUYENLIEU()
        {
            this.CongThucs = new HashSet<CongThuc>();
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        public int IDNL { get; set; }
        public string TENNL { get; set; }
        public int IDLNL { get; set; }
        public Nullable<int> SOLUONGTON { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongThuc> CongThucs { get; set; }
        public virtual LOAINGUYENLIEU LOAINGUYENLIEU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}