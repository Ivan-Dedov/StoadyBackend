namespace Stoady.Models
{
    /// <summary>
    /// Роль пользователя в команде
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// Создатель команды
        /// </summary>
        Creator = 1,

        /// <summary>
        /// Администратор команды
        /// </summary>
        Admin = 2,

        /// <summary>
        /// Участник команды
        /// </summary>
        Member = 3
    }
}
