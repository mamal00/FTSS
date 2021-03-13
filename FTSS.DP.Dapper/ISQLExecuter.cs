using FTSS.Models.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM
{
	public interface ISQLExecuter
	{
        /// <summary>
        /// Execute database query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, System.Data.CommandType? commandType = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, System.Data.CommandType? commandType = null);
        Task<DBResult> ExecuteAsync(string sql, object param = null);

    }
}
