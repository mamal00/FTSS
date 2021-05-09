using FTSS.Logic.Security;
using FTSS.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
	public class SP_Roles_GetAll
	{
        public static async Task<DBResult> Call(IDBCTX ctx,BaseModel data, string key, string issuer)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Roles_GetAll(connectionString);
            data.Token = JWT.GetUserModel(key,issuer).User.Token;
            var rst = await sp.Call(data);
            return rst;
        }
    }
}
