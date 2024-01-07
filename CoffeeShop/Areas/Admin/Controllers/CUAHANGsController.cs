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
    public class CUAHANGsController : Controller
    {
        private QLCFEntities db = new QLCFEntities();

        // GET: Admin/CUAHANGs
        public ActionResult Index()
        {
            var cUAHANGs = db.CUAHANGs.Include(c => c.KHOHANG);
            return View(cUAHANGs.ToList());
        }

        // GET: Admin/CUAHANGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUAHANG cUAHANG = db.CUAHANGs.Find(id);
            if (cUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(cUAHANG);
        }

        // GET: Admin/CUAHANGs/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/CUAHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCH,DiaChiCH,SLNhanVien,DoanhThu")] CUAHANG cUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.CUAHANGs.Add(cUAHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(cUAHANG);
        }

        // GET: Admin/CUAHANGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUAHANG cUAHANG = db.CUAHANGs.Find(id);
            if (cUAHANG == null)
            {
                return HttpNotFound();
            }


            return View(cUAHANG);
        }

        // POST: Admin/CUAHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCH,DiaChiCH,SLNhanVien,DoanhThu")] CUAHANG cUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cUAHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cUAHANG);
        }

        // GET: Admin/CUAHANGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUAHANG cUAHANG = db.CUAHANGs.Find(id);
            if (cUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(cUAHANG);
        }

        // POST: Admin/CUAHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CUAHANG cUAHANG = db.CUAHANGs.Find(id);
            db.CUAHANGs.Remove(cUAHANG);
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
