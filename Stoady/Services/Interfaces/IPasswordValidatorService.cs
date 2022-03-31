namespace Stoady.Services.Interfaces
{
    public interface IPasswordValidatorService
    {
        (string HashedPassword, string Salt) GetHashedPassword(
            string password);

        bool ValidatePassword(
            string password,
            string truePassword,
            string salt);
    }
}
