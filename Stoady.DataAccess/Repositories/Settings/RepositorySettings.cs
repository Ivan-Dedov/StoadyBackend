namespace Stoady.DataAccess.Repositories.Settings
{
    /// <summary>
    /// Настройки репозиториев
    /// TODO: Сделать config файл, чтобы были настраиваемыми без изменения кода
    /// </summary>
    internal static class RepositorySettings
    {
        /// <summary>
        /// Запрос на подключение к БД
        /// </summary>
        public const string ConnectionString = "Server=ec2-52-211-158-144.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9elrdlh8nmq04;Username=rxxaapxbpdyrlk;Password=97ecec32a181a8066f081d64aef963e0dda3c6cda9f3b6af4d19e73d5ca14a01";

        /// <summary>
        /// Таймаут запроса
        /// </summary>
        public const int TimeoutSeconds = 5;
    }
}
