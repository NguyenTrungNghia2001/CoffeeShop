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
    public class LOAISPsController : Controller
    {
        private QLCFEntities db = new QLCFEntities();

        // GET: Admin/LOAISPs
        public ActionResult Index()
        {
            return View(db.LOAISPs.ToList());
        }

        // GET: Admin/LOAISPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAISP lOAISP = db.LOAISPs.Find(id);
            if (lOAISP == null)
            {
                return HttpNotFound();
            }
            return View(lOAISP);
        }

        // GET: Admin/LOAISPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LOAISPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MALSP,TENLSP,MOTA")] LOAISP lOAISP)
        {
            if (ModelState.IsValid)
            {
                db.LOAISPs.Add(lOAISP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAISP);
        }

        // GET: Admin/LOAISPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAISP lOAISP = db.LOAISPs.Find(id);
            if (lOAISP == null)
            {
                return HttpNotFound();
            }
            return View(lOAISP);
        }

        // POST: Admin/LOAISPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MALSP,TENLSP,MOTA")] LOAISP lOAISP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAISP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAISP);
        }

        // GET: Admin/LOAISPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAISP lOAISP = db.LOAISPs.Find(id);
            if (lOAISP == null)
            {
                return HttpNotFound();
            }
            return View(lOAISP);
        }

        // POST: Admin/LOAISPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOAISP lOAISP = db.LOAISPs.Find(id);
            db.LOAISPs.Remove(lOAISP);
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
