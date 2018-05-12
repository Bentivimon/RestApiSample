using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestApiTest.Data.Context;
using RestApiTest.Data.Models;

namespace RestApiTest.Data.Seeders
{
    public class DefaultSeeder
    {
        private readonly UniversityDbContext _dbContext;

        public DefaultSeeder(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void EnsureDatabaseCreated(UniversityDbContext context)
        {
            context.Database.Migrate();
        }

        public void Seed()
        {
            EnsureDatabaseCreated(_dbContext);

            if (_dbContext.Students.ToList().Count != 0)
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

            var strudetns = new List<StudentEntity>()
            {
                new StudentEntity
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Age = 20,
                    GroupId = 1
                },
                new StudentEntity
                {
                    FirstName = "Tolik",
                    LastName = "BlaBla",
                    Age = 22,
                    GroupId = 1
                },
                new StudentEntity
                {
                    FirstName = "Kolya",
                    LastName = "Kol",
                    Age = 21,
                    GroupId = 1
                },
                new StudentEntity
                {
                    FirstName = "Wital",
                    LastName = "Poho",
                    Age = 20,
                    GroupId = 2
                },
                new StudentEntity
                {
                    FirstName = "Fedir",
                    LastName = "Filelele",
                    Age = 19,
                    GroupId = 2
                },
                new StudentEntity
                {
                    FirstName = "Olya",
                    LastName = "Simonova",
                    Age = 21,
                    GroupId = 2
                }
            };

            _dbContext.Students.AddRange(strudetns);
            _dbContext.SaveChanges();
        }
    }
}
