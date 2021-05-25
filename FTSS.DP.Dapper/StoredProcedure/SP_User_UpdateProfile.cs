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
	public class SP_User_UpdateProfile : ISP<Models.Database.Tables.Users>
	{
        private readonly string _cns;

        public SP_User_UpdateProfile(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Users data)
        {
            string sql = "dbo.SP_User_UpdateProfile";
            DBResult rst = null;
            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetDataParams(data);
                p.AddDynamicParams(Common.GenerateParams(data, new List<string> { "UserId", "Email", "Prs_No", "Mobile", "Codemeli", "Password", "Token" }));
                var dbResult = await connection.QueryFirstOrDefaultAsync<Models.Database.BaseIdModel>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);
                rst = Common.GetResult(p, dbResult);
            }
            return rst;
        }
    }
}
