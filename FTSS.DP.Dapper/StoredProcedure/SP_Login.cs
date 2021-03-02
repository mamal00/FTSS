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
        private readonly string _cns;

        public SP_Login(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_Login_Params filterParams)
        {
			try
			{
                string sql = "dbo.SP_Login";
                DBResult rst = null;

                using (var connection = new SqlConnection(_cns))
                {
                    var p = Common.GetSearchParams();
                    p.AddDynamicParams(Common.GenerateParams(filterParams, null));
                    var dbResult = await connection.QueryFirstOrDefaultAsync<Models.Database.StoredProcedures.SP_Login>(
                        sql, p, commandType: System.Data.CommandType.StoredProcedure);

                    rst = Common.GetResult(p, dbResult);
                }

                return rst;
            }
            catch(Exception ex)
			{
                throw new Exception(ex.Message);
			}
        }
    }
}