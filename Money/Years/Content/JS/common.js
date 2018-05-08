var helper = {
    //1.0 给ligerGrid获取完服务器响应回来的数据后回调时使用,data:服务器响应回来的json数据被转换成了js的对象
    gridsu: function (data) {
        //data:格式：1、正常格式 {"Rows":[{KID:1,KType:1,KName:"",Kvalue:""}],"Total":10}
        //2.异常或者未登录格式:{status:1/2,msg:""}
        if (data.status == "1") {
            helper.errorTip(data.msg);
        } else if (data.status == "2") {
            //提醒用户未登录,用户点击确定按钮跳转到登录页
            helper.warnTip(data.msg, "tip", function () { window.top.location = "/admin/login/login"; });
            //2秒钟以后自动跳转到登录页
            setTimeout(function () { window.top.location = "/admin/login/login"; }, 2000);
        }
    },
    //2.0提示成功信息
    layer_success: function (msg) {
        layer.alert(msg, { icon: 1,title:"系统提示" });
    },
    //3.0提示错误信息
    layer_error: function (msg) {
        layer.alert(msg, { icon: 2, title: "系统提示" });
    },
    //4.0提示错误信息
    layer_warning: function (msg) {
        layer.alert(msg, { icon: 7, title: "系统提示" });
    },
    //5.0弹出层
    /*
      参数解释：
      title	标题
      url		请求的url
      id		需要操作的数据id
      w		弹出层宽度（缺省调默认值）
      h		弹出层高度（缺省调默认值）
  */
    layer_show: function (title, url, w, h)
    {
        if (title == null || title == '') {
            title = false;
        };
        if (url == null || url == '') {
            url = "404.html";
        };
        if (w == null || w == '') {
            w = 800;
        };
        if (h == null || h == '') {
            h = ($(window).height() - 50);
        };
        layer.open({
            type: 2,
            area: [w + 'px', h + 'px'],
            fix: false, //不固定
            maxmin: true,
            shade: 0.4,
            shift: 5,
            title: title,
            content: url
        });
    },
    //6.0关闭弹出框口
    layer_close: function () {
        var index = parent.layer.getFrameIndex(window.name);
        parent.layer.close(index);
    },
    //7.0 封装统一判断服务器响应回来的具体状态值，进行不同的操作
    checkStatus: function (ajaxobj, callbackFun) {
        //ajaxobj格式:{status=0/1/2,msg=""}
        if (ajaxobj.status == "1")//error
        {
            helper.errorTip(ajaxobj.msg);
        } else if (ajaxobj.status == "2")//未登录
        {
            //提醒用户未登录,用户点击确定按钮跳转到登录页
            helper.warnTip(ajaxobj.msg, "tip", function () { window.top.location = "/admin/login/login"; });
            //2秒钟以后自动跳转到登录页
            setTimeout(function () { window.top.location = "/admin/login/login"; }, 2000);
        } else if (ajaxobj.status == "0") {
            callbackFun();//成功以后，回调特定的逻辑
        } else {
            helper.errorTip("未知错误,请确认js属性名称是否存在");
        }
    },
    //8.0新增和编辑的回调函数封装
    ajaxsuccess: function (ajaxobj) {
        //ajaxobj格式:{status=0/1/2,msg=""}
        helper.checkStatus(ajaxobj, function () {
            //1.0 刷新父页中的列表
            window.parent.grid.reload();

            //2.0 关闭当前的新增面板
            window.parent.helper.win.close();
        });
    },
    //9.0 datatables删除用户信息判断
    deleteItems: function (selectedItems) {
        if (selectedItems && selectedItems.length) {
            if (selectedItems.length == 1) {
                layer.confirm("确定要删除 '" + selectedItems[0].userName + "' 吗?", {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    layer.msg('的确很重要', { icon: 1 });
                });

            } else {
                layer.confirm("确定要删除选中的" + selectedItems.length + "项记录吗?", {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    layer.msg('的确很重要', { icon: 2 });
                });
            }
        } else {
            helper.layer_warning('请先选中要操作的行');
        }
    }
}