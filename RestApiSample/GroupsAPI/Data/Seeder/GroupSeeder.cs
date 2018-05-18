using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupsAPI.Data.Context;
using GroupsAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupsAPI.Data.Seeder
{
    public class GroupSeeder
    {
        private readonly DatabaseContext _dbContext;

        public GroupSeeder(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void EnsureDatabaseCreated()
        {
            _dbContext.Database.Migrate();
        }

        public void Seed()
        {
            EnsureDatabaseCreated();

            if (_dbContext.Groups.ToList().Count != 0)
                return;

            var groups = new List<GroupEntity>
            {
                new GroupEntity
                {
                    Name = "FirstGroup",
                    FacultyId = 1,
                    CountOfStudents = 10
                },
                new GroupEntity
                {
                    Name = "SecondGroup",
                    FacultyId = 1,
                    CountOfStudents = 3
                }
            };

            _dbContext.Groups.AddRange(groups);
            _dbContext.SaveChanges();
        }
    }
}
