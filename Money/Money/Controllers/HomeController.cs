using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Money.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public JsonResult GetSymbols()
        {
            //   return new JsonResult();

            return Json(new List<object>()
            {  new  {id=1 },
            new {id=2}
            },JsonRequestBehavior.AllowGet

                );
        }
    }
}