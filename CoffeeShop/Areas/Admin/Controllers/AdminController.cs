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
    public class AdminController : Controller
    {
        QLCFEntities database = new QLCFEntities();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ADMINuser admin)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(admin.TENDANGNHAP))
                    ModelState.AddModelError(string.Empty, "User name không được để trống");
                if (string.IsNullOrEmpty(admin.MATKHAU))
                    ModelState.AddModelError(string.Empty, "Password không được để trống");
                //Kiểm tra có admin này hay chưa
                var adminDB = database.ADMINusers.FirstOrDefault(ad => ad.TENDANGNHAP == admin.TENDANGNHAP && ad.MATKHAU == admin.MATKHAU && ad.ID == admin.ID);
                if (adminDB == null)
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
                else
                {
                    Session["Admin"] = adminDB;
                    ViewBag.ThongBao = "Đăng nhập admin thành công";
                    return RedirectToAction("Index", "Statistic");
                }
            }
            return View();

        }
    }
}