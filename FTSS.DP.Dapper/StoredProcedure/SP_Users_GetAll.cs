﻿using Dapper;
using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_Users_GetAll : ISP<Models.Database.StoredProcedures.SP_Users_GetAll_Params>
    {
        private readonly string _cns;

        public SP_Users_GetAll(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_Users_GetAll_Params filterParams)
        {
     
            string sql = "dbo.SP_Users_GetAll";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams(filterParams);
                p.AddDynamicParams(Common.GenerateParams(filterParams,new List<string> { "Token", "PageSize", "StartIndex","Sort" }));
                var dbResult =await connection.QueryAsync<Models.Database.StoredProcedures.SP_Users_GetAll>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);
                rst = Common.GetResult(p, dbResult);
            }
            return rst;
        }
    }
}