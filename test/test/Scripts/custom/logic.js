

var grids;
function init2(num,init_grids)
{
    grids = init_grids;

    var flag = false;

    if (grids.length == 0)
    {
        for (var i = 0; i < num; i++)
        {
            grids[i] = new Array();

            for (var k = 0; k < num; k++)
            {
                var o = new Object();
                o.num = 0;
                o.merge = false;
                grids[i][k] = o;
            }
        }
        addGrid();
        addGrid();
    }
}

function left_move() {
    //首先 将所有的格子移动到右边去
    var oper1 = false;
    var oper2 = false;

    for (var i = 0; i < grids.length; i++) {
        var sortlist = [];

        for (var k = 0; k < grids[i].length; k++) {
            sortlist.push(grids[i][k]);
        }

        oper1 = sort(sortlist);

        if (oper1 == true) {
            oper2 = true;
        }
    }
    return oper2;
}

function right_move() {
    //首先 将所有的格子移动到右边去
    var oper1 = false;
    var oper2 = false;

    for (var i = 0; i < grids.length; i++) {
        var sortlist = [];

        for (var k = grids[i].length - 1; k >= 0; k--) {
            sortlist.push(grids[i][k]);
        }

        oper1 = sort(sortlist);

        if (oper1 == true) {
            oper2 = true;
        }
    }
    return oper2;
}

function up_move() {
    //首先 将所有的格子移动到右边去
    var oper1 = false;
    var oper2 = false;

    for (var i = 0; i < grids.length; i++) {
        var sortlist = [];

        for (var k = 0; k < grids[i].length; k++) {
            sortlist.push(grids[k][i]);
        }

        oper1 = sort(sortlist);

        if (oper1 == true) {
            oper2 = true;
        }
    }
    return oper2;
}

function down_move() {
    //首先 将所有的格子移动到右边去
    var oper1 = false;
    var oper2 = false;


    for (var i = 0; i < grids.length; i++) {
        var sortlist = [];

        for (var k = grids[i].length - 1; k >= 0; k--) {
            sortlist.push(grids[k][i]);
        }

        oper1 = sort(sortlist);

        if (oper1 == true) {
            oper2 = true;
        }
    }
    return oper2;
}

function sort(arr) {
    var oper1 = false;
    var oper2 = false;
    var oper3 = false;


    while (true) {
        oper1 = moveGrid(arr);
        oper2 = mergeGrid(arr);

        if (oper1 == true || oper2 == true) {
            oper3 = true;
        }

        if (oper1 == false && oper2 == false) {
            break;
        }
    }
    return oper3;
}

//得到空格位置
function moveGrid(arr) {
    var oper = false;

    var index = getVan(arr);

    if (index == -1)
        return false;

    for (var i = 0; i < arr.length; i++) {
        var grid = arr[i];
        if (grid.num != 0) {
            if (i > index) {
                arr[index].num = arr[i].num;
                arr[i].num = 0;

                index = getVan(arr);

                oper = true;
            }
        }
    }

    return oper;
}
//合并格子
function mergeGrid(arr) {
    var oper = false;

    for (var i = 0; i < arr.length - 1; i++) {
        if (arr[i].num == arr[i + 1].num && arr[i].num != 0 && arr[i].merge == false && arr[i + 1].merge == false) {
            arr[i].num += arr[i].num;
            arr[i + 1].num = 0;
            arr[i].merge = true;
            oper = true;
        }
    }
    return oper;
}

//移动位置
function getVan(arr) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].num == 0) {
            return i;
        }
    }
    return -1;
}

function addGrid() {
    var sortlist = [];

    for (var i = 0; i < grids.length; i++) {
        for (var k = 0; k < grids[i].length; k++) {

            if (grids[i][k].num == 0) {
                sortlist.push(grids[i][k]);
            }

        }
    }
    var rand = Math.round(Math.random() * (sortlist.length - 1));
    var grid = sortlist[rand];
    grid.num = 2;
}

//检测游戏是否结束
function isOver()
{
    for (var i = 0; i < grids.length; i++) {
        for (var k = 0; k < grids[i].length; k++) {
            if (grids[i][k].num == 0) {
                return false;
            }
        }
    }

    //判断相邻两个是否相同

    for (var i = 0; i < grids.length; i++) {
        for (var k = 0; k < grids[i].length - 1; k++) {
            if (grids[i][k].num == grids[i][k + 1].num) {
                return false;
            }
        }
    }

    for (var i = 0; i < grids.length - 1; i++) {
        for (var k = 0; k < grids[i].length; k++) {
            if (grids[i][k].num == grids[i + 1][k].num) {
                return false;
            }
        }
    }

    return true;
}


//是否有合并
function haveMerge()
{
    for (var i = 0; i < grids.length; i++)
    {
        for (var k = 0; k < grids[i].length; k++)
        {
            if (grids[i][k].merge == true)
            {
                return true;
            }
        }
    }
    return false
}
//清理合并标志
function cleanMerge()
{
    for (var i = 0; i < grids.length; i++)
    {
        for (var k = 0; k < grids[i].length; k++)
        {
            grids[i][k].merge = false;
        }
    }
}

function getCurrScore() {
    var curr = 0;

    for (var i = 0; i < grids.length; i++) {
        for (var k = 0; k < grids[i].length; k++) {
            curr += grids[i][k].num;
        }
    }

    return curr;
}


//public void setSoundFlag(boolean flag)
// {
//   sound_flag = flag;
// }

 function getdirection(orgPos, dirPos)
 {
    var TTpos = {};

    TTpos.x = dirPos.x - orgPos.x;
    TTpos.y = dirPos.y - orgPos.y;

    var length = Math.max(Math.abs(TTpos.x), Math.abs(TTpos.y));

    if (length < 5) {
        return "";
    }


    var tanx = Math.atan2(TTpos.y, TTpos.x);

    var degrees = (180 / Math.PI) * tanx;

    while (degrees > 360) {
        degrees -= 360;
    }

    while (degrees < 0) {
        degrees += 360;
    }

    //角度得到了...怎么算方向
    if (degrees > 315 || degrees <= 45)   //朝右
    {
        return "right";
    }

    if (degrees > 45 && degrees <= 135)   //朝下
    {
        return "down";
    }

    if (degrees > 135 && degrees <= 225)   //朝左
    {
        return "left";
    }

    if (degrees > 225 && degrees <= 315)   //朝上
    {
        return "up";
    }

    return "";

}
