using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Years.IServices;
using Years.Model;
using Years.Services;
using Years.WebCore;

namespace Years.WebUI.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(string phone,string pwd)
        {

            //验证手机号是否合法

            //验证密码是否合法

            //检测数据库中是否存在这个名字

            using (var db = new YearsEntities())
            {
                var info = db.user_info.FirstOrDefault(item => item.mobile == phone);

                if (info != null)
                {
                    return Error("该手机号已经被注册");
                }
                else
                {
                    info = new user_info();

                    info.mobile = phone;
                    info.pass = pwd;
                    info.name = "测试账号";

                    db.user_info.Add(info);

                    db.SaveChanges();

                    return Success(null, "注册成功");
                }

            }
        }

        [HttpPost]
        public JsonResult Login(string phone, string pwd)
        {

            //验证手机号是否合法

            //验证密码是否合法

            //检测数据库中是否存在这个名字

            using (var db = new YearsEntities())
            {
                var info = db.user_info.FirstOrDefault(item => item.mobile == phone);

                if (info == null)
                {
                    return Error("noreg","该手机号还没有注册");
                }
                else
                {

                    Session["user"] = info.mobile;

                    return Success(null, "登录成功");
                }

            }
        }
    }
}