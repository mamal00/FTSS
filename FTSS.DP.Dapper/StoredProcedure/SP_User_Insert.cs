using Dapper;
using FTSS.Models.Database;
using FTSS.Models.Database.Tables;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_Insert : ISP<Models.Database.Tables.Users>
    {
        private readonly string _cns;

        public SP_User_Insert(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Users data)
        {
            string sql = "dbo.SP_User_Insert";
            DBResult rst = null;
            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetDataParams(data);
                p.AddDynamicParams(Common.GenerateParams(data, new List<string>{"UserId" }));
                var dbResult =await connection.QueryFirstAsync<Models.Database.StoredProcedures.SP_User_Insert>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);
                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
    }
}
