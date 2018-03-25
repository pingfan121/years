using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer.Define.EnumServer
{
    public enum EnumAiStateProcessType
    {
        none = 0,
        begin = 1,
        run = 2,
        end = 3
    }

    public enum EnumAiStateType
    {
        unknown = 0,
        sleep = 1,
        active_patrol = 2, // 巡逻
        active_search = 3, // 搜索
        active_return = 4, // 返回
        active_chase = 5, // 追击
        active_fight = 6, // 战斗
        active_escape = 7, // 逃跑
        active_path = 8, // 路径
        active_die = 9, // 死亡
        active_refresh = 10, // 刷新
        active_end,

    }
}
