using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;
using System.Web.Security;

namespace Vinabook.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string sTaiKhoan = f["username"].ToString();
            string sMatKhau = f.Get("password").ToString();
            string urlString = f.Get("urlString").ToString();
            var usr = (from u in db.KhachHangs
                       where u.TaiKhoan == sTaiKhoan && u.MatKhau == sMatKhau
                       select u).FirstOrDefault();
            //TempData["UserName"] = usr.TaiKhoan;
            if (usr != null)
            {
                //create seession/ token for loged in user
               // FormsAuthentication.SetAuthCookie(usr.TaiKhoan, false);
                Session["TaiKhoan"] = usr;
                //lay gio hang cua khach hang 
                if (urlString.Trim() != "")
                {
                    string[] url = urlString.Split('/');
                    if (url[url.Length-1] == "Login")
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(urlString);
                }
                else
                return RedirectToAction("Index", "Home");
            }
            
            TempData["Message"] = "Username or password is wrong";
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!";

            return View();
            //KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            //if (kh != null)
            //{
            //    //<script type="text/javascript"> alert('Xss done');</script>

            //    ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công !";
            //    Session["TaiKhoan"] = kh;
            //    return RedirectToAction("Index","Home");
            //}
            //ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!";
            //return View();

        }

        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                //Chèn dữ liệu vào bảng khách hàng
                db.KhachHangs.Add(kh);
                //Lưu vào csdl 
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult Logout(string urlString)
        {
            //FormsAuthentication.SignOut();
            Session["TaiKhoan"] = null;
            if (urlString.Trim() != "")
                return Redirect(urlString);
            return RedirectToAction("Index", "Home");
        }
    }
}