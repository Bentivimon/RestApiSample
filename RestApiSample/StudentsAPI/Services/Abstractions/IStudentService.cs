using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels.RequestModels;

namespace StudentsAPI.Services.Abstractions
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentRequestModel>> GetAllStudentsAsync();
        Task<StudentRequestModel> GetStudentAsync(long id);
        Task<StudentRequestModel> AddStudentAsync(StudentRequestModel request);
        Task<StudentRequestModel> UpdateStudentAsync(StudentRequestModel request);
    }
}
