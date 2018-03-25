using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Controllers
{
    public class TestController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        //
        // GET: /HelloWorld/Welcome/
//         public string Welcome()
//         {
//             return "This is the Welcome action method...";
//         }

//         public string Welcome(string name, int numTimes = 1)
//         {
//             return HttpUtility.HtmlEncode("Hello " + name + ", NumTimes is: " + numTimes);
//         }

        public ActionResult Welcome(int id,string name)
        {
         //   return "Hello " + name + ", NumTimes is: " + id;

            ViewBag.Message = "你好呀 " + name;
            ViewBag.id = id;

            return View();
        }
	}
}