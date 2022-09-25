namespace GameBackend.Library.Exceptions
{
    /// <summary>
    /// 网站异常，通常是前端数据不规范导致
    /// </summary>
    public class SiteException: Exception
    {
        public SiteException():base(){}
        public SiteException(string message):base(message){}
    }
}
