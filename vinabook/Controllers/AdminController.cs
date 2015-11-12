using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vinabook.Models;
using System.Web.Security;

namespace Vinabook.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form, string ReturnUrl)
        {

            string url = Server.UrlDecode(Request.UrlReferrer.PathAndQuery);
            string[] urldecode = url.Split('=');
            string returnurl = "";
            if (urldecode.Count() > 1)
            {
                returnurl = urldecode[1];
            }
            string username = form["username"].ToString();
            string password = form["password"].ToString();
            var usr = (from u in db.Adminnistrators
                       where u.Username == username && u.Password == password && u.IsActive == true
                       select u).FirstOrDefault();
            if (usr != null)
            {
                //create seession/ token for loged in user
                FormsAuthentication.SetAuthCookie(usr.Username, false);

                if (!string.IsNullOrEmpty(returnurl))
                {
                    return Redirect(returnurl);

                }
                return RedirectToAction("Index");


            }
            TempData["Message"] = "Tên tài khoản hoặc mật khẩu không đúng!";
            //
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public PartialViewResult TotalMember()
        {
            return PartialView();
        }
        public PartialViewResult TotalBook()
        {
            return PartialView();
        }
        public PartialViewResult TotalMoney()
        {   
            return PartialView();
        }
        public PartialViewResult TotalDHChuaThanhToan()
        {
            return PartialView();
        }
        
    }
}