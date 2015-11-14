using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vinabook.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Security;

namespace Vinabook.Controllers
{
    [Authorize]
    public class QuanLyKHController : Controller
    {
        // GET: QuanLyKH
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.KhachHangs.ToList().OrderBy(n => n.HoTen).ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// Tao moi
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KhachHang kh)
        {
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                ViewBag.ThongBao = "Thêm mới thành công";
            }
            else
            {
                ViewBag.ThongBao = "Thêm mới thất bại";
            }
            return View();
        }
        /// <summary>
        /// Chinh Sua
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int MaKH)
        {
            //Lấy ra đối tượng sách theo mã 
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.GioiTinh = kh.GioiTinh;
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(KhachHang kh, FormCollection f)
        {
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                //Thực hiện cập nhận trong model
                db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }
        /// <summary>
        /// Hien thi
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        public ActionResult Details(int MaKH)
        {

            //Lấy ra đối tượng sách theo mã 
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(kh);

        }

        [HttpPost]
        public JsonResult XemCTKH(int makh)
        {
            TempData["makh"] = makh;
            return Json(new { Url = Url.Action("XemCTKHPartial") });
        }
        public PartialViewResult XemCTKHPartial()
        {
            int maKH = (int)TempData["makh"];
            var lstKH = db.KhachHangs.Where(n => n.MaKH == maKH).ToList();
            return PartialView(lstKH);
        }
        /// <summary>
        /// Xoa
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int MaKH)
        {
            //Lấy ra đối tượng sách theo mã 
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(kh);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(int MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KhachHangs.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
       
    }
}