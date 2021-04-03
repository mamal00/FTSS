using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.API.Controllers
{
	[Route("/api/[controller]/[action]")]
	[ApiController]
	public class RolesController : BaseController
	{
        public RolesController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger)
 : base(dbCTX, logger)
        {
        }
        [HttpPut]
        [Filters.Auth]
        public async Task<IActionResult> GetAll([FromBody] Models.Database.BaseModel filterParams)
        {
            try
            {
                var dbResult = await Logic.Database.StoredProcedure.SP_Roles_GetAll.Call(_ctx, filterParams);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in RolesController.GetAll(filterParams)");
                return Problem(e.Message, e.StackTrace, 500, "Error in GetAll");
            }
        }
    }
}
