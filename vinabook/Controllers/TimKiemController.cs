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
    public class TimKiemController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        //[HttpPost]
        //public ActionResult TimKiem()
        //{
        //    return Json(new { Url = Url.Action("Index") });
        //}
        //// GET: TimKiem

        public ActionResult Index(string Key, int? page)
        {
            ViewBag.Key = Key;
            
          //  List<Sach> lsKQTK = db.Saches.Where(m => m.TenSach.Contains(Key)).ToList();           
            List<Sach> lsKQTK = db.Saches.Where(n => n.TenSach.Contains(Key) || n.ChuDe.TenChuDe.Contains(Key) || n.NhaXuatBan.TenNXB.Contains(Key)).ToList();
            ViewBag.TuKhoa = Key;
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            if (lsKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy";
                return View(db.Saches.OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));

            }
            ViewBag.ThongBao = "Đã tìm thấy " + lsKQTK.Count + " kết quả";
            

             return View(lsKQTK.OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
            //return PartialView("TimKiemPartial");
        }
        [HttpPost]
        public ActionResult Index(FormCollection f, int? page)//string txtTimKiem,int ?page)
        {
            string Key = f["txtTimKiem"].ToString();
            //List<Sach> lsKQTK = db.Saches.Where(m => m.TenSach.Contains(Key)).ToList();
            List<Sach> lsKQTK = db.Saches.Where(n => n.TenSach.Contains(Key) || n.ChuDe.TenChuDe.Contains(Key) || n.NhaXuatBan.TenNXB.Contains(Key)).ToList();
            ViewBag.Key = Key;
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            if (lsKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy";
                return View(db.Saches.OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));

            }
            ViewBag.ThongBao = "Đã tìm thấy " + lsKQTK.Count + " kết quả";
           

             return View(lsKQTK.OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
            //return PartialView("TimKiemPartial");
        }
    }
}