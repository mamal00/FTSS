using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_Fish_GetAll
	{
		public int FishId { get; set; }
		public string GhesmatName { get; set; }
		public string FishType { get; set; }
		public int? Sal { get; set; }
		public int? Mah { get; set; }
		public string HesabNo { get; set; }
		public string BankName { get; set; }
		public string BankShobe { get; set; }
		public string SodoorDate { get; set; }
		public Int64? Jam { get; set; }
		public Int64? JamKosoor { get; set; }
		public Int64? JamDaryafty { get; set; }
		public string JamDaryaftyHorof { get; set; }
		public string Date_ { get; set; }
		public int? Goroh { get; set; }
		public int? TedadAele { get; set; }
		public string Mantaghe { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Codemeli { get; set; }
	}
}
