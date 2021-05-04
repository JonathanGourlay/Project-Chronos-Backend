using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Project_Chronos_Backend.Controllers
{
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult MapToIActionResult<T>(Func<T> getData)
        {
            try
            {
                var result = getData.Invoke();
                if (result == null)
                {
                    return Ok(default(T));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
