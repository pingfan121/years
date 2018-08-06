using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.Common.Enum
{

    /// <summary>
    /// 负责标记ajax请求以后的json数据状态
    /// </summary>
    public class EnumAjaxState
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static string success="success";
        /// <summary>
        ///  错误异常
        /// </summary>
        public static string error = "error";
        /// <summary>
        /// 未登录
        /// </summary>
        public static string nologin = "nologin";

        //未注册
        public static string noreg = "noreg";

    }
    
}
