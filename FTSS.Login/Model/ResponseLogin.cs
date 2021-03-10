using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.Login.Model
{
	public class ResponseLogin
	{
		public string data { get; set; }
		public int? actualSize { get; set; }
		public int? errorCode { get; set; }
		public string errorMessage { get; set; }
	}
}
