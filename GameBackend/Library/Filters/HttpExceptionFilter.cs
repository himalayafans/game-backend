using GameBackend.Library.Common;
using GameBackend.Library.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace GameBackend.Library.Filters
{
    /// <summary>
    /// HTTP异常过滤器
    /// </summary>
    public class HttpExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public HttpExceptionFilter(ILogger<HttpExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {

            if (context.Exception is HttpException e)
            {
                AjaxResult<string> result = new AjaxResult<string>()
                {
                    Success = false,
                    Message = e.Message,
                    Content = ""
                };
                context.Result = new JsonResult(result)
                {
                    StatusCode = e.StatusCode
                };
            }
            else
            {
                _logger.LogError(context.Exception, context.Exception.Message);
                AjaxResult<string> result = new AjaxResult<string>()
                {
                    Success = false,
                    Message = "内部服务器错误",
                    Content = ""
                };
                context.Result = new JsonResult(result)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
