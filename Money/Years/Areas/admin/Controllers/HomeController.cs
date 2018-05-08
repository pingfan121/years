using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Years.WebCore;

namespace Years.WebUI.Areas.admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: admin/Home
        public ActionResult Index()
        {
            return View();
        }

    }
}