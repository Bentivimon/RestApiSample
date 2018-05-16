using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels.RequestModels;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Data.Context;
using StudentsAPI.Data.Models;
using StudentsAPI.Services.Abstractions;

namespace StudentsAPI.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly DatabaseContext _dbContext;

        public StudentService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<StudentRequestModel>> GetAllStudentsAsync()
        {
            return (await _dbContext.Students.ToListAsync()).Select(x => new StudentRequestModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                GroupId = x.GroupId
            });
        }

        public async Task<StudentRequestModel> GetStudentAsync(long id)
        {
            var studentEntity = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (studentEntity == null)
                throw new ArgumentException($"Student with id {id} not found");

            return new StudentRequestModel
            {
                Id = studentEntity.Id,
                FirstName = studentEntity.FirstName,
                LastName = studentEntity.LastName,
                Age = studentEntity.Age,
                GroupId = studentEntity.GroupId
            };
        }

        public async Task<StudentRequestModel> AddStudentAsync(StudentRequestModel request)
        {
            var studentEntity = new StudentEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                GroupId = request.GroupId
            };

            await _dbContext.Students.AddAsync(studentEntity);
            await _dbContext.SaveChangesAsync();

            request.Id = studentEntity.Id;

            return request;
        }

        public async Task<StudentRequestModel> UpdateStudentAsync(StudentRequestModel request)
        {
            var studentEntity = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (studentEntity == null)
                throw new ArgumentException($"Student with id {request.Id} not found");

            studentEntity.FirstName = request.FirstName;
            studentEntity.LastName = request.LastName;
            studentEntity.Age = request.Age;
            studentEntity.GroupId = request.GroupId;

            await _dbContext.SaveChangesAsync();

            return request;
        }
    }
}
