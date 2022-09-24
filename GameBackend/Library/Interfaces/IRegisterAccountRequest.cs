namespace GameBackend.Library.Interfaces
{
    /// <summary>
    /// 账号注册HTTP请求接口
    /// </summary>
    public interface IRegisterAccountRequest
    {
        string name { get; set; }
        string email { get; set; }
        string password { get; set; }
    }
}
