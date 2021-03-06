using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database
{
	public class BaseIdModel:BaseModel
	{
		/// <summary>
		/// Id for Delete , Get ...
		/// </summary>
		public int? Id { get; set; }
	}
}
