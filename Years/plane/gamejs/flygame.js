var gamestate = 0;//当前游戏状态 0是未开始 1是 进行中 2是已结束

var main = {

    //游戏开始
    gamestart: function()
    {
    },

    gameend: function ()
    {
    },




    //碰撞检测
    //两个矩形碰撞检测
    crash: function (obj1, obj2)
    {
        //两个物体上下左右的位置
        var left1 = obj1.x;
        var right1 = obj1.x + obj1.w;
        var top1 = obj1.y;
        var bottom1 = obj1.y + obj1.h;
        var left2 = obj2.x;
        var right2 = obj2.x + obj2.w;
        var top2 = obj2.y;
        var bottom2 = obj2.y + obj2.h;

        //判断是否发生碰撞
        if (right1 < left2 || bottom1 < top2 || left1 > right2 || top1 > bottom2)
        {
             return false;
        }
        else
        {
                return true;
        }
    }

}

