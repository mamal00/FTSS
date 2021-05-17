using Dapper;
using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
	public class SP_FishDetail_GetAll : ISP<Models.Database.StoredProcedures.SP_FishDetail_GetAll_Params>
	{
        private readonly string _cns;
        public SP_FishDetail_GetAll(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_FishDetail_GetAll_Params filterParams)
        {
            string sql = "dbo.SP_FishDetail_GetAll";
            DBResult rst = null;
            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams(filterParams.Token);
                p.AddDynamicParams(Common.GenerateParams(filterParams, new List<string> { "Token"}));
                var dbResult = await connection.QueryAsync<Models.Database.StoredProcedures.SP_FishDetail_GetAll>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);
                rst = Common.GetResult(p, dbResult);
            }
            return rst;
        }
        public async Task<DBResult> CallMobile(Models.Database.StoredProcedures.SP_FishDetail_GetAll_Params filterParams)
        {
            string sql = "dbo.SP_FishDetail_GetAll";
            string sqlFishUser = "dbo.SP_Fish_User_Get";
            DBResult rst = null;
            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams(filterParams.Token);
                p.AddDynamicParams(Common.GenerateParams(filterParams, new List<string> { "Token" }));
                var pFishUser = Common.GetSearchParams(filterParams.Token);
                pFishUser.AddDynamicParams(Common.GenerateParams(new { Id = filterParams.FishId }, new List<string> { "Token" }));
                var dbResult = await connection.QueryAsync<Models.Database.StoredProcedures.SP_FishDetail_GetAll>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);
                var dbResultFishUser = await connection.QueryFirstOrDefaultAsync<Models.Database.StoredProcedures.SP_Fish_User_Get>(
                    sqlFishUser, pFishUser, commandType: System.Data.CommandType.StoredProcedure);

                var dto = dbResult.Select(model => new Models.Database.StoredProcedures.SP_FishDetailMobile_GetAll()
                {
                    Baghimande=model.Baghimande,
                    CodeSabet=model.CodeSabet,
                    FishItemId=model.FishItemId,
                    Mablagh=model.Mablagh,
                    Noe=model.Noe,
                    SabetId_FishValue=model.SabetId_FishValue,
                    TitleSabet=model.TitleSabet
                }).Cast<Models.Database.StoredProcedures.SP_FishDetailMobile_GetAll>().ToList();
                 dbResultFishUser.FishDetailList=dto;
                rst = Common.GetResult(p, dbResultFishUser);
            }
            return rst;
        }
    }
}
