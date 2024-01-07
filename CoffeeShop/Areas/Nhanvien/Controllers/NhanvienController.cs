using CoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class NhanvienController : Controller
    {
        // GET: Nhanvien/Nhanvien
        QLCFEntities database = new QLCFEntities();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (Session["Nhanvien"] == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(NHANVIEN admin)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(admin.EMAIL))
                    ModelState.AddModelError(string.Empty, "User name không được để trống");
                if (string.IsNullOrEmpty(admin.PASSWORD))
                    ModelState.AddModelError(string.Empty, "Password không được để trống");
                //Kiểm tra có admin này hay chưa
                var adminDB = database.NHANVIENs.FirstOrDefault(ad => ad.EMAIL == admin.EMAIL && ad.PASSWORD == admin.PASSWORD);
                if (adminDB == null)
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
                else
                {
                    Session["Nhanvien"] = adminDB;
                    ViewBag.ThongBao = "Đăng nhập admin thành công";
                    return RedirectToAction("Index", "Statisticnv");
                }
            }
            return View();

        }
    }
}