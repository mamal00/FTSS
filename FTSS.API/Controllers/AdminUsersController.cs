using Microsoft.AspNetCore.Http;
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
	public class AdminUsersController : BaseController
	{
		/// <summary>
		/// Access to appsettings.json
		/// </summary>
		private readonly AutoMapper.IMapper _mapper;
		public AdminUsersController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger, IConfiguration configuration, AutoMapper.IMapper mapper)
			: base(dbCTX, logger, configuration)
		{
			_mapper = mapper;
		}
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
		/// <summary>
		/// Login For Admin
		/// </summary>
		/// <param name="filterParams"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> Login([FromBody] Models.Database.StoredProcedures.SP_Admin_Login_Params filterParams)
		{
			try
			{
				var rst = await Logic.Database.StoredProcedure.SP_Admin_Login.Call(_ctx, _mapper, filterParams, JWTKey, JWTIssuer);
				return FromDatabase(rst);
			}
			catch (Exception e)
			{
				_logger.Add(e, "Error in AdminUsersController.Login(filterParams)");
				return Problem(e.Message, e.StackTrace, 500, "Error in Login");
			}
		}
	}
}
