using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_Fish_GetAll_Params: BaseSearchParams
	{
		public string Codemeli { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
