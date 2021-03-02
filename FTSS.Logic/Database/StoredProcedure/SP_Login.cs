using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_Login
    {
        /// <summary>
        /// Calling SP_Login stored procedure in database
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
            Models.Database.StoredProcedures.SP_Login_Params filterParams)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Login(connectionString);
            var rst =await sp.Call(filterParams);
            return rst;
        }
    }
}