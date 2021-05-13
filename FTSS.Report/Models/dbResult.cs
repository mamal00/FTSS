using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTSS.Report.Models
{
	public class dbResult
	{
        /// <summary>
        /// The query result, can be a list or just a simple Id
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// All record count by the current condition
        /// </summary>
        public int ActualSize { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}