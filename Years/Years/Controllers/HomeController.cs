using GameLib.Util;
using System;
using System.Web.Mvc;
using Years.IServices;
using Years.Model;
using Years.Services;
using Years.WebCore;

namespace Years.WebUI.Controllers
{
    public class HomeController : BaseController
    {

        IUserInfoServices userinfoservice;


        public HomeController(IUserInfoServices userinfoservice)
        {
            this.userinfoservice = userinfoservice;
        }

        public ActionResult Index()
        {
            // return JsonResult.

            //var usreinfo = userinfoservice.QueryWhere(c => c.uID > 2).FirstOrDefault();
            //for (int i = 0; i < 10; i++)
            //{
            //    userinfoservice.Add(new UserInfo()
            //    {
            //        id = ObjectId.NewObjectId().ToString(),
            //        name = "admin" + i,
            //        pass = "123456",
            //        real_name = "超级管理员" + i,
            //        create_time = DateTime.Now,
            //        intro = "测试添加功能"
            //    });
            //}

            //userinfoservice.SaverChanges();
            //return Content("添加数据成功");
            return Content("测试网页");

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
    }
}