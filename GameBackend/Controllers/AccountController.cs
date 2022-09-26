using GameBackend.Library.Dtos.Account;
using GameBackend.Library.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login([FromBody] AccountLoginDto account)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 账号注册
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountRegisterDto account)
        {
            await _accountService.Register(account);
            return Ok();
        }
    }
}
