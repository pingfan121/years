
    var data = {
        count: 4,
        grids:[],
        colors: {},
        max_score: 0,
        curr_score: 0,
        point: {}

    }

    var canvas1;

    /**
     * 生命周期函数--监听页面加载
     */
    function onLoad(canvas)
    {
        canvas1 = canvas;
        initColor();
        onStart();
    }

    function initColor() {

        var colors = data.colors;

        colors.c0 = "#9d9087";
        colors.c2 = "#fedcbd";
        colors.c4 = "#dec674";
        colors.c8 = "#c7a252";
        colors.c16 = "#f26522";
        colors.c32 = "#dbce8f";
        colors.c64 = "#c99979";
        colors.c128 = "#de773f";
        colors.c256 = "#bed742";
        colors.c512 = "#da765b";
        colors.c1024 = "#fdb933";
        colors.c2048 = "#f173ac";
        colors.c4096 = "#f7acbc";
        colors.c8192 = "#ef5b9c";
        colors.c16384 = "#45b97c";
        colors.c32768 = "#f15b6c";
        colors.c65536 = "#f8aba6";
        colors.c131072 = "#f391a9";
        colors.c262144 = "#bd6758";
        colors.c524288 = "#d64f44";
        colors.c1048576 = "#c76968";
        colors.c2097152 = "#f26522";
        colors.c4194304 = "#78a355";
        colors.c8388608 = "#faa755";
        colors.c16777216 = "#fab27b";
        colors.c33554432 = "#426ab3";
        colors.c67108864 = "#ae6642";
        colors.c134217728 = "#9b95c9";
        colors.c268435456 = "#d5c59f";
        colors.c536870912 = "#df9464";
        colors.c1073741824 = "#87843b";
    }

//开始的点
    var s_point = {};
    function startPoint(point)
    {
        s_point = point;

    }

    var e_point = {};

    function endPoint(point)
    {
        e_point = point;

        operGame(s_point, e_point);
    }
   
    function onRestart()
    {
        var grids = [];

        if (data.count == 4) {
            setarrdata("grids_4", grids);
        }
        else {
            setarrdata("grids_6", grids);
        }

        onStart();
    }

    function onStart()
    {
        if (data.count == 4)
        {
            data.max_score = getstrdata('max_4');
            data.grids = getarrdata('grids_4');
        }
        else
        {
            data.max_score = getstrdata('max_6');
            data.grids = getarrdata('grids_6');
        }

        if (data.max_score == undefined || data.max_score == "") {
            data.max_score = 0;
        }

        if (data.grids == undefined || data.grids == null || data.grids == '') {
            data.grids = [];
        }
        //读取数据
        init2(data.count, data.grids);

        onUpdate();
    }
    function operGame(src, tag) {
        var dir = getdirection(src, tag)

        var oper = false;

        if (dir == "up") {
            oper = up_move();
        }
        if (dir == "down") {
            oper = down_move();
        }
        if (dir == "left") {
            oper = left_move();
        }
        if (dir == "right") {
            oper = right_move();
        }

        if (oper == true) {
            var merge_flag = haveMerge();

            if (merge_flag == true)
            {
                playsound(true);
                cleanMerge();
            }
            else {
                playsound(false);
            }

            addGrid();

        }
        onUpdate();

        if (isOver() == true) {
            alert("游戏结束");
        }
    }
    function onResetCount()
    {
        if (data.count == 4)
        {
            data.count = 6;
        }
        else
        {
            data.count = 4;
        }

        onStart();
    }
    function getCount()
    {
        return data.count;
    }

    function onUpdate()
    {
        onDraw();

        var s_max = "";
        var s_grids = "";

        if (data.count == 4) {
            s_max = "max_4";
            s_grids = "grids_4";
        }
        else {
            s_max = "max_6";
            s_grids = "grids_6";
        }

        var grids = data.grids;

        data.curr_score = getCurrScore();

        if (data.curr_score > data.max_score)
        {
            data.max_score = data.curr_score;
        }

        //保存最大分
        setstrdata(s_max, data.max_score);
        //保存格子数据
        setarrdata(s_grids, grids);

        //更新界面分数
        updateScore();

    }
    //画图
    function onDraw()
    {
        var canvas = canvas1.getContext('2d');
      
     
       
        var count =data.count;
        var width = canvas1.width;

        var gap = width/100;

        var grid_width = (width - (count + 1) * gap) / count;

        canvas.fillStyle = "#72777b";
        canvas.fillRect(0, 0, width, width);

        var grids = data.grids;

        for (var i = 0; i < grids.length; i++) {
            for (var k = 0; k < grids[i].length; k++) {

                var grid = grids[i][k];

                var starty = gap + i * grid_width + i * gap;
                var startx = gap + k * grid_width + k * gap;

                var temp = data.colors["c" + grid.num];

                canvas.fillStyle=temp;

                canvas.fillRect(startx, starty, grid_width, grid_width);

                if (grid.num != 0) {
                    if (grids[i][k].num < 100000)
                    {
                        var size = grid_width / 3;
                        canvas.font=size+"px Arial";
                    }
                    else if (grids[i][k].num < 10000000)
                    {
                        var size = grid_width / 4;
                        canvas.font = size + "px Arial";
                    }
                    else if (grids[i][k].num < 100000000)
                    {
                        var size = grid_width / 5;
                        canvas.font = size + "px Arial";
                    }
                    else if (grids[i][k].num < 2100000000)
                    {
                        var size = grid_width / 6;
                        canvas.font = size + "px Arial";
                    }

                    canvas.fillStyle="#000000";
                    canvas.textAlign = 'center';
                    canvas.textBaseline = 'middle';
                    canvas.fillText(grids[i][k].num + "", startx + grid_width / 2, starty + grid_width / 2);

                }
            }
        }

    }

    //更新分数
    function updateScore()
    {
        document.getElementById("cur_score").innerHTML = data.curr_score;
        document.getElementById("max_score").innerHTML = data.max_score;
    }

//播放声音
    var soundflag=true;

    function playsound(merge)
    {
        var myaudio = document.getElementById("myaudio");

        if (soundflag == true) {
            if (merge == true) {
                myaudio.src = "../sound/merge.wav";
            }
            else {
                myaudio.src = "../sound/move.wav";
            }

            myaudio.play();
        }
    }
