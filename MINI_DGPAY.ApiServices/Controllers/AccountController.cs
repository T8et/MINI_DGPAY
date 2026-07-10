using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MINI_DGPAY.DataHub.Models;
using MINI_DGPAY.Services.Main;

namespace MINI_DGPAY.ApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccServices acc;
        public AccountController(AccServices _acc)
        {
            this.acc = _acc;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var respone = await acc.GetAll();
            return Ok(respone);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await acc.GetById(id);
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(BtAccount account)
        {
            var response = await acc.Create(account);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(BtAccount account)
        {
            var response = await acc.Update(account);
            return Ok(response);
        }
    }
}
