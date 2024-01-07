using CoffeeShop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models
{
    public class Loveitem
    {

        QLCFEntities database = new QLCFEntities();
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string HINHANH { get; set; }
        public double GIASP { get; set; }

        public Loveitem(int MASP) {
            this.MaSP = MASP;
            var product = database.SANPHAMs.Single(n => n.MASP == this.MaSP);
            this.TenSP = product.TENSP;
            this.HINHANH = product.HINHANH;
            this.GIASP = double.Parse(product.GIASP.ToString());
        }

    }
        
}