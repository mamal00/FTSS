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
	public class UserProfileController : BaseController
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
        public UserProfileController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger, IConfiguration configuration)
           : base(dbCTX, logger, configuration)
        {
        }

        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Filters.Auth]
        public async Task<IActionResult> Get()
        {
            try
            {
                var dbResult = await Logic.Database.StoredProcedure.SP_Users_Get.CallProfile(_ctx, JWTKey, JWTIssuer);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UserProfileController.Get()");
                return Problem(e.Message, e.StackTrace, 500, "Error in Get");
            }
        }
        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Models.Database.Tables.Users data)
        {
            try
            {
                var rst = await Logic.Database.StoredProcedure.SP_User_UpdateProfile.Call(_ctx, data, JWTKey, JWTIssuer);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UserProfileController.Update(data)");
                  return Problem(e.Message, e.StackTrace, 500, "Error in Update");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePassword([FromBody] Models.Database.StoredProcedures.SP_User_ChangePassword_Params data)
        {
            try
            {
                var rst = await Logic.Database.StoredProcedure.SP_User_ChangePassword.Call(_ctx, data, JWTKey, JWTIssuer);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UserProfileController.UpdatePassword(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in UpdatePassword");
            }
        }
    }
}
