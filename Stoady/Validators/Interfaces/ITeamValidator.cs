namespace Stoady.Validators.Interfaces
{
    public interface ITeamValidator
    {
        /// <summary>
        /// Проверяет, что команда с данным ID существует.
        /// </summary>
        /// <param name="teamId">ID команды</param>
        /// <returns></returns>
        bool ValidateTeam(
            long teamId);
    }
}
