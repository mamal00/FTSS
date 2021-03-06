using Dapper;
using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
	public class SP_Users_Get : ISP<Models.Database.StoredProcedures.SP_Users_Get_Params>
	{
        private readonly string _cns;

        public SP_Users_Get(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_Users_Get_Params filterParams)
        {

            string sql = "dbo.SP_Users_Get";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams(filterParams.Token);
                p.AddDynamicParams(Common.GenerateParams(filterParams, new List<string> { "Token"}));
                var dbResult = await connection.QueryFirstOrDefaultAsync<Models.Database.StoredProcedures.SP_Users_Get>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);
                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
    }
}
