using GameBackend.Library.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameBackend.Controllers.Base
{
    [Route("[controller]/[action]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// 生成统一的response数据格式
        /// </summary>
        protected AjaxResult<T> AjaxResult<T>(T content, bool success = true, string message = "")
        {
            return new AjaxResult<T>()
            {
                Content = content,
                Success = success,
                Message = message
            };
        }
        /// <summary>
        /// 生成默认成功的response
        /// </summary>
        protected AjaxResult<string> AjaxResult()
        {
            return new AjaxResult<string>()
            {
                Content = "",
                Success = true,
                Message = ""
            };
        }
    }
}
