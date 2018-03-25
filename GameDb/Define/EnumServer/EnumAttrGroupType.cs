using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer.Define.EnumServer
{
    public enum EnumAttrGroupType
    {
        none = 0,
        begin = -1,
        total = 1,
        unit = 2,
        item = 3,
        bufffer = 4,
        skill = 5,
        officer = 6,
        title = 7,
        wing = 8,
        guan_jie = 9,
        blood_sacrifice_activate = 10,
        blood_sacrifice_level = 11,
        guild_level = 12,
        equip = 13,
        equip_totem = 14,
        vip = 15,
        item_collect = 16, // 物品收藏
        end = 17,
    }
}
