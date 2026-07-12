using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MINI_DGPAY.Services.Main;

namespace MINI_DGPAY.ApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        [HttpGet("Response")]
        public IActionResult Execute<T>(CommonResponse<T> model)
        {
            if (model.isSuccess)
            {
                return Ok(model);
            }
            if (model.isValidationError)
            {
                return BadRequest(model);
            }
            if (model.isSystemError)
            {
                return BadRequest(model);
            }
            if(model.isError)
            {
                return BadRequest(model);
            }
            if (model.isDataNotExist)
            {
                return BadRequest(model);
            }
            if (model.isSystemError)
            {
                return BadRequest(model);
            }
            return Ok(model);
        }
    }
}
