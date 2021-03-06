using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database
{
    public class BaseSearchParams: BaseModel
    {
        /// <summary>
        /// Database token
        /// </summary>
    
        public int StartIndex { get; set; }
        
        public int PageSize { get; set; }
		public string Sort { get; set; }
	}
}
