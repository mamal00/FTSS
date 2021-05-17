using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
	public class SP_Admin_FishDetail_GetAll
	{
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
Models.Database.StoredProcedures.SP_FishDetail_GetAll_Params filterParams, string key, string issuer)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Admin_FishDetail_GetAll(connectionString);
            filterParams.Token = JWT.GetUserModel(key, issuer).User.Token;
            var rst = await sp.Call(filterParams);
            return rst;
        }
        public static async Task<Models.Database.DBResult> CallReport(IDBCTX ctx,
Models.Database.StoredProcedures.SP_FishDetail_GetAll_Params filterParams, string key, string issuer)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Admin_FishDetail_GetAll(connectionString);
            filterParams.Token = JWT.GetUserModel(key, issuer).User.Token;
            var rst = await sp.CallReport(filterParams);
            return rst;
        }
    }
}
