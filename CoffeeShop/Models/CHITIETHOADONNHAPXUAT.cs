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
    
    public partial class CHITIETHOADONNHAPXUAT
    {
        public int ID { get; set; }
        public Nullable<int> IDNLKHO { get; set; }
        public Nullable<int> SOLUONG { get; set; }
        public Nullable<int> IDHD { get; set; }
    
        public virtual NGUYENLIEUKHO NGUYENLIEUKHO { get; set; }
        public virtual HOADONNHAPXUAT HOADONNHAPXUAT { get; set; }
    }
}
