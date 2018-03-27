using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Years.IServices;
using Years.Model;
using Years.WebCore;

namespace Years.WebUI.Areas.admin.Controllers
{
    public class AdminUserInfoController : BaseController
    {
        IAdminUserInfoServices adminUserInfoService;

        public AdminUserInfoController(IAdminUserInfoServices adminUserInfoService)
        {
            this.adminUserInfoService = adminUserInfoService;
        }
        // GET: admin/UserInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(AdminUserInfo model)
        {
            adminUserInfoService.Add(model);
            adminUserInfoService.SaverChanges();
            return WriteSuccess("添加成功");
        }
        public ActionResult GetData()
        {
            int pageIndex = Request["start"] != null ? int.Parse(Request["start"]) : 1;
            int pageSize = Request["length"] != null ? int.Parse(Request["length"]) : 5;
            int draw = Request["draw"] != null ? int.Parse(Request["draw"]) : 1;
            int totalCount;
            short delFlag = 0;
            var userInfoList = adminUserInfoService.QueryByBeginPage<int>(pageIndex, pageSize, out totalCount, r => r.uStatus == delFlag, r => r.uID, true);
            var temp = from u in userInfoList
                       select new { ID = u.uID, UserName = u.uLoginName, UserPwd = u.uLoginPWD, RealName = u.uRealName, subtime = u.uCreateTime, Remark = u.uRemark };
            var jsonDataTemp = new
            {
                data = temp.ToList(),
                draw = draw,
                recordsTotal = totalCount,
                recordsFiltered = totalCount

            };
            return Json(jsonDataTemp, JsonRequestBehavior.AllowGet);
        }
    }
}