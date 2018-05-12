using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestApiTest.Data.Context;
using RestApiTest.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using RestApiTest.Services.Abstractions;

namespace RestApiTest.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly UniversityDbContext _dbConext;

        public StudentService(UniversityDbContext dbConext)
        {
            _dbConext = dbConext;
        }

        public async Task<IEnumerable<StudentEntity>> GetAllStudentsAsync()
        {
            return await _dbConext.Students.ToListAsync();
        }

        public async Task<StudentEntity> GetStudentByIdAsync(long studentId)
        {
            var student = await _dbConext.Students.FirstOrDefaultAsync(x => x.Id == studentId);

            if(student == null)
                throw new ArgumentException($"Student with id {student} not found");

            return student;
        }

        public async Task<StudentEntity> AddStudentAsync(StudentEntity student)
        {
            await _dbConext.Students.AddAsync(student);
            await _dbConext.SaveChangesAsync();

            return student;
        }

        public async Task<StudentEntity> UpdateStudentAsync(StudentEntity studentForUpdate)
        {
            var student = await _dbConext.Students.FirstOrDefaultAsync(x => x.Id == studentForUpdate.Id);

            if (student == null)
                throw new ArgumentException($"Student with id {studentForUpdate.Id} not found");

            student.GroupId = studentForUpdate.GroupId;
            student.Age = studentForUpdate.Age;
            student.FirstName = studentForUpdate.FirstName;
            student.LastName = studentForUpdate.LastName;

            await _dbConext.SaveChangesAsync();

            return studentForUpdate;
        }

        public async Task DeleteStudentAsync(long studentId)
        {
            var student = await _dbConext.Students.FirstOrDefaultAsync(x => x.Id == studentId);

            if (student == null)
                throw new ArgumentException($"Student with id {studentId} not found");

            _dbConext.Students.Remove(student);
            await _dbConext.SaveChangesAsync();
        }
    }
}
