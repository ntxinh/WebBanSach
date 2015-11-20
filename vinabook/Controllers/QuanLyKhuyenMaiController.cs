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
    public class QuanLyKhuyenMaiController : Controller
    {
        // GET: QuanLyKhuyenMai
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.KhuyenMais.ToList().OrderBy(n => n.MaKM).ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create(KhuyenMai nxb)
        {
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                db.KhuyenMais.Add(nxb);
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
        public ActionResult Edit(string MaNXB)
        {
            //Lấy ra đối tượng sách theo mã 
            KhuyenMai nxb = db.KhuyenMais.SingleOrDefault(n => n.MaKM == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.DaSuDung = nxb.DaSuDung.ToString();
            return View(nxb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(KhuyenMai nxb, FormCollection f)
        {
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                //Thực hiện cập nhận trong model
                db.Entry(nxb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }
        /// <summary>
        /// Hien thi
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        public ActionResult Details(string MaNXB)
        {

            //Lấy ra đối tượng sách theo mã 
            KhuyenMai nxb = db.KhuyenMais.SingleOrDefault(n => n.MaKM == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(nxb);

        }
        /// <summary>
        /// Xoa
        /// </summary>
        /// <param name="MaSach"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(string MaNXB)
        {
            //Lấy ra đối tượng sách theo mã 
            KhuyenMai nxb = db.KhuyenMais.SingleOrDefault(n => n.MaKM == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(nxb);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(string MaNXB)
        {
            KhuyenMai nxb = db.KhuyenMais.SingleOrDefault(n => n.MaKM == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KhuyenMais.Remove(nxb);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}