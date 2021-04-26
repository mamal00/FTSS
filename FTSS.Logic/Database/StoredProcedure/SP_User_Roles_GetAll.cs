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
	public class SP_User_Roles_GetAll
	{
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
              Models.Database.StoredProcedures.SP_User_Roles_GetAll_Params data)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Roles_GetAll(connectionString);
            data.Token = JWT.GetUserModel().User.Token;
            var rst = await sp.Call(data);
            return rst;
        }
    }
}
