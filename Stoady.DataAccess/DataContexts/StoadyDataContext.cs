using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Stoady.DataAccess.Models.Dao;

namespace Stoady.DataAccess.DataContexts
{
    public sealed class StoadyDataContext : DbContext
    {
        #region Таблицы

        public DbSet<UserDao> Users { get; set; }

        public DbSet<RoleDao> Roles { get; set; }

        public DbSet<TeamDao> Teams { get; set; }

        public DbSet<SubjectDao> Subjects { get; set; }

        public DbSet<TopicDao> Topics { get; set; }

        public DbSet<QuestionDao> Questions { get; set; }

        public DbSet<StatisticsDao> Statistics { get; set; }

        public DbSet<TeamUserDao> TeamUser { get; set; }

        public DbSet<UserQuestionDao> UserQuestions { get; set; }

        #endregion

        public StoadyDataContext(DbContextOptions<StoadyDataContext> options)
            : base(options)
        {
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }
    }
}
