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
	public class FishDetailController : BaseController
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
        public FishDetailController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger, IConfiguration configuration)
       : base(dbCTX, logger, configuration)
        {
        }

        /// <summary>
        /// GetAll FishDetail With Seach Params
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Filters.Auth]
        public async Task<IActionResult> GetAll([FromBody] Models.Database.StoredProcedures.SP_FishDetail_GetAll_Params filterParams)
        {
            try
            {
                var dbResult = await Logic.Database.StoredProcedure.SP_FishDetail_GetAll.Call(_ctx, filterParams, JWTKey, JWTIssuer);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in FishDetailController.GetAll()");
                return Problem(e.Message, e.StackTrace, 500, "Error in GetAll");
            }
        }
        /// <summary>
        /// Get Fish Detail Sum
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        [HttpPut]
        [Filters.Auth]
        public async Task<IActionResult> GetSum([FromBody] Models.Database.StoredProcedures.SP_Fish_Get_Sum_Params filterParams)
        {
            try
            {
                var dbResult = await Logic.Database.StoredProcedure.SP_FishDetail_Sum_Get.Call(_ctx, filterParams, JWTKey, JWTIssuer);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in FishDetailController.GetSum()");
                return Problem(e.Message, e.StackTrace, 500, "Error in GetSum");
            }
        }
        [HttpPut]
        [Filters.Auth]
        public async Task<IActionResult> Get([FromBody] Models.Database.StoredProcedures.SP_FishDetail_GetAll_Params filterParams)
        {
            try
            {
                var dbResult = await Logic.Database.StoredProcedure.SP_FishDetail_GetAll.CallMobile(_ctx, filterParams, JWTKey, JWTIssuer);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in FishDetailController.Get()");
                return Problem(e.Message, e.StackTrace, 500, "Error in GetSum");
            }
        }
    }
}
