using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
	public class SP_Fish_User_GetAll
	{
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
Models.Database.StoredProcedures.SP_Fish_GetAll_Params filterParams, string key, string issuer)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Fish_GetAll(connectionString);
            filterParams.Token = JWT.GetUserModel(key,issuer).User.Token;
            filterParams.UserId = JWT.GetUserModel(key,issuer).User.UserId;
            var rst = await sp.Call(filterParams);
            return rst;
        }
    }
}
