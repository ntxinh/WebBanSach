using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinabook.Models;

namespace Vinabook.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult PieChart()
        {
            return View(ChartData.GetPieChartData());
        }
    }
}