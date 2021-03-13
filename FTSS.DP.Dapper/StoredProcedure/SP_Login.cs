using System.Collections.Generic;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Login : ISP<Models.Database.StoredProcedures.SP_Login_Params>
    {
        private readonly ISQLExecuter _ISQLExecuter;
        public SP_Login(string cns, ISQLExecuter ISQLExecuter = null)
        {

            if (string.IsNullOrEmpty(cns))
                throw new ArgumentNullException("Could not create a new SP_Log_Insert instance with empty connectionString");
            if (ISQLExecuter == null)
                _ISQLExecuter = new SQLExecuter(cns);
            else
                _ISQLExecuter = ISQLExecuter;
        }

        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_Login_Params filterParams)
        {
                if (filterParams == null)
                    throw new ArgumentNullException("خطا در نحوه ارسال درخواست");
                string sql = "dbo.SP_Login";
                DBResult rst = null;
                    var p = Common.GetSearchParams();
                    p.AddDynamicParams(Common.GenerateParams(filterParams, null));
                    var dbResult = await _ISQLExecuter.QueryFirstOrDefaultAsync<Models.Database.StoredProcedures.SP_Login>(
                        sql, p, commandType: System.Data.CommandType.StoredProcedure);
                    rst = Common.GetResult(p, dbResult);
                return rst;
        }
    }
}