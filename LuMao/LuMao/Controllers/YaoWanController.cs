using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaoWanDB;

namespace LuMao.Controllers
{
    public class YaoWanController : Controller
    {
        // GET: YaoWan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            using (var db = new YaoWan1ET())
            {
                var data = db.race.Select(c => c).ToList();

                Console.WriteLine(data);

                return View(data);
            }
        }
    }
}