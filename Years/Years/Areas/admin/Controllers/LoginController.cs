using GameLib.Util;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Years.Common;
using Years.Common.Cache;
using Years.IServices;
using Years.ViewModel;
using Years.WebCore;

namespace Years.WebUI.Areas.admin.Controllers
{

    public class LoginController : BaseController
    {
        private readonly ICacheManager cacheManager;
        IAdminUserInfoServices AdminUserInfoServices;
        public LoginController(IAdminUserInfoServices UserInfoServices, ICacheManager cacheManager)
        {
            this.AdminUserInfoServices = UserInfoServices;
            this.cacheManager = cacheManager;
        }

        // GET: admin/Login
        public ActionResult Index()
        {
            AdminLoginInfoViewModel uinfo = new AdminLoginInfoViewModel()
            {
                login_name = "admin",
                is_member = false
            };

            if (Request.Cookies[Keys.IsMember] != null)
            {
                uinfo.is_member = true;
            }
            return View(uinfo);
        }

        [HttpPost]
        public ActionResult Login(AdminLoginInfoViewModel model)
        {
            string vcodeFromSession = string.Empty;
            if (Session[Keys.vcode] != null)
            {
                vcodeFromSession = Session[Keys.vcode].ToString();
            }
            if (model.vcode=="" || vcodeFromSession.Equals(model.vcode, StringComparison.OrdinalIgnoreCase) == false)
            {
                return WriteError("验证码不合法");
            }
            var userinfo = AdminUserInfoServices.QueryWhere(c => c.name == model.login_name && c.pass == model.login_pass).FirstOrDefault();
            if (userinfo == null)
            {
                return WriteError("用户名或者密码错误");
            }
            // Session[Keys.uinfo] = userinfo;
            //改用redis缓存
            string sessionId = ObjectId.NewObjectId().ToString();//必须保证Memcache的key唯一
            cacheManager.Set(sessionId, userinfo, TimeSpan.FromHours(1));
            Response.Cookies[Keys.uinfo].Value = sessionId;//将自创的用户信息以Cookie的形式返回给浏览器。

            if (model.is_member)
            {
                HttpCookie cookie = new HttpCookie(Keys.IsMember, userinfo.id);
                cookie.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Add(cookie);
            }
            else
            {
                HttpCookie cookie = new HttpCookie(Keys.IsMember, "");
                cookie.Expires = DateTime.Now.AddYears(-3);
                Response.Cookies.Add(cookie);
            }

            return WriteSuccess("登录成功");

        }
        [HttpGet]
        public ActionResult Logout()
        {
            //清空Session[Keys.uinfo]对象
            if (cacheManager.Contains(Request.Cookies[Keys.uinfo].Value))
            {
                cacheManager.Remove(Request.Cookies[Keys.uinfo].Value);
                Response.Cookies[Keys.uinfo].Expires = DateTime.Now.AddYears(-3);
            }
            return Redirect("/admin/Login/Index");



        }
    }

}