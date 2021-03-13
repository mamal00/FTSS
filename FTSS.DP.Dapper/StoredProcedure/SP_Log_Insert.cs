using System.Collections.Generic;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Log_Insert : ISP<Models.Database.StoredProcedures.SP_Log_Insert_Params>
    {
        private readonly ISQLExecuter _ISQLExecuter;
        private static IHttpContextAccessor context;
        public SP_Log_Insert(string cns,ISQLExecuter ISQLExecuter=null)
        {
            if (string.IsNullOrEmpty(cns))
                throw new ArgumentNullException("Could not create a new SP_Log_Insert instance with empty connectionString");
            if (ISQLExecuter == null)
                _ISQLExecuter = new SQLExecuter(cns);
            else
                _ISQLExecuter = ISQLExecuter;
            context = new HttpContextAccessor();
        }
      
        /// <summary>
        /// Calling 'SP_Log_Insert' stored procedure
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_Log_Insert_Params model)
        {
            if (model==null || string.IsNullOrEmpty(model.Msg))
                throw new Exception("SP_Log_Insert.Call Error in Send Parameter");

            string sql = "dbo.SP_Log_Insert";
            string ip ="127.0.0.1";
            if (context!=null && context.HttpContext!=null)
             ip= context.HttpContext.Connection.RemoteIpAddress.ToString();
                var result = await _ISQLExecuter.QueryFirstOrDefaultAsync<OutputIdModel>(sql,
        new
        {
            IPAddress = ip,
            MSG = model.Msg
        }, System.Data.CommandType.StoredProcedure);
                return new DBResult(200, "", result);
    
        }
    }
}
