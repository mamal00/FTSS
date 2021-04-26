using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_User_Roles_GetAll
	{
		public int? RoleId { get; set; }
		public string RoleTitle { get; set; }
		public int? UserId { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? UserRoleId { get; set; }
	}
}
