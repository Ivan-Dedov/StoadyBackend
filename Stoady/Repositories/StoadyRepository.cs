namespace Stoady.Repositories
{
    public abstract class StoadyRepository
    {
        private const string Host = "localhost";
        private const string UserName = "administrator";
        private const string Password = "assdinsan";
        private const string DatabaseName = "stoady";

        protected const string ConnectionString = $"Host={Host};Username={UserName};Password={Password};Database={DatabaseName}";
    }
}
