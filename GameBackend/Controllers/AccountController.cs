using GameBackend.Library.Dtos.Account;
using GameBackend.Library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameBackend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterDto account)
        {
            await _accountService.Register(account);
            return Ok();
        }
    }
}
