using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Project_Chronos_Backend.API.Controllers
{
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public class BaseController : ControllerBase
    {
        protected IActionResult MapToIActionResult<T>(Func<T> getData)
        {
            try
            {
                var result = getData.Invoke();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
