﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.API.Controllers
{
	[Route("/api/[controller]/[action]")]
	[ApiController]
	public class FishController : BaseController
	{
		/// <summary>
		/// Read JWT key from appsettings.json
		/// </summary>
		public string JWTKey
		{
			get
			{
				var rst = this._configuration.GetValue<string>("JWT:Key");
				return (rst);
			}
		}

		public string JWTIssuer
		{
			get
			{
				var rst = this._configuration.GetValue<string>("JWT:Issuer");
				return (rst);
			}
		}
		public FishController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger, IConfiguration configuration)
	   : base(dbCTX, logger, configuration)
		{
		}

		
		/// <summary>
		/// Get Sum Fish
		/// </summary>
		/// <param name="filterParams"></param>
		/// <returns></returns>
		[HttpPut]
		[Filters.Auth]
		public async Task<IActionResult> GetSum([FromBody] Models.Database.StoredProcedures.SP_Fish_Get_Sum_Params filterParams)
		{
			try
			{
				var dbResult = await Logic.Database.StoredProcedure.SP_Fish_Sum_Get.Call(_ctx, filterParams, JWTKey, JWTIssuer);
				return FromDatabase(dbResult);
			}
			catch (Exception e)
			{
				_logger.Add(e, "Error in FishController.GetSum()");
				return Problem(e.Message, e.StackTrace, 500, "Error in GetSum");
			}
		}
	
	}
}
