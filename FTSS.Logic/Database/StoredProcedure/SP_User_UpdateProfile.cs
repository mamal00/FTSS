using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
	public class SP_User_UpdateProfile
	{
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
          Models.Database.Tables.Users data)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_UpdateProfile(connectionString);
            data.Token = JWT.GetUserModel().User.Token;
            var rst = await sp.Call(data);
            return rst;
        }
    }
}
