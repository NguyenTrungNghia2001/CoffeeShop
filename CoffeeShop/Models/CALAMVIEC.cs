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
    
    public partial class CALAMVIEC
    {
        public int MACLV { get; set; }
        public string TENCLV { get; set; }
        public System.TimeSpan GIOBD { get; set; }
        public System.TimeSpan GIOKT { get; set; }
        public string DiaChiCH { get; set; }
    
        public virtual CUAHANG CUAHANG { get; set; }
    }
}
