using Microsoft.EntityFrameworkCore;
using StudentsAPI.Data.Models;

namespace StudentsAPI.Data.Context
{
    public class DatabaseContext: DbContext
    {
        public DbSet<StudentEntity> Students { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
