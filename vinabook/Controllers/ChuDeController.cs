using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;

namespace Vinabook.Controllers
{
    //[Authorize]
    public class ChuDeController : Controller
    {
        // GET: ChuDe
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult DanhMucChuDePartial()
        {
            //Lấy ra chủ đề đầu tiên trong csdl
            int MaChuDe = int.Parse(db.ChuDes.ToList().ElementAt(0).MaChuDe.ToString());
            //Tạo 1 viewbag gán sách theo chủ đề đầu tiên trong csdl
            ViewBag.SachTheoChuDe = db.Saches.Where(n => n.MaChuDe == MaChuDe).ToList();
            
            return PartialView(db.ChuDes.ToList());
        }
    }
}