using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MINI_DGPAY.Services.Main;
using System.Runtime.CompilerServices;

namespace MINI_DGPAY.ApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TranServices services;
        public TransactionController(TranServices _services)
        {
            services = _services;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await services.GetAll();
            return Ok(response);
        }

        [HttpGet("GetById/{code}")]
        public async Task<IActionResult> GetById(string code)
        {
            var response = await services.GetById(code);
            if (response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("MakeDeposit")]
        public async Task<IActionResult> MakeDeposit(string sender, string receiver, decimal amount, string remark)
        {
            var response = await services.MakeDeposit(sender, receiver, amount, remark);
            return Ok(response);
        }
    }
}