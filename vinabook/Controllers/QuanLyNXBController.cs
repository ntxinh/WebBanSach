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
    public class QuanLyNXBController : Controller
    {
        // GET: QuanLyNXB
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB).ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create(NhaXuatBan nxb)
        {
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                db.NhaXuatBans.Add(nxb);
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
        public ActionResult Edit(int MaNXB)
        {
            //Lấy ra đối tượng sách theo mã 
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(nxb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NhaXuatBan nxb, FormCollection f)
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
        public ActionResult Details(int MaNXB)
        {

            //Lấy ra đối tượng sách theo mã 
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
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
        public ActionResult Delete(int MaNXB)
        {
            //Lấy ra đối tượng sách theo mã 
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(nxb);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(int MaNXB)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhaXuatBans.Remove(nxb);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}