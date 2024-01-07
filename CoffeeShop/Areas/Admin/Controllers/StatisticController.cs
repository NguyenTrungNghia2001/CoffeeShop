using CoffeeShop.Controllers;
using CoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Admin/Statistic
        QLCFEntities db = new QLCFEntities();   
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
        {
            var query = from o in db.GIOHANGs
                        select new
                        {
                            NGAYTAO = o.NGAYDAT,
                            TONGTIEN = o.Trigia,
                        };
            if(!string.IsNullOrEmpty(fromDate) )
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "yyyy/MM", null);
                query = query.Where(x => x.NGAYTAO >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "yyyy/MM", null);
                query = query.Where(x => x.NGAYTAO < endDate);
            }
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.NGAYTAO)).Select(x=> new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y=>y.TONGTIEN),
            }).Select(x=> new
            {
                Date = x.Date,
                DoanhThu = x.TotalBuy
            });
            return Json(new {Data = result}, JsonRequestBehavior.AllowGet);
        }
    }
}