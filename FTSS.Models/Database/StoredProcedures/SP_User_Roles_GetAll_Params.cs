using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	 public class SP_User_Roles_GetAll_Params:BaseModel
	{
		public int? UserId { get; set; }
		public int? RoleId { get; set; }
	}
}
