using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
	public class SP_User_ChangePassword
	{
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
     Models.Database.StoredProcedures.SP_User_ChangePassword_Params data, string key, string issuer)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_ChangePassword(connectionString);
            data.Token = JWT.GetUserModel(key,issuer).User.Token;
            var rst = await sp.Call(data);
            return rst;
        }
    }
}
