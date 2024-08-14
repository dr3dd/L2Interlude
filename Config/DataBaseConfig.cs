namespace Config
{
    public class DataBaseConfig
    {
        public const string DataBaseSection = "DataBaseSection";
        /// <summary>
        /// Database Server Host
        /// Хост сервера базы данных
        /// </summary>
        public string DataBaseHost { get; set; }
        /// <summary>
        /// Database server port
        /// Порт сервера базы данных
        /// </summary>
        public int DataBasePort { get; set; }
        /// <summary>
        /// Server database name
        /// Имя базы данных сервера
        /// </summary>
        public string DataBaseName { get; set; }
        /// <summary>
        /// Database user name
        /// Имя пользователя базы данных
        /// </summary>
        public string DataBaseUser { get; set; }
        /// <summary>
        /// Database user password
        /// Пароль пользователя базы данных
        /// </summary>
        public string DataBasePassword { get; set; }
        /// <summary>
        /// Create a database if it does not exist (requires mqsql db read permissions)
        /// Создать базу данных, если её не существует (требуются права на чтение бд mqsql)
        /// </summary>
        public bool DataBaseAutoCreate { get; set; } = false;

    }
}