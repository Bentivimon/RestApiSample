using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Data.Context;
using StudentsAPI.Data.Models;

namespace StudentsAPI.Data.Seeder
{
    public class StudentSeeder
    {
        private readonly DatabaseContext _dbContext;

        public StudentSeeder(DatabaseContext dbContext)
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

            if (_dbContext.Students.ToList().Count != 0)
                return;

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
