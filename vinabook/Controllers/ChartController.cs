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
        public ActionResult ThongKeDoanhThu_Chart()
        {
            //bai này cũ
            //string connectionString = ConfigurationManager.ConnectionStrings["QuanLyBanSachEntities"].ConnectionString;
            //string conn = ExtractFromString(connectionString, "data source=", "EntityFramework").Replace(@"\\",@"\");
            string conn = @"data source=.\sqlexpress;initial catalog=QuanLyBanSach;user id=sa;password=123;MultipleActiveResultSets=True;App=EntityFramework";
            DataTable table = new DataTable();

            // Creates a SQL connection
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM ThongKeDoanhThuTheoNam_View", connection))
                {
                    table.Load(command.ExecuteReader());
                }
                connection.Close();
            }
            ////IEnumerable<DataRow> sequence = table.AsEnumerable();
            //List<DataRow> list = table.AsEnumerable().ToList();
            //ViewBag.List = list;
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
            return View(list);
            //return Json(new { Url=Url.Action("_Partial_ThongKeDoanhThu_Chart") });
        }
        public ActionResult _Partial_ThongKeDoanhThu_Chart()
        {
            return View();
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