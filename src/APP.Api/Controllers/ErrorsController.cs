﻿using APP.Api.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP.Api.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {
        //-------------------------------------------------------  Not forget errors middleware
        //built in middleware 
        /*
         * app.UseStatusCodePagesWithReExecute("/ControlerName/{0}");
         * app.UseStatusCodePagesWithReExecute("/errors/{0}");
        */
        //custom middleware for error with exceptions ..................... Excption middleware

        [HttpGet("Error")]
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new BaseCommonResponse(statusCode));
        }
    }
}
