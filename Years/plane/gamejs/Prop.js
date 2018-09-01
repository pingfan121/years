var Prop =
{

    init: function(x, y, w, h, type, speed) 
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
        this.type = type; //道具类型(0:炸弹, 1:双排子弹)
        this.speed = speed;
        this.isUsed = false; //道具有没有被使用
    },
    //方法
    draw: function ()
    {
        ctx.drawImage(propImg, this.type * this.w, 0, this.w, this.h, this.x, this.y, this.w, this.h);
    },
    //移动
    move: function ()
    {
        this.y += this.speed;
    }

}