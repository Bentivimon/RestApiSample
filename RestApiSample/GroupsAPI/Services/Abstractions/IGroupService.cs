using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.RequestModels;

namespace GroupsAPI.Services.Abstractions
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupMessage>> GetAllGroupsAsync();
        Task<GroupMessage> GetGroupAsync(long id);
        Task<GroupMessage> AddGroupAsync(GroupMessage request);
        Task<GroupMessage> UpdateGroupAsync(GroupMessage request);
    }
}
