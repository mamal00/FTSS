using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
	public class SP_Log_Insert_Params
	{
		public SP_Log_Insert_Params(string msg)
		{
			Msg = msg;
		}
	
		public SP_Log_Insert_Params()
		{

		}
		public string Msg { get; set; }
	}
}
