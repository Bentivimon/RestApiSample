using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.RequestModels;

namespace StudentsAPI.Services.Abstractions
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentMessage>> GetAllStudentsAsync();
        Task<StudentMessage> GetStudentAsync(long id);
        Task<IEnumerable<StudentMessage>> GetStudentsOfGroup(long groupId);
        Task<StudentMessage> AddStudentAsync(StudentMessage request);
        Task<StudentMessage> UpdateStudentAsync(StudentMessage request);
    }
}
