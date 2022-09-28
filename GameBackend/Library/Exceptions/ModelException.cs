using System.Net;

namespace GameBackend.Library.Exceptions
{
    /// <summary>
    /// 模型验证异常
    /// </summary>
    public class ModelException : HttpException
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; }
        public ModelException(string fieldName, string message, HttpStatusCode httpStatusCode) : base(httpStatusCode, message)
        {
            FieldName = fieldName;
        }
    }
}
