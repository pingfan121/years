/**
 * Created by Administrator on 2016/5/30.
 */
$(function () {
    $('#menu').metisMenu();
    // $(".rightcontent").load("welcome.html");
})

//左侧菜单点击效果
function afterPageLoad() {
    $('#zccd .menu').menu();
    $('#zccd .menu .nav li:not(".nav-parent") a').click(function() {
        var $this = $(this);
        $('.menu .nav .active').removeClass('active');
        $this.closest('li').addClass('active');
        var parent = $this.closest('.nav-parent');
        if(parent.length)
        {
            parent.addClass('active');
        }
    });
}