namespace GameBackend.Library.Exceptions
{
    /// <summary>
    /// 模型验证异常
    /// </summary>
    public class ModelException : SiteException
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; }
        public ModelException(string fieldName, string message) : base(message)
        {
            FieldName = fieldName;
        }
    }
}
