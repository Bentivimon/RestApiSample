using GroupsAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupsAPI.Data.Context
{
    public class DatabaseContext: DbContext
    {
        public DbSet<GroupEntity> Groups { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
