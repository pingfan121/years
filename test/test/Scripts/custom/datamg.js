
function getstrdata(key)
{
    return localStorage.getItem(key);
}
function setstrdata(key,data) {
    localStorage.setItem(key,data);
}

//设置数组数据
function getarrdata(key)
{
    var str = localStorage.getItem(key);

    if (str == "" || str == undefined || str == null)
    {
        var arr=new Array();

        return arr;
    }
    
    try
    {
        return JSON.parse(str);
    }
    catch(ex)
    {
        var arr=new Array();

        return arr;
    }
   
}
//设置
function setarrdata(key, data)
{
    var str = JSON.stringify(data);

    localStorage.setItem(key, str);
}