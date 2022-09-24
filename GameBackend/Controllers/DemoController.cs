using GameBackend.Library.Data;
using GameBackend.Library.Services;
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
        private readonly ILogger<DemoController> _logger;
        private readonly EncryptionService _encryptionService;

        public DemoController(ILogger<DemoController> logger, EncryptionService encryptionService)
        {
            _logger = logger;
            _encryptionService = encryptionService;
        }
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns>返回当前时间</returns>
        [HttpGet]
        public DateTime Test()
        {
            _logger.LogWarning(_encryptionService.PasswordHash("123"));
            _logger.LogWarning("警告信息");
            return DateTime.Now;
        }

        [HttpGet]
        public string Hello()
        {
            return "hello world";
        }
    }
}
