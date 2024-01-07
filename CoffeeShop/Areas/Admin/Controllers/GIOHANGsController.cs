using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoffeeShop.Controllers;
using CoffeeShop.Models;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class GIOHANGsController : Controller
    {
        private QLCFEntities db = new QLCFEntities();

        // GET: Admin/GIOHANGs
        public ActionResult Index()
        {
            var gIOHANGs = db.GIOHANGs.Include(g => g.KHACHHANG);
            return View(gIOHANGs.ToList());
        }

        // GET: Admin/GIOHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIOHANG gIOHANG = db.GIOHANGs.Find(id);
            if (gIOHANG == null)
            {
                return HttpNotFound();
            }
            return View(gIOHANG);
        }

        // GET: Admin/GIOHANGs/Create
        public ActionResult Create()
        {
            ViewBag.IDKH = new SelectList(db.KHACHHANGs, "IDKH", "TenKH");
            return View();
        }

        // POST: Admin/GIOHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NGAYDAT,IDKH,Trigia,AddressDeliverry,Dagiao,Ngaygiaohang,TenNguoiNhan,HTThanhToan,HTGiaoHang,DienThoaiNhan,TTrang")] GIOHANG gIOHANG)
        {
            if (ModelState.IsValid)
            {
                db.GIOHANGs.Add(gIOHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDKH = new SelectList(db.KHACHHANGs, "IDKH", "TenKH", gIOHANG.IDKH);
            return View(gIOHANG);
        }

        // GET: Admin/GIOHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIOHANG gIOHANG = db.GIOHANGs.Find(id);
            if (gIOHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKH = new SelectList(db.KHACHHANGs, "IDKH", "TenKH", gIOHANG.IDKH);
            return View(gIOHANG);
        }

        // POST: Admin/GIOHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NGAYDAT,IDKH,Trigia,AddressDeliverry,Dagiao,Ngaygiaohang,TenNguoiNhan,HTThanhToan,HTGiaoHang,DienThoaiNhan,TTrang")] GIOHANG gIOHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIOHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKH = new SelectList(db.KHACHHANGs, "IDKH", "TenKH", gIOHANG.IDKH);
            return View(gIOHANG);
        }

        // GET: Admin/GIOHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIOHANG gIOHANG = db.GIOHANGs.Find(id);
            if (gIOHANG == null)
            {
                return HttpNotFound();
            }
            return View(gIOHANG);
        }

        // POST: Admin/GIOHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GIOHANG gIOHANG = db.GIOHANGs.Find(id);
            db.GIOHANGs.Remove(gIOHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
