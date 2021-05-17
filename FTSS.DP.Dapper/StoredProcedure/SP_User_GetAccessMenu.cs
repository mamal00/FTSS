﻿using System.Collections.Generic;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using FTSS.Models.Database;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
    public class SP_User_GetAccessMenu : ISP<Models.Database.StoredProcedures.SP_Login>
    {
        private readonly string _cns;

        public SP_User_GetAccessMenu(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_Login Data)
        {
            if (Data == null || string.IsNullOrEmpty(Data.Token))
                throw new Exception("SP_User_GetAccessMenu.Call can not be call without passing userInfo");

            string sql = "dbo.SP_User_GetAccessMenu";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams(Data.Token);

                var dbResult =await connection.QueryAsync<Models.Database.StoredProcedures.SP_User_GetAccessMenu>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);

                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
        public async Task<DBResult> CallAdmin(Models.Database.StoredProcedures.SP_Admin_Login Data)
        {
            if (Data == null || string.IsNullOrEmpty(Data.Token))
                throw new Exception("SP_User_GetAccessMenu.Call can not be call without passing userInfo");

            string sql = "dbo.SP_User_GetAccessMenu";
            DBResult rst = null;

            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams(Data.Token);

                var dbResult = await connection.QueryAsync<Models.Database.StoredProcedures.SP_User_GetAccessMenu>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);

                rst = Common.GetResult(p, dbResult);
            }

            return rst;
        }
    }
}