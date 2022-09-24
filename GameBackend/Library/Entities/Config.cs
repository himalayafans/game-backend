using Dapper.Contrib.Extensions;

namespace GameBackend.Library.Entities
{
    /// <summary>
    /// 配置
    /// </summary>
    [Table("config")]
    public class Config
    {
        [ExplicitKey]
        public Guid id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; } = "";
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; } = "";
    }
}
