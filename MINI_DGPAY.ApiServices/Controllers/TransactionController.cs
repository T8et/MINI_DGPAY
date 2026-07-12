using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MINI_DGPAY.Services.Main;
using System.Runtime.CompilerServices;

namespace MINI_DGPAY.ApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ResponseController
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
            return Ok(response);
        }

        [HttpPost("MakeDeposit")]
        public async Task<IActionResult> MakeDeposit(string sender, decimal amount, string remark)
        {
            var response = await services.MakeDeposit(sender, amount, remark);
            return Execute(response);
        }

        [HttpPost("MakeWithdrawal")]
        public async Task<IActionResult> MakeWithdrawal(string sender, decimal amount, string remark, int pin = 0)
        {
            var response = await services.MakeWithdrawl(sender, amount, remark, pin);
            return Execute(response);
        }

        [HttpPost("MakeTransfer")]
        public async Task<IActionResult> MakeTransfer(string sender, string receiver, decimal amount, string remark, int pin)
        {
            var response = await services.MakeTransfer(sender, receiver, amount, remark, pin);
            return Execute(response);
        }
    }
}