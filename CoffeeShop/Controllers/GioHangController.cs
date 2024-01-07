using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeShop.Models;
using PayPal.Api;

namespace CoffeeShop.Controllers
{

    public class GioHangController : Controller
    {
        // GET: GioHang
        private QLCFEntities db = new QLCFEntities();   
        public ActionResult Index()
        {
            return View();
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
        public ActionResult ThemSanPhamVaoGio(int MaSP)
        {
            List<MatHangMua> gioHang = LayGioHang();
            MatHangMua sanPham = gioHang.FirstOrDefault(s => s.MaSP == MaSP);
            if (sanPham == null)
            {
                sanPham = new MatHangMua(MaSP);
                gioHang.Add(sanPham);
            }
            else
            {
                sanPham.Quantity++;
            }
            return RedirectToAction("Detail", "Home", new { id = MaSP });
        }
        private double TinhTongSL()
        {
            double tongSL = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
                tongSL = gioHang.Sum(n => n.Quantity);
            return tongSL;
        }
        private double TinhTongTien()
        {
            double TongTien = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
                TongTien = gioHang.Sum(n => n.ThanhTien());
            return TongTien;
        }
        private double TinhTongTienKM(double giamgia)
        {
            double TongTien = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
                TongTien = gioHang.Sum(n => n.ThanhTien()*giamgia);
            return TongTien;
        }
        public ActionResult HienThiGioHang()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("GioHangRong", "GioHang");
            }
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }
        public ActionResult HienThiGioHangSauTT()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("GioHangRong", "GioHang");
            }
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }
        public ActionResult XoaMatHang(int MaSP)
        {
            List<MatHangMua> gioHang = LayGioHang();
            var sanpham = gioHang.FirstOrDefault(s => s.MaSP == MaSP);
            if (sanpham != null)
            {
                gioHang.RemoveAll(s => s.MaSP == MaSP);
                return RedirectToAction("HienThiGioHang");
            }
            if (gioHang.Count == 0)
                return RedirectToAction("GioHangRong");
            return RedirectToAction("HienThiGioHang");
        }
        public ActionResult CapNhatMatHang(int MaSP, int SoLuong)
        {
            List<MatHangMua> gioHang = LayGioHang();
            var sanpham = gioHang.FirstOrDefault(s => s.MaSP == MaSP);
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

        public ActionResult DatHang()
        {

              
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
                return RedirectToAction("Menu", "Home");
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }

        QLCFEntities database = new QLCFEntities();
        public ActionResult DongYDatHang([Bind(Include = "IDKH,NGAYDAT,Trigia,Dagiao,TenNguoiNhan,AddressDeliverry,DienThoaiNhan,HTThanhToan,TTrang,HTGiaoHang,GIAMGIA")] GIOHANG DonHang)
        {
            KHACHHANG cus = Session["TaiKhoan"] as KHACHHANG;
            List<MatHangMua> gioHang = LayGioHang();
            
            if (DonHang.GIAMGIA == null)

            {

                DonHang.IDKH = cus.IDKH;
                DonHang.NGAYDAT = DateTime.Now;
                DonHang.Trigia = (int)(decimal)TinhTongTien();
                DonHang.Dagiao = false;
                DonHang.TenNguoiNhan = cus.TenKH;
                DonHang.AddressDeliverry = cus.DiaChiKH;
                DonHang.DienThoaiNhan = cus.SDTKH;
                DonHang.HTGiaoHang = 1;
                DonHang.TTrang = true;

                database.GIOHANGs.Add(DonHang);
                database.SaveChanges();



                foreach (var sanpham in gioHang)
                {
                    CHITIETGIOHANG chitiet = new CHITIETGIOHANG();
                    chitiet.IDGH = DonHang.ID;
                    chitiet.MASP = sanpham.MaSP;
                    chitiet.SOLUONG = sanpham.Quantity;
                    chitiet.UnitPrice = (int)(decimal)sanpham.GIASP;
                    database.CHITIETGIOHANGs.Add(chitiet);

                }
                database.SaveChanges();
            }
            else
            {
                var km = database.KHUYENMAIs.FirstOrDefault(n => n.IDKM == DonHang.GIAMGIA);
                DonHang.IDKH = cus.IDKH;
                DonHang.NGAYDAT = DateTime.Now;
                DonHang.Trigia = (int)(decimal)TinhTongTienKM(km.GIAMGIA);
                DonHang.Dagiao = false;
                DonHang.TenNguoiNhan = cus.TenKH;
                DonHang.AddressDeliverry = cus.DiaChiKH;
                DonHang.DienThoaiNhan = cus.SDTKH;
                DonHang.HTGiaoHang = 1;
                DonHang.TTrang = true;

                database.GIOHANGs.Add(DonHang);
                database.SaveChanges();



                foreach (var sanpham in gioHang)
                {
                    CHITIETGIOHANG chitiet = new CHITIETGIOHANG();
                    chitiet.IDGH = DonHang.ID;
                    chitiet.MASP = sanpham.MaSP;
                    chitiet.SOLUONG = sanpham.Quantity;
                    chitiet.UnitPrice = (int)(decimal)sanpham.GIASP;
                    database.CHITIETGIOHANGs.Add(chitiet);

                }
                database.SaveChanges();
            }

            Session["GioHang"] = null;
            return RedirectToAction("HoanThanhDonHang");

        }

        public ActionResult DongYDatHangSAUTT()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
                return RedirectToAction("Menu", "Home");
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }


        public ActionResult DongYDatHangKDN([Bind(Include = "IDKH,NGAYDAT,Trigia,Dagiao,TenNguoiNhan,AddressDeliverry,DienThoaiNhan,HTThanhToan,TTrang,HTGiaoHang")] GIOHANG DonHang )
        {
            List<MatHangMua> gioHang = LayGioHang();
            
       
                DonHang.IDKH = null;
                DonHang.NGAYDAT = DateTime.Now;
                DonHang.Trigia = (int)(decimal)TinhTongTien();
                DonHang.Dagiao = false;
                DonHang.HTThanhToan = 1;
                DonHang.HTGiaoHang = 1;
                DonHang.TTrang = true;
            Session["Don"] = DonHang;
            database.GIOHANGs.Add(DonHang);
                database.SaveChanges();
              

            foreach (var sanpham in gioHang)
            {
                CHITIETGIOHANG chitiet = new CHITIETGIOHANG();
                chitiet.IDGH = DonHang.ID;
                chitiet.MASP = sanpham.MaSP;
                chitiet.SOLUONG = sanpham.Quantity;
                chitiet.UnitPrice = (int)(decimal)sanpham.GIASP;
                database.CHITIETGIOHANGs.Add(chitiet);

            }
            
            database.SaveChanges();

            return RedirectToAction("HoanThanhDonHang");
        }


        public ActionResult HoanThanhDonHang()
        {
            return View();
        }
        public ActionResult HienThiDonHang(int id)
        {
            var item = db.HOADONNHAPXUATs.Find(id);
            return View(item);
        }
      
        public ActionResult HienThiDonHangKTK()
        {
            GIOHANG nguoiDung = (GIOHANG)Session["Don"];
            return View(database.GIOHANGs.Where(s => s.ID == nguoiDung.ID).FirstOrDefault());


        }
        public ActionResult SuccessView()
        {
            return View();  
        }
        public ActionResult FailureView()
        {
            return View();
        }


        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/GioHang/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return RedirectToAction("HienThiGioHangSauTT");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            KHACHHANG cus = Session["TaiKhoan"] as KHACHHANG;
            List<MatHangMua> gioHang = LayGioHang();
           
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            foreach (var item in gioHang)
            {
                itemList.items.Add(new Item()
                {
                    name = item.TenSP,
                    currency = "USD",
                    price = Math.Round(item.GIASP / 23500,2).ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = item.MaSP.ToString(), 
                });
            }
            //Adding Item Details like name, currency, price etc  

            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = gioHang.Sum(n => Math.Round(n.ThanhTien() / 23500, 2)).ToString(),
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = gioHang.Sum(n => Math.Round(n.ThanhTien() / 23500, 2)).ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }





    }
}

