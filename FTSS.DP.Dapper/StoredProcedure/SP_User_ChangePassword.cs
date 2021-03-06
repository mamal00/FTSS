using Dapper;
using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
	public class SP_User_ChangePassword : ISP<Models.Database.StoredProcedures.SP_User_ChangePassword_Params>
	{
        private readonly string _cns;

        public SP_User_ChangePassword(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_User_ChangePassword_Params data)
        {
            string sql = "dbo.SP_User_ChangePassword";
            DBResult rst = null;
            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetDataParams(data);
                p.AddDynamicParams(Common.GenerateParams(data, new List<string> {"Token" }));
                var dbResult = await connection.QueryFirstAsync<Models.Database.BaseIdModel>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);
                rst = Common.GetResult(p, dbResult);
            }
            return rst;
        }
    }
}
