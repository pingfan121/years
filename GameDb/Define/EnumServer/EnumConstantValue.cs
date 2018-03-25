namespace GameServer.Define.EnumServer
{
    public enum EnumConstantValue
    {
        // 摆摊相关
        stall_name_count = 12, // 摊位最大字符数
        stall_trade_count = 722, // 交易记录最大显示条数

        // 任务相关
        task_everyday_refresh = 1, // 刷新每日任务星数所需绑定金币
        task_everyday_count_buy_cost = 2, // 购买每日任务所需元宝
        task_everyday_count_buy_times = 3, // 日常任务可购买次数
        task_everyday_double = 4, // 双倍领取领取每日任务奖励所需绑定金币
        task_everyday_vippoint = 5, // 每日任务一键最高星所需元宝
        task_everyday_count = 6, // 每日任务每天免费次数
        task_everyday_triple = 7, // 三倍领取每日任务奖励所需绑定金币
        task_weekly_count = 9, // 周任务每周免费次数
        task_begin = 10, // 起始任务ID
        task_everyday_reset_level = 13, // 日常任务重置次数等级ID
        task_everyday_level = 14, // 开启日常任务的等级
        task_weekly_level = 15, // 开启周常任务的等级
        task_everyday_level_limit = 16, // 日常任务一次满星等级
        task_everyday_level_max = 17, // 日常任务最高等级
        task_everyday_level_trigger = 18, // 日常任务开启前置主线任务ID（任务完成后开启）
        task_everyday_award_change = 19, // 官阶等级到达10时日常任务奖励改变
        task_everyday_one_click_count = 21, // 一键完成日常任务消耗斧子数量
        task_everyday_one_click_item_id = 22, // 斧子ID
        task_elite_everyday_count = 23, // 每日精英任务每天免费次数
        task_elite_everyday_id = 24, // 每日精英任务ID

        // 装备相关
        //equip_durable_weapon_rate = 105, // 武器掉耐久概率
        //equip_durable_clothing_rate = 106, // 衣服掉耐久概率
        //equip_durable_jewelry_rate = 107, // 饰品掉耐久概率
        equip_durable_multiple = 108, // 掉耐久倍数
        equip_inlay = 109, // 魂装镶嵌所需最小装备组
        equip_summon_item = 110, // 天人合一所需物品
        equip_summon_suit_id = 111, // 天人合一套装ID
        equip_summon_response = 112, // 天人合一响应时间
        equip_summon_cooldown = 113, // 天人合一CD时间
        equip_totem_cooldown = 2501, // 图腾CD时间

        // 商店相关
        shop_repo_price = 301, // 回购价格倍数

        // 组队相关
        team_max_member_count = 401, // 最大组队人数
        team_exp = 402, // 组队后队友人数对经验加成比例
        team_level_max = 403, // 自动组队最大等级
        mission_monster_share_level = 404, // 任务怪共享等级

        // 物品相关
        item_bag_count = 601, // 背包格子数量
        item_storage_page = 602, // 仓库最大页数
        item_storage_grid = 603, // 每页仓库最大物品个数
        item_storage_initial = 609, // 初始仓库格数
        item_drop_duration_time = 604, // 物品掉在地上消失时间
        item_bead_count = 605, // 聚灵珠每日使用次数
        item_bead_add_count = 606, // 每次转生增加聚灵丹每日使用次数
        item_bead_ratio = 607, // 聚灵珠获得经验比例
        item_fashion_count = 608, // 时装格子数量

        // 帮会相关
        guild_create_role_level = 701, // 建帮会角色等级下限
        guild_create_vippoit_count = 702, // 创建帮会所需元宝
        guild_create_vippoit_level = 703, // 创建帮会所需vip等级
        guild_log_page_max = 704, // 帮会日志最大页数
        guild_log_page_count = 705, // 帮会日志每页数量
        guild_storage_page_max = 706, // 帮会仓库最大页数
        guild_storage_page_count = 707, // 帮会仓库每页数量
        guild_red_page_max = 708, // 帮会红包最大页数
        guild_red_page_count = 709, // 帮会红包每页数量
        guild_red_max = 710, // 红包最大数量
        guild_red_gold_min = 711, // 红包最小金额
        guild_red_time = 712, // 红包持续时间
        guild_red_min = 713, // 红包最小数量
        guild_treasure_chest = 725, // 帮会宝箱ID
        guild_kill_boos_count = 726, // 帮会争霸最小杀boss数目
        guild_kill_boos_award_level = 727, // 帮会争霸领奖最小等级
        guild_pk_Module = 728, // 第一次加入帮会模式修改
        guild_auto_dissolve_time = 729, // 帮会解散所需天数

        // 人物相关
        role_relive_immediately_yb = 501, // 原地复活所需金币
        role_relive_immediately_hp = 502, // 原地复活血量万分比
        role_relive_immediately_mp = 503, // 原地复活魔法量万分比
        role_relive_back_hp = 504, // 回城复活血量万分比
        role_relive_back_mp = 505, // 回城复活魔法量万分比
        role_rebirth_level = 801, // 转生开启等级
        role_rebirth_count = 802, // 每日免费降级获得转生经验次数
        role_rebirth_expend_gold = 803, // 降级获得转生经验所需绑定金币
        role_stars_level = 804, // 星宿开启等级
        role_stars_expend = 805, // 星宿消耗金币
        //role_rune_level = 716, // 符文等级上限
        //role_wolf_level = 717, // 狼牙等级上限
        //role_shield_level = 718, // 护盾等级上限
        role_officer_level = 719, // 官职等级上限

        // 仇恨
        pk_sin_inc = 1001, // 击杀角色增加罪恶值
        pk_sin_dec_per = 1002, // 罪恶值衰减间隔时间
        pk_sin_dec_val = 1003, // 罪恶值每次衰减数量
        pk_sin_dec_val2 = 1004, // 在红名村每次衰减罪恶值的数量
        pk_malice = 1005, // 恶意PK持续时间
        pk_sin_trans = 1006, // 红名传送回城点

        // 幸运
        luck_val = 1101, // 每点幸运值出现最大伤害增加概率
        luck_max = 1102, // 最大幸运值
        luck_sin = 1103, // 玩家犯谋杀罪降祝福概率

        // 聊天相关CD时间
        chat_world_time = 1201, // 世界频道聊天时间限制（毫秒）

        // 翅膀操作
        //wing_strike_rate = 1301, // 翅膀培养暴击概率
        //wing_strike_value = 1302, // 翅膀培养增加培养值
        wing_expend_base_id = 1303, // 翅膀培养消耗物品
        wing_base_id = 1304, // 第一个翅膀ID
        wing_mall_upgrade = 1305, // 升级翅膀商城购买ID

        // 出生点
        born_map_id = 1401, // 出生点地图
        born_point_x = 1402, // 出生点坐标x
        born_point_y = 1403, // 出生点坐标y
        born_point_off = 1404, // 出生点坐标偏移量

        //城主时装
        shacheng_master_weapon = 1501, // 沙城城主武器
        shacheng_master_clothes = 1502, // 沙城城主衣服

        // 世界繁荣度
        world_begin_role_level = 1601, // 获得世界繁荣度经验加成的起始角色等级

        //邮件
        mail_max_count = 900,           // 邮件最大数量
        mail_effective_time = 901,      // 邮件持续时间

        //宝藏
        mine_yaoshi = 1701,
        mine_grid = 1702,

        // 装备回收
        recycle_day = 1801, // 回收改变规则天数天数
        recycle_team_min = 1802, // 回收改变之前回收装备最大套数
        recycle_team_max = 1803, // 回收改变之后回收装备最大套数
        recycle_strengthen_min = 1804, // 回收改变之前回收装备最大强化等级
        recycle_strengthen_max = 1805, // 回收改变之后回收装备最大强化等级
        recycle_exp_max = 1806, // 回收每日最大经验
        recycle_team_min_limit = 1807, // 禁止回收最小组别
        recycle_max_level = 1808, // 回收最大等级

        mine_one_change = 1901,  //宝藏第一次改变天数
        mine_one_use_gould = 1902,   //宝藏第一次改变使用最大组
        mine_two_change = 1903,  //宝藏第二次改变天数
        mine_two_use_gould = 1904,   //宝藏第二次改变使用最大组

        buff_worldid = 1905,  //世界繁荣度buff id

        //skill_trhy = 1906,   //天人合一技能id
        mars_call_vip_point = 1907, // 召唤护卫价格

        htsdid = 1911, //护体神盾id;
        htsdyb = 1912, //护体神盾需要元宝数量

        needingot =1913, //达到挖宝送物品的元宝数量
        senditemid=1914, //送物品的id


        offline_hour_exp = 2101, // 60000	每小时获得离线经验
        offline_max_hour = 2102, // 72	最大小时数
        offline_2_award = 2103,	// 13	2倍领取每小时所需元宝
        offline_5_award = 2104, // 40	5倍领取每小时所需元宝

        invest_time = 2201, // 时间投资元宝数
        invest_level = 2202, // 等级投资元宝数
        invest_record_count = 2203, // 投资显示记录条数

        vip_item_count = 2401, // vip金砖对应限制ID(对应物品使用次数表)
        vip_card_1 = 2402, // vip白银卡
        vip_card_2 = 2403, // vip黄金卡

        open_neigong_level = 20, //内功开启等级
        

        bj_exp=2601,  //经验报警限值
        bj_ng = 2602,  //内功报警限值
        bj_yb = 2603,  //元宝报警限值
        bj_attr = 2604,  //属性报警限值

        xs_yh = 2605,  //羽化系数
        xs_lx = 2606,  //龙心系数
        xs_ly = 2607,  //狼牙系数
        xs_cb = 2608,  //翅膀系数
        xs_xj = 2609,  //血祭系数
        xs_gj = 2610,  //官阶系数
        xs_xx = 2611,  //星宿系数
        xs_cz = 2612,  //充值系数
        xs_xf = 2613,  //消费系数

        secret_store_refresh = 2701, //神秘商店元宝刷新所需数量
        secret_store_time = 2702, //神秘商店元宝刷新所需时间

        crazy_exchange_Ingot_cd = 2807, //挂机狂欢兑换CD（毫秒）

        guaji_min_level=2808,//普通挂机最低等级
        guaji_vip_level1=2809, //vip低级等级限制
        guaji_vip_level2 = 2810, //vip高级等级限制

        beheaded_time = 2811, //头颅挖取时间（毫秒）
        //
        limitMoveTime=11111,//移动限制间隔时间
        checkMoveTimes = 11112,//移动检测间隔时间

    }
}
