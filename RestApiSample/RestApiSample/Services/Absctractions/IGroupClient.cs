using DataModels.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.Services
{
    public interface IGroupClient
    {
        Task<IEnumerable<GroupMessage>> GetAllGroupsAsync();
        Task<GroupMessage> GetGroupByIdAsync(long groupId);
        Task<GroupMessage> AddGroupAsync(GroupMessage groupRequest);
        Task<GroupMessage> UpdateGroupAsync(GroupMessage groupRequest);
    }
}
