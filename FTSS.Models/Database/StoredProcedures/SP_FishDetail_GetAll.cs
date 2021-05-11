using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_FishDetail_GetAll
	{
		public int? FishDetailId { get; set; }
		public decimal? FishValue { get; set; }
		public int? SabetId_FishValue { get; set; }
		public string TitleSabet { get; set; }
		public int? FishId { get; set; }
	}
}
