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
    public class LOAINGUYENLIEUxController : Controller
    {
        private QLCFEntities db = new QLCFEntities();

        // GET: Admin/LOAINGUYENLIEUx
        public ActionResult Index()
        {
            return View(db.LOAINGUYENLIEUx.ToList());
        }

        // GET: Admin/LOAINGUYENLIEUx/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAINGUYENLIEU lOAINGUYENLIEU = db.LOAINGUYENLIEUx.Find(id);
            if (lOAINGUYENLIEU == null)
            {
                return HttpNotFound();
            }
            return View(lOAINGUYENLIEU);
        }

        // GET: Admin/LOAINGUYENLIEUx/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LOAINGUYENLIEUx/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLNL,TenLNL")] LOAINGUYENLIEU lOAINGUYENLIEU)
        {
            if (ModelState.IsValid)
            {
                db.LOAINGUYENLIEUx.Add(lOAINGUYENLIEU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAINGUYENLIEU);
        }

        // GET: Admin/LOAINGUYENLIEUx/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAINGUYENLIEU lOAINGUYENLIEU = db.LOAINGUYENLIEUx.Find(id);
            if (lOAINGUYENLIEU == null)
            {
                return HttpNotFound();
            }
            return View(lOAINGUYENLIEU);
        }

        // POST: Admin/LOAINGUYENLIEUx/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLNL,TenLNL")] LOAINGUYENLIEU lOAINGUYENLIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAINGUYENLIEU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAINGUYENLIEU);
        }

        // GET: Admin/LOAINGUYENLIEUx/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAINGUYENLIEU lOAINGUYENLIEU = db.LOAINGUYENLIEUx.Find(id);
            if (lOAINGUYENLIEU == null)
            {
                return HttpNotFound();
            }
            return View(lOAINGUYENLIEU);
        }

        // POST: Admin/LOAINGUYENLIEUx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOAINGUYENLIEU lOAINGUYENLIEU = db.LOAINGUYENLIEUx.Find(id);
            db.LOAINGUYENLIEUx.Remove(lOAINGUYENLIEU);
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
