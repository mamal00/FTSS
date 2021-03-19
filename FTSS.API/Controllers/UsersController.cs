using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FTSS.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FTSS.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class UsersController : BaseController
    {
        public UsersController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger) 
            : base(dbCTX, logger)
        {
        }

        /// <summary>
        /// Search between all users by filter parameters
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        [HttpPut]
        [Filters.Auth]
        public async Task<IActionResult> GetAll([FromBody] Models.Database.StoredProcedures.SP_Users_GetAll_Params filterParams)
        {
            try
            {
                var dbResult =await Logic.Database.StoredProcedure.SP_Users_GetAll.Call(_ctx, filterParams);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.GetAll(filterParams)");
                return Problem(e.Message, e.StackTrace, 500, "Error in GetAll");
            }
        }
        /// <summary>
        /// Get User With UserId
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        [HttpPut]
        [Filters.Auth]
        public async Task<IActionResult> Get([FromBody] Models.Database.StoredProcedures.SP_Users_Get_Params filterParams)
        {
            try
            {
                var dbResult = await Logic.Database.StoredProcedure.SP_Users_Get.Call(_ctx, filterParams);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Get(filterParams)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Get");
            }
        }

        /// <summary>
        /// Add new user to database
        /// </summary>
        /// <param name="data">
        /// User info
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Models.Database.Tables.Users data)
        {
            try
            {
                var rst =await Logic.Database.StoredProcedure.SP_User_Insert.Call(_ctx, data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Insert(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Insert");
            }
        }
        /// <summary>
        /// Update user to database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Models.Database.Tables.Users data)
        {
            try
            {
                var rst = await Logic.Database.StoredProcedure.SP_User_Update.Call(_ctx, data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Update(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Insert");
            }
        }
        /// <summary>
        /// Login and get database token
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Login([FromBody] Models.Database.StoredProcedures.SP_Login_Params filterParams)
        {
            try
            {
                var rst = await Logic.Security.UserInfo.Login(_ctx, filterParams);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Login(filterParams)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Login");
            }
        }
        /// <summary>
        /// Delete User With Id
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Models.Database.BaseIdModel data)
        {
            try
            {
                var rst = await Logic.Database.StoredProcedure.SP_User_Delete.Call(_ctx, data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in UsersController.Delete(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Delete");
            }
        }

    }
}
