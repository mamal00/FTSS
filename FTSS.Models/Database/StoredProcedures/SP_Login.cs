using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Login : BaseModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
		public string Email { get; set; }
		public string Codemeli { get; set; }

		/// <summary>
		/// Database token expiration
		/// </summary>
		public DateTime ExpireDate { get; set; }
    }
}
