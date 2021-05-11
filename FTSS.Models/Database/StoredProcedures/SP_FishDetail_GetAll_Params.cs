using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_FishDetail_GetAll_Params: BaseModel
	{
		public int? FishId { get; set; }
	}
}
