using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.Tables
{
    public class Users : Models.Database.BaseModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Prs_No { get; set; }
        public string Mobile { get; set; }
		public string Codemeli { get; set; }
	}
}
