using db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace test.Controllers
{
    public class YaoWanController : Controller
    {
        // GET: YaoWan
        public ActionResult Index()
        {
            using (var db = new testEntities())
            {
                var data = db.yao_wan.Select(c => c).ToList();

                Console.WriteLine(data);

                return View(data);
            }


          
            

               
        }
    }
}