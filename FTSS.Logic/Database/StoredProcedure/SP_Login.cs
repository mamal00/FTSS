using FTSS.Logic.CommonOperations;
using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Database.StoredProcedure
{
    public class SP_Login
    {
	
        /// <summary>
        /// Calling SP_Login stored procedure in database
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public static async Task<Models.Database.DBResult> Call(IDBCTX ctx, AutoMapper.IMapper mapper,
            Models.Database.StoredProcedures.SP_Login_Params filterParams,string key,string issuer)
        {
            var connectionString = ctx.GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Login(connectionString);
            var rst =await sp.Call(filterParams);
            var spAccessMenu = new FTSS.DP.DapperORM.StoredProcedure.SP_User_GetAccessMenu(connectionString);
            var spAccessMenuResult=await spAccessMenu.Call(rst.Data as Models.Database.StoredProcedures.SP_Login);
            //Convert Teacher to User object
            var user = mapper.Map<UserInfo>(rst.Data);
            user.accessMenuJson= JSON.ObjToJson(spAccessMenuResult.Data);
            var response= JWT.GenerateToken(user,key,issuer);
            return response;
        }
    }
}