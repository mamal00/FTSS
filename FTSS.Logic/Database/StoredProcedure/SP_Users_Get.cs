using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
	public class SP_Users_Get
	{
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
        Models.Database.StoredProcedures.SP_Users_Get_Params filterParams)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Users_Get(connectionString);
            filterParams.Token= JWT.GetUserModel().User.Token;
            var rst = await sp.Call(filterParams);
            return rst;
        }
        public static async Task<Models.Database.DBResult> CallProfile(IDBCTX ctx)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Users_Get(connectionString);
            var model =new Models.Database.StoredProcedures.SP_Users_Get_Params();
            model.Token = JWT.GetUserModel().User.Token;
            model.UserId = JWT.GetUserModel().User.UserId;
            var rst = await sp.Call(model);
            return rst;
        }
    }
}
