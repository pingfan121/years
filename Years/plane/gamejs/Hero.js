
//英雄图片
var heroImg = new Image();
heroImg.src = "../../img/herofly.png";

//英雄对象
var hero = {
    //属性
    x: mapCanvas.width / 2 - 33,
    y: mapCanvas.height - 82 - 100,
    w: 66,
    h: 82,
    i: 0,
    flagI: 0, 
    bullets: [], //用于记录发射出去的子弹
    flagShot: 0, //子弹发射频率
    armType: 0, //武器类型(0: 单排; 1:双排)
    boom: false, //是否爆炸
   

    draw: function () {
        //控制图片切换
        this.flagI++;
        if (this.flagI == 10)
        {
            if (this.boom)
            {
                this.i++;
                if (this.i == 5)
                {
                    //英雄死亡
                    gameOver();
                }
            }
            else
            {
                this.i = (this.i++) % 2;
            }
            //重置
            this.flagI = 0;
        }

        //把图片的某一部分画到canvas上某个区域
        ctx.drawImage(heroImg, this.i * this.w, 0, this.w, this.h, this.x, this.y, this.w, this.h);
    },

    //发射子弹
    shot: function () {
        //爆炸后, 不能发射子弹
        if (!this.boom) {
            this.flagShot++;
        }

        if (this.flagShot == 5)
        {
            //播放发射子弹音乐
            musics[0].play();

            if (this.armType)
            {
                //创建双排子弹对象
                var bullet = new Bullet(this.x + this.w / 2 - 24, this.y + 20, 48, 14, bulletImg2, 2);
            }
            else
            {
                //创建单排子弹对象
                var bullet = new Bullet(this.x + this.w / 2 - 2, this.y - 14, 6, 14, bulletImg1, 1);
            }

            //记录子弹
            this.bullets.push(bullet);
            //重置
            this.flagShot = 0;
        }

        //移动每一颗子弹
        for (var i = 0; i < this.bullets.length; i++)
        {
            //判断子弹是否超出屏幕
            if (this.bullets[i].y <= -this.bullets[i].h)
            {
                //删除子弹
                this.bullets.splice(i, 1);
                i--;
            }
            else
            {
                //移动子弹
                this.bullets[i].move();
                this.bullets[i].draw();
            }
        }
    },
}