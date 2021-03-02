using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_User_GetAccessMenu
    {
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
            Models.Database.StoredProcedures.SP_Login userInfo)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_GetAccessMenu(connectionString);
            var rst =await sp.Call(userInfo);
            return rst;
        }
    }
}
