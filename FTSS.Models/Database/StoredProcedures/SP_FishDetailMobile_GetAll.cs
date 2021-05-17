using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_FishDetailMobile_GetAll
	{
		public int? FishItemId { get; set; }
		public int? Noe { get; set; }
		public Int64? Mablagh { get; set; }
		public Int64? Baghimande { get; set; }
		public string TitleSabet { get; set; }
		public int? SabetId_FishValue { get; set; }
		public string CodeSabet { get; set; }
	}
}
