//子弹类
var Bullet =
    {
        //属性
        x: 0,
        y: 0,
        w: 0,
        h: 0,
        img: 0,
        hurt: 0,

        init: function (x, y, w, h, img, hurt)
        {
            //属性
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.img = img;
            this.hurt = hurt; //伤害
        },

        draw: function () {
            ctx.drawImage(this.img, this.x, this.y, this.w, this.h);
        },
        //移动方法
        move: function () {
            this.y -= 15;
        }
}
