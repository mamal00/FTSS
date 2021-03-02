using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_Users_GetAll
    {
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
            Models.Database.StoredProcedures.SP_Users_GetAll_Params filterParams)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Users_GetAll(connectionString);
            var rst =await sp.Call(filterParams);
            return rst;
        }
    }
}
