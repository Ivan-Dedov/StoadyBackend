namespace Stoady.Validators.Interfaces
{
    public interface IUserValidator
    {
        /// <summary>
        /// Проверяет, что пользователь с данным ID существует.
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns></returns>
        bool ValidateUser(
            long userId);
    }
}
