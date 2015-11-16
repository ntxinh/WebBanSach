using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;

namespace Vinabook.Controllers
{
    //[Authorize]
    public class NhaXuatbanController : Controller
    {
        // GET: NhaXuatban
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult DanhMucNhaXuatBanPartial()
        {
            //Lấy ra chủ đề đầu tiên trong csdl
            int MaNXB = int.Parse(db.NhaXuatBans.ToList().ElementAt(0).MaNXB.ToString());
            //Tạo 1 viewbag gán sách theo chủ đề đầu tiên trong csdl
            ViewBag.SachTheoNXB = db.Saches.Where(n => n.MaNXB == MaNXB).ToList();

            return PartialView(db.NhaXuatBans.ToList());
        }
    }
}