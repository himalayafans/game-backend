namespace GameBackend.Library.Data
{
    /// <summary>
    /// 数据库参数计数器
    /// </summary>
    public class DbArgsCounter
    {
        private int _counter = 0;
        /// <summary>
        /// 下一个参数命名
        /// </summary>
        public string Next()
        {
            this._counter++;
            return "$" + this._counter.ToString();
        }
        /// <summary>
        /// 当前参数命名
        /// </summary>
        public string Current()
        {
            return "$" + this._counter.ToString();
        }
    }
}
