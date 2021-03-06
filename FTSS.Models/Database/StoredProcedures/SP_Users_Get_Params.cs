using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_Users_Get_Params:BaseModel
	{
		public int? UserId { get; set; }
	}
}
