using System;
using System.Collections.Generic; 
using System.Text;  
using Easy4net.CustomAttributes; 
namespace GameDb.Logic  
{  
	 [Table(Name = "app_role")] 
	 public class TbAppRole:TbLogic
	 { 
		private int _id;
		[Id(Name = "id", Strategy = GenerationType.FILL)]
		public int Id{ 
			get{ return _id;}
			 set{_id=value;}
		} 

		private string _name;
		[Column(Name = "name")]
		public string Name{ 
			get{ return _name;}
			 set{if(_name==value)return;
			_name=value;
			changedKeys.Add("Name");}
		} 

		private string _pass;
		[Column(Name = "pass")]
		public string Pass{ 
			get{ return _pass;}
			 set{if(_pass==value)return;
			_pass=value;
			changedKeys.Add("Pass");}
		} 


       override public void copy(TbLogic tblogic) {
         if (tblogic == this)return;
         TbAppRole t=tblogic as TbAppRole;
			Name=t.Name;
			Pass=t.Pass;
       }	 } 
}    

