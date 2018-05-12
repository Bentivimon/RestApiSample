using Microsoft.EntityFrameworkCore;
using RestApiTest.Data.Models;

namespace RestApiTest.Data.Context
{
    public class UniversityDbContext: DbContext
    {
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<StudentEntity> Students { get; set; }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentEntity>()
                .HasOne(x => x.Group)
                .WithMany(y => y.Students);
        }
    }
}
