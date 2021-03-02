using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FTSS.API.Filters
{
    public class ModelValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           var checkValidate= context.ModelState;
            if(!checkValidate.IsValid)
			{
                context.Result = new BadRequestObjectResult(context.ModelState);
          
            }
            base.OnActionExecuting(context);
        }

    
    }
}
