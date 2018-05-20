using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    //这是一个静态通用数据类 所定义的字段都必须是 int 类型的  线程操作原子是int 所以不用考虑跨线程的问题
    public class CommonData
    {
        public static int parities_USRMB;//美元对人民币汇率
    }
}