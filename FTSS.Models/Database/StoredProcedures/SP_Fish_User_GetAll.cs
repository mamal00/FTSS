using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_Fish_User_GetAll
	{
		public int FishId { get; set; }
		public int? FishType { get; set; }
		public DateTime? FishDate { get; set; }
		public DateTime? IssuanceDate { get; set; }
		public string AccountNumber { get; set; }
		public decimal? InsurancePrice { get; set; }
		public decimal? WritPrice { get; set; }
		public decimal? ExtraSum { get; set; }
		public decimal? DeductionSum { get; set; }
		public decimal? Payment { get; set; }
		public decimal? TaxPrice { get; set; }
		public int? AcceptUserId { get; set; }
		public string InsuranceBranch { get; set; }
		public string BankName { get; set; }
		public string BranchName { get; set; }
		public string AccountName { get; set; }
		public string InsuranceNumber { get; set; }
	}
}
