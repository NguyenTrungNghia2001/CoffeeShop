using CoffeeShop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models
{
    public class NLXUAT
    {
        QLCFEntities database = new QLCFEntities();
        public int IdNLKHO { get; set; }
        public string TenNLKHO { get; set; }
        public int Quantity { get; set; }

        public NLXUAT(int IDNLKHO)
        {
            this.IdNLKHO = IDNLKHO;
            var nlieu = database.NGUYENLIEUKHOes.Single(n => n.IDNLKHO == this.IdNLKHO);
            this.TenNLKHO = nlieu.TENNLKHO;
            this.Quantity = 1;
        }
    }
}