using CoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using PayPal.Api;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        QLCFEntities database = new QLCFEntities();
        public ActionResult TrangChu()
        {
            return View();
        }
        
        public ActionResult Detail(int id)
        {
            var product = database.SANPHAMs.FirstOrDefault(n => n.MASP == id);
            return View(product);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult Tra()
        {
            var tra = database.SANPHAMs.Where(n => n.LOAISP == 1).Take(4).ToList();
            return PartialView(tra);   
        }
        public ActionResult Goiy(string id)
        {
            var product = database.SANPHAMs.Where(n => n.TENSP.Contains(id)).Take(10).ToList();
            return PartialView(product);
        }

        public ActionResult Coffe()
        {
            var cafe = database.SANPHAMs.Where(n => n.LOAISP == 2).Take(4).ToList();
            return PartialView(cafe);
        }
        public ActionResult Daxay()
        {
            var daxay = database.SANPHAMs.Where(n => n.LOAISP == 3).Take(4).ToList();
            return PartialView(daxay);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SanPhamLoai(int id, int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            var lstProductCate = database.SANPHAMs.Where(n => n.LOAISP1.MALSP == id).ToList();
            
            return View(lstProductCate.ToPagedList(pageNum, pageSize));
        }
        
        public ActionResult ListMenu()
        {
            var lstCategory = database.LOAISPs.ToList();
            return PartialView(lstCategory);

        }
        public ActionResult ThemSPLove(int MaSP)
        {
            var list = database.LISTLOVEs;
            LISTLOVE listlove = new LISTLOVE();
            KHACHHANG nguoiDung = (KHACHHANG)Session["TaiKhoan"];
            if (nguoiDung == null) {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            else {
                
                listlove.IDSP = MaSP;
                listlove.IDKH = nguoiDung.IDKH;

                database.LISTLOVEs.Add(listlove);
                database.SaveChanges();
                return RedirectToAction("Menu", "Home");
            }
            
        }

     
       
    }
}