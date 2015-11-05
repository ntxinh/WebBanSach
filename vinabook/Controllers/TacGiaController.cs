using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;

namespace Vinabook.Controllers
{
    public class TacGiaController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: TacGia
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhMucTacGia_Partial()
        {
            var listTacGia = from tg in db.TacGias
                             select tg;
            return PartialView(listTacGia.ToList());
        }
    }
}