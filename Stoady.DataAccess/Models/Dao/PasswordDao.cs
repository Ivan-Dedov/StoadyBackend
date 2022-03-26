namespace Stoady.DataAccess.Models.Dao
{
    public sealed class PasswordDao
    {
        public string Password { get; init; }

        public string Salt { get; init; }
    }
}
