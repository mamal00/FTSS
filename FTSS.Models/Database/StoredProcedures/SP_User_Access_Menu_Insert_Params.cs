using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_User_Access_Menu_Insert_Params:BaseModel
	{
		public int UserId { get; set; }
		public string MenuIdJson { get; set; }
	}
}
