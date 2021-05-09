using FTSS.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Security
{
    public class UserInfo
    {
        public readonly Logic.Database.IDBCTX _ctx;

        public UserInfo(Logic.Database.IDBCTX ctx)
        {
            this._ctx = ctx;
        }

        public UserInfo()
        {
            this.User = new Models.Database.StoredProcedures.SP_Login();
            this.AccessMenu = new List<Models.Database.StoredProcedures.SP_User_GetAccessMenu>();
        }
		#region properties
		public string Email { get; set; }
		public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ExpireDate { get; set; }
		public string Token { get; set; }
		public string accessMenuJson { get; set; }
		#endregion
		public string Username { get; set; }

        /// <summary>
        /// User information
        /// </summary>
        public Models.Database.StoredProcedures.SP_Login User { get; set; }

        /// <summary>
        /// Application menu and restful APIs
        /// </summary>
        public List<Models.Database.StoredProcedures.SP_User_GetAccessMenu> AccessMenu { get; set; }

     
    }
}
