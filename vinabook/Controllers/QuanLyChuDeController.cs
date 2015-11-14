using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vinabook.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
namespace Vinabook.Controllers
{
    [Authorize]
    public class QuanLyChuDeController : Controller
    {
        // GET: QuanLyChuDe
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.ChuDes.ToList().OrderBy(n => n.TenChuDe).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult IndexPartial(int ? page)
        {
            int pageNumber = ( page ?? 1);
            int pageSize = 10;
            return PartialView(db.ChuDes.ToList().OrderBy(n => n.TenChuDe).ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create(ChuDe cd)
        {
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                db.ChuDes.Add(cd);
                db.SaveChanges();
                ViewBag.ThongBao = "Thêm mới thành công";
            }
            else {
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
        public ActionResult Edit(int MaCD)
        {
            //Lấy ra đối tượng sách theo mã 
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaCD);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
            return View(cd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ChuDe cd, FormCollection f)
        {
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                //Thực hiện cập nhận trong model
                db.Entry(cd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        
        /// <summary>
        /// Hien thi
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        public ActionResult Details(int MaCD)
        {

            //Lấy ra đối tượng sách theo mã 
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaCD);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(cd);

        }

        [HttpPost]
        public JsonResult XemCTCD(int macd)
        {
            TempData["macd"] = macd;
            return Json(new { Url = Url.Action("XemCTCDPartial") });
        }
        public PartialViewResult XemCTCDPartial()
        {
            int maCD = (int)TempData["macd"];
            var lstCD = db.ChuDes.Where(n => n.MaChuDe == maCD).ToList();
            return PartialView(lstCD);
        }
        /// <summary>
        /// Xoa
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int MaCD)
        {
            //Lấy ra đối tượng sách theo mã 
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaCD);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(cd);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(int MaCD)
        {
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaCD);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.ChuDes.Remove(cd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /////////////////////
        [HttpPost]
        public JsonResult Remove(int id)
        {
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.ChuDes.Remove(cd);
            db.SaveChanges();
            return Json(new { Url = Url.Action("IndexPartial") });
        }

    }
}