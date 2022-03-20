namespace Stoady.Validators.Interfaces
{
    public interface IUserTeamValidator
    {
        /// <summary>
        /// Проверяет, что пользователь состоит в данной команде.
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="teamId">ID команды</param>
        /// <returns></returns>
        bool ValidateUserIsInTeam(
            long userId,
            long teamId);

        /// <summary>
        /// Проверяет, что пользователь имеет права администратора в данной команде.
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="teamId">ID команды</param>
        /// <returns></returns>
        bool ValidateUserHasAdminRights(
            long userId,
            long teamId);
    }
}
