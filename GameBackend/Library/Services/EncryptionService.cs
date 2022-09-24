using GameBackend.Library.Utils;

namespace GameBackend.Library.Services
{
    /// <summary>
    /// 加密服务
    /// </summary>
    public class EncryptionService
    {
        /// <summary>
        /// 获取密码哈希值
        /// </summary>
        public string PasswordHash(string password)
        {
            return PasswordEncryptor.Hash(password);
        }
    }
}
