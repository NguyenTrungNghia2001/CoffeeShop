using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoffeeShop.Controllers;
using CoffeeShop.Models;

namespace CoffeeShop.Areas.Nhanvien.Controllers
{
    public class TINTUCsController : Controller
    {
        private QLCFEntities db = new QLCFEntities();

        // GET: Admin/TINTUCs
        public ActionResult Index()
        {
            return View(db.TINTUCs.ToList());
        }

        // GET: Admin/TINTUCs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TINTUC tINTUC = db.TINTUCs.Find(id);
            if (tINTUC == null)
            {
                return HttpNotFound();
            }
            return View(tINTUC);
        }

        // GET: Admin/TINTUCs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TINTUCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IDTINTUC,TIEUDE,HinhAnhTieuDe,ImageFile,NOIDUNG")] TINTUC tINTUC)
        {
            string filename = Path.GetFileNameWithoutExtension(tINTUC.ImageFile.FileName);
            string extension = Path.GetExtension(tINTUC.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;


            tINTUC.HinhAnhTieuDe = "~/img/" + filename;


            filename = Path.Combine(Server.MapPath("~/img/"), filename);
            tINTUC.ImageFile.SaveAs(filename);
            if (ModelState.IsValid)
            {
                
                db.TINTUCs.Add(tINTUC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tINTUC);
        }

        // GET: Admin/TINTUCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TINTUC tINTUC = db.TINTUCs.Find(id);
            if (tINTUC == null)
            {
                return HttpNotFound();
            }
            return View(tINTUC);
        }

        // POST: Admin/TINTUCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "IDTINTUC,HinhAnhTieuDe,TIEUDE,NOIDUNG,NGAYDANG")] TINTUC tINTUC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tINTUC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tINTUC);
        }

        // GET: Admin/TINTUCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TINTUC tINTUC = db.TINTUCs.Find(id);
            if (tINTUC == null)
            {
                return HttpNotFound();
            }
            return View(tINTUC);
        }

        // POST: Admin/TINTUCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TINTUC tINTUC = db.TINTUCs.Find(id);
            db.TINTUCs.Remove(tINTUC);
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
