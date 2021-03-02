using System.Collections.Generic;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Log_Insert : ISP<string>
    {
        private readonly string _cns;

        public SP_Log_Insert(string cns)
        {
            _cns = cns;
        }

        /// <summary>
        /// Calling 'SP_Log_Insert' stored procedure
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<DBResult> Call(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                throw new Exception("SP_Log_Insert.Call need a text message as parameter");

            string sql = "dbo.SP_Log_Insert";
            //int PersonelId = -1;
            string IPAddress = "test";

            using (var connection = new SqlConnection(_cns))
            {
               await connection.ExecuteAsync(sql,
                    new 
                    {                        
                        IPAddress,
                        MSG = msg 
                    }, 
                    commandType: System.Data.CommandType.StoredProcedure);
            }

            return new DBResult(0, "");
        }
    }
}
