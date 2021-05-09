using Dapper;
using FTSS.DP.DapperORM;
using FTSS.Logic.Security;
using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
	public class SP_User_Roles_Delete 
	{
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
Models.Database.BaseIdModel param, string key, string issuer)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Delete(connectionString);
            param.Token = JWT.GetUserModel(key,issuer).User.Token;
            var rst = await sp.Call(param);
            return rst;
        }
    
    }
}
