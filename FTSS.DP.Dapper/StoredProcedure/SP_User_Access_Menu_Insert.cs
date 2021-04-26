using FTSS.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
	public class SP_User_Access_Menu_Insert
	{
        private readonly ISQLExecuter _ISQLExecuter;
        public SP_User_Access_Menu_Insert(string cns, ISQLExecuter ISQLExecuter = null)
        {

            if (string.IsNullOrEmpty(cns))
                throw new ArgumentNullException("Could not create a new SP_Log_Insert instance with empty connectionString");
            if (ISQLExecuter == null)
                _ISQLExecuter = new SQLExecuter(cns);
            else
                _ISQLExecuter = ISQLExecuter;
        }
        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_User_Access_Menu_Insert_Params data)
        {
            if (data == null)
                throw new ArgumentNullException("خطا در نحوه ارسال در خواست");
            string sql = "dbo.SP_User_Access_Menu_Insert";
            DBResult rst = null;

            var p = Common.GetSearchParams(data.Token);
            p.AddDynamicParams(Common.GenerateParams(data, new List<string> { "Token" }));
            var dbResult = await _ISQLExecuter.QueryFirstOrDefaultAsync<Models.Database.OutputIdModel>(
                sql, p, System.Data.CommandType.StoredProcedure);
            rst = Common.GetResult(p, dbResult);
            return rst;
        }
    }
}
