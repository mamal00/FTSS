using FTSS.DP.DapperORM.StoredProcedure;
using FTSS.Models.Database.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    /// <summary>
    /// Implement ILog for saving log at database
    /// </summary>
    public class DB : ILog
    {
        Models.Database.ISP<SP_Log_Insert_Params> _storedProcedure;

        public DB(Models.Database.ISP<SP_Log_Insert_Params> storedProcedure)
        {
            _storedProcedure = storedProcedure;
        }

        /// <summary>
        /// Log an Exception with custom message
        /// </summary>
        /// <param name="customMessage"></param>
        /// <param name="e"></param>
        public void Add(Exception e, string customMessage = null)
        {
            string text = string.Format("{0}\nException: {1}\nStackTrace: {2}\n",
                customMessage ?? "", e.Message, e.StackTrace);
            var model = new SP_Log_Insert_Params(text);
            this.Add(model);
        }

        /// <summary>
        /// Log a simple text at database
        /// </summary>
        /// <param name="msg"></param>
        public void Add(SP_Log_Insert_Params model)
        {
            _storedProcedure.Call(model);
        }
    }
}
