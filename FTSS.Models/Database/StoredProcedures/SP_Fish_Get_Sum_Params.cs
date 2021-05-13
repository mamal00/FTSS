using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_Fish_Get_Sum_Params: BaseModel
	{
		public string Codemeli { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }

	}
}
