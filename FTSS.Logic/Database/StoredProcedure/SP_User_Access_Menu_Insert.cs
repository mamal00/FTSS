using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
	public class SP_User_Access_Menu_Insert
	{
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
          Models.Database.StoredProcedures.SP_User_Access_Menu_Insert_Params data)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Access_Menu_Insert(connectionString);
            data.Token = JWT.GetUserModel().User.Token;
            var rst = await sp.Call(data);
            return rst;
        }
    }
}
