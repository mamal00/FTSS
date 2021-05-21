using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.Login.Model
{
	public class ResponseLogin
	{
		public LoginModel data { get; set; }
		public int? actualSize { get; set; }
		public int? errorCode { get; set; }
		public string errorMessage { get; set; }
	}
	public class LoginModel
	{
		public string token { get; set; }
		public string Prs_no { get; set; }
		public string Mobile { get; set; }
		public string Codemeli { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
