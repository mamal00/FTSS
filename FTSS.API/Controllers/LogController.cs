using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FTSS.Models.Database.StoredProcedures;
using Microsoft.AspNetCore.Mvc;

namespace FTSS.API.Controllers
{
    public class complex
    {
        public string message { get; set; }
    }

    [Route("/api/[controller]/[action]")]
    public class LogController : Controller
    {
        private readonly Logic.Log.ILog _logger;

        public LogController(Logic.Log.ILog logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]SP_Log_Insert_Params data)
        {
            _logger.Add(data);
            return Ok(data);
        }

    }
}
