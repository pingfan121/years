//敌人类
var Enemy =
    {

        init: function (x, y, w, h, img, speed, hp, score, maxI) {
            //属性
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.img = img;
            this.speed = speed; //速度
            this.hp = hp; //血量
            this.score = score; //分数
            this.boom = false; //是否爆炸
            this.i = 0; //第几张图片
            this.maxI = maxI; //播放的图片张数
            this.isDie = false; //是否死亡
            this.flagI = 0; //图片切换的频率
        },

                   
        draw: function ()
        {
            //爆炸, 切换图片
            if (this.boom)
            {
                this.flagI++;
                if (this.flagI == 5)
                {
                    this.i++;
                    if (this.i == this.maxI)
                    {
                        //当图片切换结束,飞机死亡
                        this.isDie = true;
                    }
                    //重置
                    this.flagI = 0;
                }
            }
            ctx.drawImage(this.img, this.i * this.w, 0, this.w, this.h, this.x, this.y, this.w, this.h);
        },

        move = function ()
        {
            this.y += this.speed;
        }


    }