using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer.Define.EnumServer
{
    public enum EnumConditionResult
    {
        no_operation = 0,           // 不操作
        update = 1,                 // 更新
        complete_update = 2,        // 完成并更新
        complete_no_operation = 3,  // 完成不更新
    }
}
