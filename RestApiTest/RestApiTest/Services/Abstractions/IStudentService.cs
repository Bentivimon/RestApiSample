using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiTest.Data.Models;

namespace RestApiTest.Services.Abstractions
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentEntity>> GetAllStudentsAsync();
        Task<StudentEntity> GetStudentByIdAsync(long studentId);
        Task<StudentEntity> AddStudentAsync(StudentEntity student);
        Task<StudentEntity> UpdateStudentAsync(StudentEntity student);
        Task DeleteStudentAsync(long studentId);
    }
}
