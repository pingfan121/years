using db;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Controllers
{
    public class PerfectController : Controller
    {
        int a = 0;
        public PerfectController()
        {
            a = new Random().Next(0, 10000);
        }
        // GET: Perfect
        public ActionResult Index()
        {
            Console.WriteLine(a);
            return View();
        }

        public string Test()
        {
          
             return (new Random().Next(1, 10000)).ToString();
        }
    }
}
