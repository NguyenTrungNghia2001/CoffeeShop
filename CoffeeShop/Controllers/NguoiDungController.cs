using CoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.UI;
using PagedList;
using CoffeeShop.Controllers;

namespace CoffeeShop.Models
{
    public class NguoiDungController : Controller
    {

        // GET: NguoiDung
        QLCFEntities database = new QLCFEntities();
        KHACHHANG nguoiDung = System.Web.HttpContext.Current.Session["TaiKhoan"] as KHACHHANG;


       /* [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]*/


        public ActionResult DangKy([Bind(Include = "TenKH,EmailKH,PassKH,SDTKH,MALKH")] KHACHHANG cus)
        {


            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cus.TenKH))
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                if (string.IsNullOrEmpty(cus.EmailKH))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(cus.PassKH))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (string.IsNullOrEmpty(cus.SDTKH.ToString()))
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                var khachhang = database.KHACHHANGs.FirstOrDefault(k => k.EmailKH == cus.EmailKH);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người dăng ký tên này");

                if (ModelState.IsValid)
                {
                    
                    database.KHACHHANGs.Add(cus);
                    database.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("DangNhap");
        }
        public List<MatHangMua> LayGioHang()
        {
            List<MatHangMua> gioHang = Session["GioHang"] as List<MatHangMua>;
            if (gioHang == null)
            {
                gioHang = new List<MatHangMua>();
                Session["GioHang"] = gioHang;
            }
            return gioHang;
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(KHACHHANG cus)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cus.EmailKH))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(cus.PassKH))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    var khach = database.KHACHHANGs.FirstOrDefault(k => k.EmailKH == cus.EmailKH && k.PassKH == cus.PassKH);
                    if (khach != null)
                    {
                        ViewBag.ThongBao = "Đăng nhập thành công";
                        Session["NameCus"] = khach.TenKH;
                        Session["TaiKhoan"] = khach;
                        Session["IDCus"] = khach.IDKH;
                        return RedirectToAction("TrangChu", "Home");
                    }
                    else

                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult ThongTinNguoiDung()
        {
            KHACHHANG nguoiDung = (KHACHHANG)Session["TaiKhoan"];
            nguoiDung.PassKH = nguoiDung.PassKH.TrimEnd();
            return View(database.KHACHHANGs.Where(s => s.IDKH == nguoiDung.IDKH).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult CapNhatThongTinCaNhan(KHACHHANG nguoiDung)
        {
            //if(ModelState.IsValid)
            try
            {
                //nguoiDung.ConfirmPass = nguoiDung.Password;
              //  nguoiDung = database.KHACHHANGs.Where((s) => s.IDKH == nguoiDung.IDKH).Select(new KHACHHANG { IDKH=nguoiDung.IDKH}).FirstOrDefault();
                database.Entry(nguoiDung).State = EntityState.Modified;
                database.SaveChanges();
                Session["TaiKhoan"] = nguoiDung;
                return RedirectToAction("ThongTinNguoiDung", new { id = nguoiDung.IDKH });
            }
            catch (DbEntityValidationException e)
            {
                string a = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    a += ("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State) + "\n";
                    foreach (var ve in eve.ValidationErrors)
                    {
                        a += ("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage) + "\n";
                    }
                }
                //throw;
                return Content(a);
            }
        }
        public ActionResult LichSuDonHang()
        {

            var LSDonHang = database.GIOHANGs.Where(n => n.IDKH == nguoiDung.IDKH).ToList();
            return View(LSDonHang);
        }
        public ActionResult DanhSachLove(int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);

            var LSDonHang = database.LISTLOVEs.Where(n => n.IDKH == nguoiDung.IDKH).ToList();
            return View(LSDonHang.ToPagedList(pageNum, pageSize));
        }
        public ActionResult HuyDonHang()
        {
            return View();
        }
        public ActionResult ThongTinDonHang(int id)
        {
            var giohang = database.GIOHANGs.FirstOrDefault(n => n.ID == id);
            return View(giohang);
        }
    }
}