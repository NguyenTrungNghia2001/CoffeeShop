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

namespace CoffeeShop.Areas.Nhanvien.Controllers
{
    public class CongThucsController : Controller
    {
        private QLCFEntities db = new QLCFEntities();

        // GET: Admin/CongThucs
        public ActionResult Index()
        {
            var congThucs = db.CongThucs.Include(c => c.NGUYENLIEU).Include(c => c.SANPHAM);
            return View(congThucs.ToList());
        }

        // GET: Admin/CongThucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongThuc congThuc = db.CongThucs.Find(id);
            if (congThuc == null)
            {
                return HttpNotFound();
            }
            return View(congThuc);
        }

        // GET: Admin/CongThucs/Create
        public ActionResult Create()
        {
            ViewBag.IDNL = new SelectList(db.NGUYENLIEUx, "IDNL", "TENNL");
            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP", "TENSP");
            return View();
        }

        // POST: Admin/CongThucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCT,MASP,TENSP,IDNL,SOLUONG,DonVi")] CongThuc congThuc)
        {
            if (ModelState.IsValid)
            {
                db.CongThucs.Add(congThuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDNL = new SelectList(db.NGUYENLIEUx, "IDNL", "TENNL", congThuc.IDNL);
            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP", "TENSP", congThuc.MASP);
            return View(congThuc);
        }

        // GET: Admin/CongThucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongThuc congThuc = db.CongThucs.Find(id);
            if (congThuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNL = new SelectList(db.NGUYENLIEUx, "IDNL", "TENNL", congThuc.IDNL);
            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP", "TENSP", congThuc.MASP);
            return View(congThuc);
        }

        // POST: Admin/CongThucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCT,MASP,TENSP,IDNL,SOLUONG")] CongThuc congThuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congThuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNL = new SelectList(db.NGUYENLIEUx, "IDNL", "TENNL", congThuc.IDNL);
            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP", "TENSP", congThuc.MASP);
            return View(congThuc);
        }

        // GET: Admin/CongThucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongThuc congThuc = db.CongThucs.Find(id);
            if (congThuc == null)
            {
                return HttpNotFound();
            }
            return View(congThuc);
        }

        // POST: Admin/CongThucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CongThuc congThuc = db.CongThucs.Find(id);
            db.CongThucs.Remove(congThuc);
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

        public ActionResult Nguyenlieu()
        {
            return PartialView();
        }
    }
}
