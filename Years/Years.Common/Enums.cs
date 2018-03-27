using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wchl.WMBlog.Common
{
    
    public class Enums
    {
        /// <summary>
        /// 负责标记ajax请求以后的json数据状态
        /// </summary>
        public enum EAjaxState
        {
            /// <summary>
            /// 成功
            /// </summary>
            success = 0,
            /// <summary>
            ///  错误异常
            /// </summary>
            error = 1,
            /// <summary>
            /// 未登录
            /// </summary>
            nologin = 2

        }
        /// <summary>
        ///  菜单使用状态
        /// </summary>
        public enum EState {
            /// <summary>
            /// 正常
            /// </summary>
            Nomal = 0,
            /// <summary>
            /// 停用(删除)
            /// </summary>
            Stop = 1
        }
    }
}
