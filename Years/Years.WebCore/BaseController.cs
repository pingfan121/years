using System;
using System.Web.Mvc;
using Years.Common.Enum;

namespace Years.WebCore
{
    public class BaseController : Controller
    {

        #region 封装ajax请求的返回方法
        public JsonResult Success( object obj, string msg="")
        {
            return Json(new { status = EnumAjaxState.success, msg = msg, datas = obj });
        }
        public JsonResult Error(string msg)
        {
            return Json(new { status = EnumAjaxState.error, msg = msg });
        }

        protected JsonResult Error(Exception ex)
        {
            //获取ex的第一级内部异常
            Exception innerEx = ex.InnerException == null ? ex : ex.InnerException;
            //循环获取内部异常直到获取详细异常信息为止
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }
            return Json(new { status = EnumAjaxState.error, msg = innerEx.Message });
        }
        #endregion
    }
}
