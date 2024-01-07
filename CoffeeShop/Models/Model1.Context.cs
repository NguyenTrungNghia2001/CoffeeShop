﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QLCFEntities : DbContext
    {
        public QLCFEntities()
            : base("name=QLCFEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADMINuser> ADMINusers { get; set; }
        public virtual DbSet<CALAMVIEC> CALAMVIECs { get; set; }
        public virtual DbSet<CHITIETGIOHANG> CHITIETGIOHANGs { get; set; }
        public virtual DbSet<CHITIETHOADON> CHITIETHOADONs { get; set; }
        public virtual DbSet<CHITIETHOADONNHAPXUAT> CHITIETHOADONNHAPXUATs { get; set; }
        public virtual DbSet<CHITIETLUONGNHANVIEN> CHITIETLUONGNHANVIENs { get; set; }
        public virtual DbSet<CongThuc> CongThucs { get; set; }
        public virtual DbSet<CUAHANG> CUAHANGs { get; set; }
        public virtual DbSet<GIOHANG> GIOHANGs { get; set; }
        public virtual DbSet<HOADON> HOADONs { get; set; }
        public virtual DbSet<HOADONNHAPXUAT> HOADONNHAPXUATs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<KHOHANG> KHOHANGs { get; set; }
        public virtual DbSet<KHUYENMAI> KHUYENMAIs { get; set; }
        public virtual DbSet<LISTLOVE> LISTLOVEs { get; set; }
        public virtual DbSet<LOAIKHACHHANG> LOAIKHACHHANGs { get; set; }
        public virtual DbSet<LOAINGUYENLIEU> LOAINGUYENLIEUx { get; set; }
        public virtual DbSet<LOAISP> LOAISPs { get; set; }
        public virtual DbSet<NGUYENLIEU> NGUYENLIEUx { get; set; }
        public virtual DbSet<NGUYENLIEUKHO> NGUYENLIEUKHOes { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<TINTUC> TINTUCs { get; set; }
    
        public virtual ObjectResult<GetRevenuesStatistic_Result> GetRevenuesStatistic()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRevenuesStatistic_Result>("GetRevenuesStatistic");
        }
    }
}