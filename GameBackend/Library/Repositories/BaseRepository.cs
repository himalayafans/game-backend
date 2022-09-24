using Npgsql;

namespace GameBackend.Library.Repositories
{
    /// <summary>
    /// 存储库基类
    /// </summary>
    public abstract class BaseRepository
    {
        protected NpgsqlConnection Connection { get; }

        protected BaseRepository(NpgsqlConnection connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
    }
}
