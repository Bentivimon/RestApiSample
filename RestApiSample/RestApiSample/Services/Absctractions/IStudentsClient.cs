using DataModels.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.Services
{
    public interface IStudentsClient
    {
        Task<IEnumerable<StudentMessage>> GetStudentsAsync();
        Task<StudentMessage> GetStudentByIdAsync(long studentId);
        Task<IEnumerable<StudentMessage>> GetStudentsOfGroupAsync(long groupId);
        Task<StudentMessage> AddStudentAsync(StudentMessage studentRequest);
        Task<StudentMessage> UpdateStudentAsync(StudentMessage studentRequest);
    }
}
