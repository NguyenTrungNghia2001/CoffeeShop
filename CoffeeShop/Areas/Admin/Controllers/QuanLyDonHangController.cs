using CoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using PagedList;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.Data.Entity;
using CoffeeShop.Controllers;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        // GET: Admin/QuanLyDonHang
        private QLCFEntities db = new QLCFEntities();
        public ActionResult Index(int? page)
        {
            var item = db.GIOHANGs.OrderByDescending(x => x.NGAYDAT).ToList();
            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(item.ToPagedList(pageNumber, pageSize));
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

        public ActionResult HienThiDonHang(int id)
        {
            var item = db.GIOHANGs.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, bool trangthai)
        {
            var item = db.GIOHANGs.Find(id);
            if (item != null)
            {
                db.GIOHANGs.Attach(item);
                item.Dagiao = trangthai;
                db.Entry(item).Property(x => x.Dagiao).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });

            }
            return Json(new { message = "Unsuccess", Success = false });
        }
        public ActionResult UpdateTTrang(int id, bool TTrang)
        {
            var item = db.GIOHANGs.Find(id);
            if (item != null)
            {
                db.GIOHANGs.Attach(item);
                item.TTrang = TTrang;
                db.Entry(item).Property(x => x.TTrang).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });

            }
            return Json(new { message = "Unsuccess", Success = false });
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIOHANG orderPro = db.GIOHANGs.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
             ViewBag.IDKH = new SelectList(db.KHACHHANGs, "IDKH", "TenKH", orderPro.IDKH);
            return View(orderPro);
        }

        // POST: Admin/OrderProes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NGATDAT,IDKH,AddressDeliverry,Trigia,Ngaygiaohang,Dagiao,TenNguoiNhan,DienThoaiNhan,HTThanhToan,HTGiaoHang,TTrang")] GIOHANG orderPro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderPro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKH = new SelectList(db.KHACHHANGs, "IDKH", "TenKH", orderPro.IDKH);
            return View(orderPro);
        }

    }

}