using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.Common
{
   public  class Keys
    {
        /// <summary>
        /// 用于存放验证码的session  key
        /// </summary>
        public const string vcode = "blogcode";

        /// <summary>
        /// 用于存放登录成功的用户实体的session  key
        /// </summary>
        public const string uinfo = "bloguinfo";
        /// <summary>
        /// 用于存放登录成功以后的用户id的cookie key
        /// </summary>
        public const string IsMember = "blogIsMember";
        /// <summary>
        ///  用于缓存整个autofac的容器对象的 缓存key
        /// </summary>
        public const string AutofacContainer = "crmautofacContainer";
        /// <summary>
        /// 用于缓存某个用户的权限按钮数据 的 缓存key
        /// </summary>
        public const string PermissFunctionsForUser = "PermissFunctionsForUser";
    }
}
