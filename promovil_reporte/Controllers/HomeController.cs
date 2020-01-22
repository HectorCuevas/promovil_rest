using promovil_reporte.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace promovil_reporte.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            GeneratePDF pdf = new GeneratePDF();
            pdf.ManipulatePdf("C:\\Users\\Norman\\Documents\\pdf\\simple_table.pdf");
            return View();
        }
    }
}
