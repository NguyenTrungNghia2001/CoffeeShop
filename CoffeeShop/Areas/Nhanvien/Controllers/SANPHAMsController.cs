using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using CoffeeShop.Controllers;
using CoffeeShop.Models;



namespace CoffeeShop.Areas.Nhanvien.Controllers
{
    public class SANPHAMsController : Controller
    {
        private QLCFEntities db = new QLCFEntities();

        // GET: Admin/SANPHAMs
        public ActionResult Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.CUAHANG).Include(s => s.LOAISP1).Include(s => s.NGUYENLIEU);
            return View(sANPHAMs.ToList());
        }

        // GET: Admin/SANPHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DiaChiCH = new SelectList(db.CUAHANGs, "DiaChiCH", "DiaChiCH");
            ViewBag.LOAISP = new SelectList(db.LOAISPs, "MALSP", "TENLSP");
            ViewBag.IDNL = new SelectList(db.NGUYENLIEUx, "IDNL", "TENNL");
            return View();
        }

        // POST: Admin/SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "MASP,TENSP,LOAISP,IDNL,SLUONG,HINHANH,ImageFile,GIASP,DiaChiCH,Mota")] SANPHAM sANPHAM)
        {
            string filename = Path.GetFileNameWithoutExtension(sANPHAM.ImageFile.FileName);
            string extension = Path.GetExtension(sANPHAM.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
         
            
            sANPHAM.HINHANH = "~/img/" + filename;
            
            
            filename = Path.Combine(Server.MapPath("~/img/"), filename);
            sANPHAM.ImageFile.SaveAs(filename);
            if (ModelState.IsValid)
            {
                
                db.SANPHAMs.Add(sANPHAM);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiaChiCH = new SelectList(db.CUAHANGs, "DiaChiCH", "DiaChiCH", sANPHAM.DiaChiCH);
            ViewBag.LOAISP = new SelectList(db.LOAISPs, "MALSP", "TENLSP", sANPHAM.LOAISP);
            ViewBag.IDNL = new SelectList(db.NGUYENLIEUx, "IDNL", "TENNL", sANPHAM.IDNL);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiaChiCH = new SelectList(db.CUAHANGs, "DiaChiCH", "DiaChiCH", sANPHAM.DiaChiCH);
            ViewBag.LOAISP = new SelectList(db.LOAISPs, "MALSP", "TENLSP", sANPHAM.LOAISP);
            ViewBag.IDNL = new SelectList(db.NGUYENLIEUx, "IDNL", "TENNL", sANPHAM.IDNL);
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "MASP,TENSP,LOAISP,IDNL,SLUONG,HINHANH,GIASP,DiaChiCH,Mota")] SANPHAM sANPHAM, HttpPostedFileBase imgFile)
        {
            /*string path = UploadImage(imgFile);*/
            if (ModelState.IsValid)
            {
               /* sANPHAM.HINHANH = path;*/
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiaChiCH = new SelectList(db.CUAHANGs, "DiaChiCH", "DiaChiCH", sANPHAM.DiaChiCH);
            ViewBag.LOAISP = new SelectList(db.LOAISPs, "MALSP", "TENLSP", sANPHAM.LOAISP);
            ViewBag.IDNL = new SelectList(db.NGUYENLIEUx, "IDNL", "TENNL", sANPHAM.IDNL);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
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
        /*public string UploadImage(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/DATA/"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/DATA/" + random + Path.GetFileName(file.FileName);
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
        }*/
    }
}
