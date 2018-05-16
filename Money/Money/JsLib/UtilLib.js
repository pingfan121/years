

//获取C#传值是 DateTime格式的数据的时候解析时间函数
var ul_getTimeText = function getTimeText(time) {

    var timestamp = Number(time.replace(/\/Date\((\d+)\)\//, "$1"));

    var oDate = new Date(parseInt(timestamp)),
        oYear = oDate.getFullYear(),
        oMonth = oDate.getMonth() + 1,
        oDay = oDate.getDate(),
        oHour = oDate.getHours(),
        oMin = oDate.getMinutes(),
        oSen = oDate.getSeconds(),
        oTime = oYear + '-' + getzf(oMonth) + '-' + getzf(oDay) + ' ' + getzf(oHour) + ':' + getzf(oMin) + ':' + getzf(oSen);//最后拼接时间  
    return oTime;
};

//补0操作,当时间数据小于10的时候，给该数据前面加一个0  
function getzf(num) {
    if (parseInt(num) < 10) {
        num = '0' + num;
    }
    return num;
}  