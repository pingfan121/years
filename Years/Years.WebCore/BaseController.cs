using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Wchl.WMBlog.Common;

namespace Years.WebCore
{
    public class BaseController : Controller
    {

        #region 封装ajax请求的返回方法
        protected ActionResult WriteSuccess(string msg)
        {
            return Json(new { status = (int)Enums.EAjaxState.success, msg = msg });
        }

        public ActionResult WriteSuccess(string msg, object obj)
        {
            return Json(new { status = (int)Enums.EAjaxState.success, msg = msg, datas = obj });
        }
        public ActionResult WriteError(string msg)
        {
            return Json(new { status = (int)Enums.EAjaxState.error, msg = msg });
        }
        protected ActionResult WriteError(Exception ex)
        {
            //获取ex的第一级内部异常
            Exception innerEx = ex.InnerException == null ? ex : ex.InnerException;
            //循环获取内部异常直到获取详细异常信息为止
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }
            return Json(new { status = (int)Enums.EAjaxState.error, msg = innerEx.Message });
        }
        #endregion
    }
}
