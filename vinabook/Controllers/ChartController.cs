using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using Vinabook.Models;
using System.Configuration;

namespace Vinabook.Controllers
{
    public class ChartController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: Chart
        public ActionResult PieChart()
        {
            return View(ChartData.GetPieChartData());
        }
        public ActionResult Chart()
        {
            return View();
        }
        public ActionResult Index()
        {
            //bai này cũ
            //string connectionString = ConfigurationManager.ConnectionStrings["QuanLyBanSachEntities"].ConnectionString;
            //string conn = ExtractFromString(connectionString, "data source=", "EntityFramework").Replace(@"\\",@"\");
            return View();
        }
        public ActionResult ThongKeDoanhThu_Nam_Chart()
        {
            //return View(list);
            return Json(new { Url=Url.Action("_Partial_ThongKeDoanhThu_Nam_Chart") });
        }
        public ActionResult _Partial_ThongKeDoanhThu_Nam_Chart()
        {
            string conn = @"data source=.\sqlexpress;initial catalog=QuanLyBanSach;user id=sa;password=123;MultipleActiveResultSets=True;App=EntityFramework";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM ThongKeDoanhThuTheoNam_View", connection))
                {
                    table.Load(command.ExecuteReader());
                }
                connection.Close();
            }
            var list = new List<ThongKeDoanhThuTheoNam>();
            foreach (DataRow r in table.Rows)
            {
                var item = new ThongKeDoanhThuTheoNam
                {
                    Nam = r["Nam"].ToString(),
                    TongTien = r["TongTien"].ToString(),
                };
                list.Add(item);
            }
            return PartialView(list);
        }
        [HttpPost]
        public ActionResult ThongKeDoanhThu_Thang_Chart(string Nam)
        {
            TempData["Nam"] = Nam;
            return Json(new { Url = Url.Action("_Partial_ThongKeDoanhThu_Thang_Chart") });
        }
        public ActionResult _Partial_ThongKeDoanhThu_Thang_Chart()
        {
            string conn = @"data source=.\sqlexpress;initial catalog=QuanLyBanSach;user id=sa;password=123;MultipleActiveResultSets=True;App=EntityFramework";
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                using (var command = new SqlCommand("exec ThongKeDoanhThuTheoThang "+ TempData["Nam"], connection))
                {
                    table.Load(command.ExecuteReader());
                }
                connection.Close();
        }
            var list = new List<ThongKeDoanhThuTheoThang>();
            foreach (DataRow r in table.Rows)
            {
                var item = new ThongKeDoanhThuTheoThang
        {
                    Thang = r["Thang"].ToString(),
                    TongTien = r["TongTien"].ToString(),
                };
                list.Add(item);
            }
            return PartialView(list);
        }



        private static string ExtractFromString(string text, string startString, string endString)
        {
            string matched = "";
            int indexStart = 0, indexEnd = 0;
            bool exit = false;
            while (!exit)
            {
                indexStart = text.IndexOf(startString);
                indexEnd = text.IndexOf(endString) + endString.Length;
                if (indexStart != -1 && indexEnd != -1)
                {
                    matched = text.Substring(indexStart, indexEnd - indexStart);
                    break;
                }
                else
                    exit = true;
            }
            return matched;
        }

    }
}