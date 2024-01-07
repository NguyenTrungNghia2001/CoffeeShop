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
    public class NGUYENLIEUxController : Controller
    {
        private QLCFEntities db = new QLCFEntities();

        // GET: Admin/NGUYENLIEUx
        public ActionResult Index()
        {
            var nGUYENLIEUx = db.NGUYENLIEUx.Include(n => n.LOAINGUYENLIEU);
            return View(nGUYENLIEUx.ToList());
        }

        // GET: Admin/NGUYENLIEUx/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUYENLIEU nGUYENLIEU = db.NGUYENLIEUx.Find(id);
            if (nGUYENLIEU == null)
            {
                return HttpNotFound();
            }
            return View(nGUYENLIEU);
        }

        // GET: Admin/NGUYENLIEUx/Create
        public ActionResult Create()
        {
            ViewBag.IDLNL = new SelectList(db.LOAINGUYENLIEUx, "IDLNL", "TenLNL");
            return View();
        }

        // POST: Admin/NGUYENLIEUx/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNL,TENNL,IDLNL,SOLUONGTON")] NGUYENLIEU nGUYENLIEU)
        {
            if (ModelState.IsValid)
            {
                db.NGUYENLIEUx.Add(nGUYENLIEU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDLNL = new SelectList(db.LOAINGUYENLIEUx, "IDLNL", "TenLNL", nGUYENLIEU.IDLNL);
            return View(nGUYENLIEU);
        }

        // GET: Admin/NGUYENLIEUx/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUYENLIEU nGUYENLIEU = db.NGUYENLIEUx.Find(id);
            if (nGUYENLIEU == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLNL = new SelectList(db.LOAINGUYENLIEUx, "IDLNL", "TenLNL", nGUYENLIEU.IDLNL);
            return View(nGUYENLIEU);
        }

        // POST: Admin/NGUYENLIEUx/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNL,TENNL,IDLNL,SOLUONGTON")] NGUYENLIEU nGUYENLIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGUYENLIEU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLNL = new SelectList(db.LOAINGUYENLIEUx, "IDLNL", "TenLNL", nGUYENLIEU.IDLNL);
            return View(nGUYENLIEU);
        }

        // GET: Admin/NGUYENLIEUx/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUYENLIEU nGUYENLIEU = db.NGUYENLIEUx.Find(id);
            if (nGUYENLIEU == null)
            {
                return HttpNotFound();
            }
            return View(nGUYENLIEU);
        }

        // POST: Admin/NGUYENLIEUx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NGUYENLIEU nGUYENLIEU = db.NGUYENLIEUx.Find(id);
            db.NGUYENLIEUx.Remove(nGUYENLIEU);
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
