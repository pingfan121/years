using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer.Define.EnumServer
{
    public enum EnumBufferStackType
    {
        exclude,    // 排斥无限制
        replace_type, // 替换同类型
        replace_level, // 替换低等级
        replace_dmg, // 替换低伤害
        additional, // 附加同类型
    }
}
