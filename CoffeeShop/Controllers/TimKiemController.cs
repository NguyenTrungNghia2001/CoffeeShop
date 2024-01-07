using CoffeeShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QLCFEntities db = new QLCFEntities();
        [HttpGet]
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
          
            int pageSize = 5;
            int pageNum = (page ?? 1);
            var lstSP = db.SANPHAMs.Where(n => n.TENSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
   
            return View(lstSP.OrderBy(n => n.TENSP).ToPagedList(pageNum, pageSize));
        }
    
        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(string sTuKhoa)
        {
            return RedirectToAction("KQTimKiem", new { @sTuKhoa = sTuKhoa });
        }
    }
}