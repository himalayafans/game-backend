using GameBackend.Controllers.Base;
using GameBackend.Library.Common;
using GameBackend.Library.Dtos.Account;
using GameBackend.Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GameBackend.Controllers
{
    public class AccountController : BaseApiController
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
        [HttpPost]
        [AllowAnonymous]
        public async Task<AjaxResult<string>> Register([FromBody] AccountRegisterDto account)
        {
            if (account.Name == "admin")
            {
                throw new Exception("该名称已存在");
            }
            await _accountService.Register(account);
            return this.AjaxResult();
        }
    }
}
