using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;
using PagedList;
using PagedList.Mvc;

namespace Vinabook.Controllers
{
    [Authorize]
    public class QLDonHangController : Controller
    {
        // GET: QLDonHang
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.DonHangs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Delete(int? MaDH)
        {
            //Lấy ra đối tượng sách theo mã 
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDH);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(dh);
        }
        [HttpPost]
        public ActionResult Delete(int MaDH)
        {
            //Xoa chi tiet don hang
            ChiTietDonHang ct = db.ChiTietDonHangs.SingleOrDefault(n => n.MaDonHang == MaDH);
            if (ct == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.ChiTietDonHangs.Remove(ct);
            db.SaveChanges();

            //Xoa don hang
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDH);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DonHangs.Remove(dh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}