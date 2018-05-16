using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.RequestModels;

namespace GroupsAPI.Services.Abstractions
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupRequestModel>> GetAllGroupsAsync();
        Task<GroupRequestModel> GetGroupAsync(long id);
        Task<GroupRequestModel> AddGroupAsync(GroupRequestModel request);
        Task<GroupRequestModel> UpdateGroupAsync(GroupRequestModel request);
    }
}
