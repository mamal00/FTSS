﻿using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
	public class SP_User_Menu_GetAll
	{
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx,
Models.Database.BaseIdModel filterParams)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Menu_GetAll(connectionString);
            filterParams.Token = JWT.GetUserModel().User.Token;
            var rst = await sp.Call(filterParams);
            return rst;
        }
    }
}
