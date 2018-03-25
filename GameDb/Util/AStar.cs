using System;
using System.Collections.Generic;
using System.Text;
using GameLib.Mathe;

namespace GameLib.Util
{
	public class Grid
	{
		public int i;
		public int j;
		public int w;
		public int h;
		public string name;

		//寻路有关。。。
		public Dictionary<Grid, Vector> links = new Dictionary<Grid, Vector>();
		public double cost = 0;//已经走了的代价
		public double precost = 0;//预计的代价
		public int num = 0;//寻路的标记
		//
		public Grid pre;
		public Vector bian;//来源

		public Grid(int _si, int _sj)
		{
			i = _si;
			j = _sj;
			name = i + "_" + j;
		}
		public void addlink(Grid gr, Vector pt)
		{
			links[gr] = pt;
		}
	}
	public class Topology
	{
		public Grid[,] grids;//2纬数组，保存了这个格子对应的Grid信息。 
		int rows;
		int cols;
		//
		byte[,] maparr;
		AStar astar;
		//
		public Topology(AStar _astar)
		{
			astar = _astar;
			//获得未分配区域的最大网格
			maparr = astar.mapdata;
			rows = maparr.GetUpperBound(0);
			cols = maparr.GetUpperBound(1);
			//
			grids = new Grid[rows, cols];
			List<Grid> arr = new List<Grid>();
			for (int i = 0; i < rows; i++)
			{
				for (var j = 0; j < cols; j++)
				{
					if (maparr[i, j] != 1 && grids[i, j] == null)
					{
						//开始计算这个网格
						Grid gd = cal(i, j);
						arr.Add(gd);
					}
				}
			}
			//求出联通边，获得联通图
			for (int i = 0; i < arr.Count - 1; i++)
			{
				for (int j = i + 1; j < arr.Count; j++)
				{
					Grid s = arr[i];
					Grid e = arr[j];
					Vector tp;
					if (s.i + s.h + 1 == e.i || e.i + e.h + 1 == s.i)
					{
						//trace("上下有可能相连");//找出相连的边
						var right = Math.Min(s.j + s.w, e.j + e.w) + 1;
						var left = Math.Max(s.j, e.j);
						//-----------
						if (right > left)
						{
							//trace(s.name,e.name)
							//trace("上下连,共享边"+left,right);
							var ttt = (s.i + s.h + 1 == e.i) ? e.i : s.i;
							tp = new Vector((right + left) / 2, ttt);
							s.addlink(e, tp);
							e.addlink(s, tp);
						}
					}
					else if (s.j + s.w + 1 == e.j || e.j + e.w + 1 == s.j)
					{
						//trace("左右有可能相连");
						var down = Math.Min(s.i + s.h, e.i + e.h) + 1;
						var top = Math.Max(s.i, e.i);
						//-----------
						if (down > top)
						{
							//trace(s.name,e.name)
							var ttt = (s.j + s.w + 1 == e.j) ? e.j : s.j;
							//trace("左右连,共享边"+top,down);
							tp = new Vector(ttt, (top + down) / 2);
							s.addlink(e, tp);
							e.addlink(s, tp);
						}
					}
				}
			}
		}

		private Grid cal(int si, int sj)
		{
			Grid grid = new Grid(si, sj);
			int w = 0;
			int h = 0;
			bool addw = true;//能否向右侧扩展
			bool addh = true;//能否向下侧扩展
			while (true)
			{
				if (sj + w + 1 < cols)
				{
					addw = true;
				}
				else
				{
					addw = false;
				}
				if (addw)
				{
					for (var i = si; i <= si + h; i++)
					{
						if (maparr[i, sj + w + 1] == 1 || grids[i, sj + w + 1] != null)
						{//不可行走
							addw = false;
							break;//存在障碍
						}
					}
				}
				if (addw)
				{
					w++;
				}
				//-------------------------------------
				if (si + h + 1 < rows)
				{
					addh = true;
				}
				else
				{
					addh = false;
				}
				if (addh)
				{
					for (var j = sj; j <= sj + w; j++)
					{
						if (maparr[si + h + 1, j] == 1 || grids[si + h + 1, j] != null)
						{//不可行走
							addh = false;
							break;//存在障碍
						}
					}
				}
				if (addh)
				{
					h++;
				}

				if (addw || addh)
				{
					if ((w + 1) / (h + 1) > 10 || (w + 1) / (h + 1) < 1 / 10)
					{
						break;//形状不规则
					}
				}
				else
				{
					break;
				}
			}
			for (var i = si; i <= si + h; i++)
			{
				for (var j = sj; j <= sj + w; j++)
				{
					grids[i,j] = grid;
				}
			}
			grid.w = w;
			grid.h = h;
			return grid;
		}
	}
	public class AStar
	{
		public byte[,] mapdata;
		public const int tilew=48;//10个像素一个格子
		public const int tileh=32;//10个像素一个格子
		public AStar()
		{
		}

