using Dapper;
using FTSS.Models.Database;
using FTSS.Models.Database.Tables;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
	public class SP_User_Update: ISP<Users>
	{
        private readonly string _cns;

        public SP_User_Update(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Users data)
        {
            string sql = "dbo.SP_User_Update";
            DBResult rst = null;
            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetDataParams(data);
                p.AddDynamicParams(Common.GenerateParams(data, new List<string> { "Password", "Token" }));
                var dbResult = await connection.QueryFirstAsync<Models.Database.BaseIdModel>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);
                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
    }
}
