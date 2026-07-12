using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MINI_DGPAY.DataHub.Models;
using MINI_DGPAY.Services.Main;

namespace MINI_DGPAY.ApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ResponseController
    {
        protected AccServices acc;
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
            return Execute(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(string code, string? username, string? moddate, string? modby)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Code is required");
            }else if(string.IsNullOrEmpty(username) && string.IsNullOrEmpty(moddate) && string.IsNullOrEmpty(modby))
            {
                return BadRequest("At least one field to update is required");
            }
            var response = await acc.Update(code, username, moddate, modby);
            return Execute(response);
        }
    }
}