		public List<Vector> FindPath(Vector sp, Vector ep)
		{
			//寻路流程。。。
			/*var arr:Array=findpath(int(sp.x/tilew),int(sp.y/tilew),int(ep.x/tilew),int(ep.y/tilew));
			arr=arr.reverse();
			arr.push(new Vector(ep.x,ep.y));*/
			List<Vector> arr=findpath((int)ep.x,(int)ep.y,(int)sp.x,(int)sp.y);
			//arr.Insert(0,sp);
			//LOG.log("起点=>" + sp+ ",终点=>" + ep);
			/*if (arr != null)
			{
				for (int i = 0; i < arr.Count; i++)
				{
					LOG.log(arr[i]);
				}
			}*/
			return arr;
		}
		
		public void setMap(byte[,] arr){
			mapdata=arr;
			int rows=mapdata.GetUpperBound(0);
			int cols=mapdata.GetUpperBound(1);
			//
			for(int i=0;i<rows;i++){
				for(int j=0;j<cols;j++){
					if(i==0 || j==0 || i==rows-1 || j==cols-1){
						mapdata[i,j]=1;//边界限制为障碍
					}
				}
			}
			topology = new Topology(this);
		}
		LinkedList<Grid> open=new LinkedList<Grid>();
		int num=1;
		Grid stGrid;
		Grid edGrid;
		Topology topology;
		//
		private List<Vector> findpath(int sx,int sy,int ex,int ey){
			//drawLine2(sx,sy,ex,ey);
			//先找到第一个碰撞格子，如果这个格子还没有呗标注过，
			//
			//开始寻路
			//颠倒一下起点和终点
			stGrid=topology.grids[sy,sx];
			edGrid=topology.grids[ey,ex];
			if(edGrid==null || stGrid==null){
				return null;
			}
			edGrid.bian=new Vector(ex,ey);
			//开始寻找周边。。
			//trace(stGrid.name,edGrid.name);
			
			//var t=getTimer();
			open.Clear();
			num++;
			stGrid.bian=new Vector(sx,sy);
			stGrid.cost=0;
			stGrid.num=num;
			stGrid.pre=null;
			stGrid.precost=Vector.distance(stGrid.bian,edGrid.bian);
			//
			open.AddFirst(stGrid);
			while(open.First.Value!=null){
				Grid cnode=open.First.Value;
				open.RemoveFirst();
				if(edGrid==cnode){
					return getPath(edGrid);
				}
				foreach(Grid o in cnode.links.Keys){
					//
					double cost=cnode.cost+Vector.distance(cnode.bian,cnode.links[o]);
					if(o.num<num){
						o.num=num;
						o.cost=cost;
						o.pre=cnode;
						o.bian=cnode.links[o];
						o.precost=Vector.distance(cnode.links[o],edGrid.bian);
						insert(o);
					}else if(o.cost>cost){//插入
						o.cost=cost;
						o.pre=cnode;
						o.bian=cnode.links[o];
						insert(o);
					}
				}
			}
			return null;
		}
		List<Vector> getPath(Grid cnode){
			Grid tnode=cnode;
			List<Vector> arr=new List<Vector>();
			while(tnode!=null){
				arr.Add(new Vector(tnode.bian.x*AStar.tilew,tnode.bian.y*AStar.tileh));
				tnode=tnode.pre;
			}
			return arr;
		}
		
		private void insert(Grid o)
		{
			/*for(var i=0;i<open.length;i++){
				var gr:Grid=open[i] as Grid;
				if(o.cost+o.precost<gr.cost+gr.precost){
					open.splice(i,0,o);
					return;
				}
			}*/
			open.AddLast(o);
		}
		//
	}
}
