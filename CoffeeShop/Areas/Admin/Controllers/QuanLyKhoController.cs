using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using CoffeeShop.Controllers;
using CoffeeShop.Models;
using PagedList;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class QuanLyKhoController : Controller
    {
        private QLCFEntities db = new QLCFEntities();

        public ActionResult HOADONNHAPXUAT(int? page)
        {
            var item = db.HOADONNHAPXUATs.OrderByDescending(x => x.NGAYXUAT).ToList();
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
        public ActionResult NGUYENLIEUKHO()
        {

            var nl = db.NGUYENLIEUKHOes.ToList();
           

            return View(nl);
        }
        public List<NLXUAT> LayNL()
        {
            List<NLXUAT> gioHang = Session["NLX"] as List<NLXUAT>;
            if (gioHang == null)
            {
                gioHang = new List<NLXUAT>();
                Session["NLX"] = gioHang;
            }
            return gioHang;
        }
        public ActionResult HienThiNHAPXUAT(int id)
        {
            var item = db.HOADONNHAPXUATs.Find(id);
            return View(item);
        }
        public ActionResult ThemSanPhamVaoHD(int IdNLKHO)
        {
            List<NLXUAT> gioHang = LayNL();
            NLXUAT sanPham = gioHang.FirstOrDefault(s => s.IdNLKHO == IdNLKHO);
            if (sanPham == null)
            {
                sanPham = new NLXUAT(IdNLKHO);
                gioHang.Add(sanPham);
            }
            else
            {
                sanPham.Quantity++;
            }
            return RedirectToAction("NGUYENLIEUKHO", "QuanLyKho");
        }
        private double TinhTongSL()
        {
            double tongSL = 0;
            List<NLXUAT> gioHang = LayNL();
            if (gioHang != null)
                tongSL = gioHang.Sum(n => n.Quantity);
            return tongSL;
        }
        public ActionResult HienThiGioHang()
        {
            List<NLXUAT> gioHang = LayNL();
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("GioHangRong", "QuanLyKho");
            }
            ViewBag.TongSL = TinhTongSL();
            return View(gioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TinhTongSL();
            return PartialView();
        }
        public ActionResult XoaMatHang(int IdNLKHO)
        {
            List<NLXUAT> gioHang = LayNL();
            var sanpham = gioHang.FirstOrDefault(s => s.IdNLKHO == IdNLKHO);
            if (sanpham != null)
            {
                gioHang.RemoveAll(s => s.IdNLKHO == IdNLKHO);
                return RedirectToAction("HienThiGioHang");
            }
            if (gioHang.Count == 0)
                return RedirectToAction("GioHangRong");
            return RedirectToAction("HienThiGioHang");
        }
        public ActionResult CapNhatMatHang(int IdNLKHO, int SoLuong)
        {
            List<NLXUAT> gioHang = LayNL();
            var sanpham = gioHang.FirstOrDefault(s => s.IdNLKHO == IdNLKHO);
            if (sanpham != null)
            {
                sanpham.Quantity = SoLuong;
            }
            return RedirectToAction("HienThiGioHang");
        }
      
        public ActionResult GioHangRong()
        {
            return View();
        }
        public ActionResult DatHang(int? id, int? idkho)
        {

            CUAHANG cuahang = new CUAHANG();
            KHOHANG kho = new KHOHANG();    
            List<NLXUAT> gioHang = LayNL();
            if (gioHang == null || gioHang.Count == 0)
                return RedirectToAction("HOADONNHAPXUAT", "QuanLyKho");
            ViewBag.TongSL = TinhTongSL();
            ViewBag.IDCH = new SelectList(db.CUAHANGs, "DiaChiCH", "DiaChiCH", cuahang.DiaChiCH);
            ViewBag.IDKHO = new SelectList(db.KHOHANGs, "IDKHO", "IDKHO", kho.IDKHO);
            return View(gioHang);
        }

        QLCFEntities database = new QLCFEntities();
        public ActionResult DongYXuatHang([Bind(Include = "IDHD,IDKHO,DiaChiCH,NGAYXUAT,TONGSOLUONG,NHAPXUAT")] HOADONNHAPXUAT DonHang)
        {
            List<NLXUAT> gioHang = LayNL();
            DonHang.NGAYXUAT = DateTime.Now;
            DonHang.TONGSOLUONG = (int)TinhTongSL();
            DonHang.NHAPXUAT = false;
            database.HOADONNHAPXUATs.Add(DonHang);
            database.SaveChanges();


            
            foreach (var sanpham in gioHang)
            {
                CHITIETHOADONNHAPXUAT chitiet = new CHITIETHOADONNHAPXUAT();
                
                chitiet.IDHD = DonHang.IDHD;
                chitiet.IDNLKHO = sanpham.IdNLKHO;
                chitiet.SOLUONG = sanpham.Quantity;
                var nlieukho = database.NGUYENLIEUKHOes.FirstOrDefault(s => s.IDNLKHO == sanpham.IdNLKHO);
                if(nlieukho != null)
                {
                    nlieukho.SOLUONGTON -= sanpham.Quantity;
                    database.Entry(nlieukho).State = EntityState.Modified;
                    
                }

                database.CHITIETHOADONNHAPXUATs.Add(chitiet);
               
            }
        
           
            database.SaveChanges();
            /* if(nlieu.IDNLKHO == chitiet.IDNLKHO) {

                 nlieu.SOLUONGTON -= chitiet.SOLUONG;
                 database.Entry(nlieu).State = EntityState.Added;
                 database.SaveChanges();
             }*/





            Session["NLX"] = null;
            return RedirectToAction("NGUYENLIEUKHO","QuanLyKho");
        }
     
        public void CapNhatNLK([Bind(Include = "SOLUONGTON")] NGUYENLIEUKHO nlieu)
        {
            List<NLXUAT> gioHang = LayNL();
            foreach(var sanpham in gioHang)
            {
                if(sanpham.IdNLKHO == nlieu.IDNLKHO)
                {
                    nlieu.SOLUONGTON -= sanpham.Quantity;
                    database.Entry(nlieu).State = EntityState.Modified;
                }
            }
            database.SaveChanges();
        }

        public ActionResult HoanThanhDonHang()
        {
            return View();
        }
      
    }
}