using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;

namespace Vinabook.Controllers
{
    public class GioHangController : Controller
    {

        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public List<CartItem> LayGioHang()
        {
            List<CartItem> lstGioHang = Session["ShoppingCart"] as List<CartItem>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<CartItem>();
                Session["ShoppingCart"] = lstGioHang;
            }
            return lstGioHang;
        }

        /*
        public ActionResult ThemGioHang(int iMaSach, string strUrl)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<CartItem> lstGioHang = LayGioHang();
            CartItem sanpham = lstGioHang.Find(n => n.productOrder.MaSach == iMaSach);
            if (sanpham == null)
            {
                sanpham = new CartItem();
                lstGioHang.Add(sanpham);
                return Redirect(strUrl);
            }
            else
            {
                sanpham.iSoLuong++;

                return Redirect(strUrl);
            }
        }

        public ActionResult CapNhapGioHang(int iMaSP, FormCollection f)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<CartItem> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (sanpham == null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return Redirect("GioHang");
        }

        public ActionResult XoaGioHang(int iMaSP)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CartItem> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (sanpham == null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSP);
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        */
        public ActionResult Index()
        {
            if (Session["ShoppingCart"] == null)
            {
                return RedirectToAction("Index", "Home");
                //return View();
            }
            List<CartItem> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        //public int TongSoLuong()
        //{
        //    int iTongSoLuong = 0;
        //    List<CartItem> lstGioHang = Session["ShoppingCart"] as List<CartItem>;
        //    if (lstGioHang != null)
        //    {
        //        iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);

        //    }
        //    return iTongSoLuong;
        //}

        //public double TongTien()
        //{
        //    double dTongTien = 0;
        //    List<CartItem> lstGioHang = Session["ShoppingCart"] as List<CartItem>;
        //    if (lstGioHang != null)
        //    {
        //        dTongTien = lstGioHang.Sum(n => n.dThanhTien);
        //    }
        //    return dTongTien;
        //}

        //public ActionResult TongSlGioHangPartial()
        //{
        //    if (TongSoLuong() == 0)
        //    {
        //        ViewBag.TongSoLuong = 0;
        //        return PartialView();
        //    }
        //    ViewBag.TongSoLuong = TongSoLuong();

        //    return PartialView();
        //}
        //public ActionResult TongTienGioHangPartial()
        //{
        //    if (TongSoLuong() == 0)
        //    {
        //        ViewBag.TongTien = 0;
        //        return PartialView();
        //    }

        //    ViewBag.TongTien = TongTien();
        //    return PartialView();
        //}

        //#region Đặt hàng
        ////Xây dựng chức năng đặt hàng
        //[HttpPost]
        //public ActionResult DatHang()
        //{
        //    ViewBag.KiemTra = 0;
        //    //Kiểm tra đăng đăng nhập
        //    if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
        //    {
        //        return RedirectToAction("Login", "NGuoiDung");
        //    }
        //    //Kiểm tra giỏ hàng
        //    if (Session["ShoppingCart"] == null)
        //    {
        //        RedirectToAction("Index", "Home");
        //    }
        //    //Thêm đơn hàng
        //    DonHang ddh = new DonHang();
        //    KhachHang kh = (KhachHang)Session["TaiKhoan"];
        //    List<CartItem> gh = LayGioHang();
        //    ddh.MaKH = kh.MaKH;
        //    ddh.NgayDat = DateTime.Now;
        //    db.DonHangs.Add(ddh);
        //    db.SaveChanges();
        //    //Thêm chi tiết đơn hàng
        //    foreach (var item in gh)
        //    {
        //        ChiTietDonHang ctDH = new ChiTietDonHang();
        //        ctDH.MaDonHang = ddh.MaDonHang;
        //        ctDH.MaSach = item.iMaSach;
        //        ctDH.SoLuong = item.iSoLuong;
        //        ctDH.DonGia = (decimal)item.dDongia;
        //        db.ChiTietDonHangs.Add(ctDH);
        //    }
        //    db.SaveChanges();
        //    //xoa gio hang khi da them thanh cong
        //    Session["ShoppingCart"] = null;
        //    //   return JavaScript("DatHangThanhCong");
        //    ViewBag.KiemTra = 1;
        //    return RedirectToAction("Index", "Home");
        //}
        //#endregion
        public ActionResult GioHangPartial()
        {
            int cartcount = 0;
            if (Session["ShoppingCart"] != null)
            {
                List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
                foreach (CartItem item in ls)
                {
                    cartcount += item.Quality;
                }
            }
            ViewBag.count = cartcount;


            return PartialView();
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DatHang(DonHang dh)
        {
            if (Session["ShoppingCart"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.DonHangs.Add(dh);
                    db.SaveChanges();
                }
                List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
                foreach (CartItem item in ls)
                {
                    ChiTietDonHang ct = new ChiTietDonHang();
                    ct.MaDonHang = dh.MaDonHang;
                    ct.MaSach = item.productOrder.MaSach;
                    ct.SoLuong = item.Quality;
                    ct.DonGia = item.productOrder.GiaBan;
                    db.ChiTietDonHangs.Add(ct);
                    db.SaveChanges();
                    Session["ShoppingCart"] = null;
                }
            }
            return View();
        }
        //public ActionResult DatHang(DonHang dh, ChiTietDonHang ct)
        //{
        //    if (Session["ShoppingCart"] != null)
        //    {
        //        if (Session["TaiKhoan"] != null])
        //        {
        //            List<KhachHang> customer = (List<KhachHang>)Session["TaiKhoan"];
        //            foreach (KhachHang item2 in customer)
        //                if (ModelState.IsValid)
        //                {
        //                    dh.MaKH = item2.MaKH;
        //                    db.DonHangs.Add(dh);
        //                    db.SaveChanges();
        //                }
        //        }

        //        List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
        //        foreach (CartItem item in ls)
        //        {

        //            ct.MaDonHang = dh.MaDonHang;
        //            ct.MaSach = item.productOrder.MaSach;
        //            ct.SoLuong = item.Quality;
        //            ct.DonGia = item.productOrder.GiaBan;
        //            db.ChiTietDonHangs.Add(ct);
        //            db.SaveChanges();
        //            Session["ShoppingCart"] = null;
        //        }
        //    }
        //    return View();
        //}

    }
}