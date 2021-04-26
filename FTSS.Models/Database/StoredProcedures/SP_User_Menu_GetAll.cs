using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_User_Menu_GetAll
	{
		public int? MenuId{ get; set; }
		public string MenuAddress { get; set; }
		public int? MenuId_Parent { get; set; }
		public string MenuTitle { get; set; }
		public bool? IsAccess { get; set; }
	}
}
