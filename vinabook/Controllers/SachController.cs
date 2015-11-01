using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;

using PagedList.Mvc;
using PagedList;

namespace Vinabook.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Saches.Where(n => n.Moi == 1).Take(10).ToList();
            return PartialView(lstSachMoi);
        }
        /// <summary>
        /// Sach moi tren menu
        /// </summary>
        /// <returns></returns>
        public ActionResult Sach(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            return View(db.Saches.Where(n => n.Moi == 1).ToList().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
        }
        public ViewResult SachTheoChuDe(int machude = 1, int? page = 1)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            var lstSach = db.Saches.Where(n => n.MaChuDe == machude).ToList().OrderBy(n => n.MaChuDe).ToPagedList(pageNumber, pageSize);
            ViewBag.TenChuDe = db.ChuDes.Single(n => n.MaChuDe == machude).TenChuDe;
            return View(lstSach);
        }
        public ViewResult SachTheoNhaXuatBan(int manxb = 1, int? page = 1)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;

            ViewBag.NhaXuatBan = db.NhaXuatBans.Single(n => n.MaNXB == manxb).TenNXB;
            var lstSach = db.Saches.Where(n => n.MaNXB == manxb).ToList().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize);
            return View(lstSach);
        }
        public PartialViewResult SachTiengAnhPartial()
        {
            int MaChuDe = 2;
            var lstSachAV = db.Saches.Where(n => n.MaChuDe == MaChuDe).Take(3).ToList();
            ViewBag.MaChuDe = MaChuDe;
            return PartialView(lstSachAV);
        }
        public PartialViewResult SachITPartial()
        {
            int MaChuDe = 1;
            var lstSachIT = db.Saches.Where(n => n.MaChuDe == MaChuDe).Take(3).ToList();
            ViewBag.MaChuDe = MaChuDe;
            return PartialView(lstSachIT);
        }
        public PartialViewResult SachPhatGiaoPartial()
        {
            int MaChuDe = 3;
            var lstSachPG = db.Saches.Where(n => n.MaChuDe == MaChuDe).Take(3).ToList();
            ViewBag.MaChuDe = MaChuDe;
            return PartialView(lstSachPG);
        }
        public ViewResult XemChiTiet(int MaSach = 0)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                //Trả về trang báo lỗi 
                Response.StatusCode = 404;
                return null;
            }
            //ChuDe cd = db.ChuDes.Single(n => n.MaChuDe == sach.MaChuDe);
            //ViewBag.TenCD = cd.TenChuDe;
            ViewBag.TenChuDe = db.ChuDes.Single(n => n.MaChuDe == sach.MaChuDe).TenChuDe;
            ViewBag.NhaXuatBan = db.NhaXuatBans.Single(n => n.MaNXB == sach.MaNXB).TenNXB;
            return View(sach);
        }
        public PartialViewResult SachCungChuDePartial()
        {

            return PartialView();
        }
        public PartialViewResult SachGanDayPartial()
        {
            var lstSachMoi = db.Saches.OrderBy(x => x.NgayCapNhat).ToList();
            return PartialView(lstSachMoi);
        }
        [HttpPost]
        public JsonResult AddToCart(int? id, int chiTietSl)
        {
            List<CartItem> listCartItem;
            //Process Add To Cart
            if (Session["ShoppingCart"] == null)
            {
                //Create New Shopping Cart Session
                listCartItem = new List<CartItem>();
                listCartItem.Add(new CartItem { Quality = chiTietSl, productOrder = db.Saches.Find(id) });
                Session["ShoppingCart"] = listCartItem;
            }
            else
            {
                bool flag = false;
                listCartItem = (List<CartItem>)Session["ShoppingCart"];
                foreach (CartItem item in listCartItem)
                {
                    if (item.productOrder.MaSach == id)
                    {
                        item.Quality += chiTietSl;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    listCartItem.Add(new CartItem { Quality = chiTietSl, productOrder = db.Saches.Find(id) });
                Session["ShoppingCart"] = listCartItem;
            }

            //Count item in shopping cart
            int cartcount = 0;
            List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            foreach (CartItem item in ls)
            {
                cartcount += item.Quality;
            }
            return Json(new { ItemAmount = cartcount });
        }
    }
}