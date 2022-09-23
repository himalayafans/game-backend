using GameBackend.Library.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameBackend.Controllers
{
    /// <summary>
    /// 演示
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns>返回当前时间</returns>
        [HttpGet]
        public DateTime Test()
        {
            return DateTime.Now;
        }

        [HttpGet]
        public string Hello()
        {
            return "hello world";
        }
    }
}
