using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels.RequestModels;
using GroupsAPI.Data.Context;
using GroupsAPI.Data.Models;
using GroupsAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GroupsAPI.Services.Implementations
{
    public class GroupService: IGroupService
    {
        private readonly DatabaseContext _dbContext;

        public GroupService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GroupRequestModel>> GetAllGroupsAsync()
        {
            return (await _dbContext.Groups.ToListAsync()).Select(x => new GroupRequestModel
            {
                Id = x.Id,
                CountOfStudents = x.CountOfStudents,
                FacultyId = x.FacultyId,
                Name = x.Name,
                StudentIds = x.StudentIds
            });
        }

        public async Task<GroupRequestModel> GetGroupAsync(long id)
        {
            var groupEntity = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);

            if(groupEntity == null)
                throw new ArgumentException($"Group with id {id} not found");

            return new GroupRequestModel
            {
                Id = groupEntity.Id,
                CountOfStudents = groupEntity.CountOfStudents,
                FacultyId = groupEntity.FacultyId,
                Name = groupEntity.Name,
                StudentIds = groupEntity.StudentIds
            };
        }

        public async Task<GroupRequestModel> AddGroupAsync(GroupRequestModel request)
        {
            var groupEntity = new GroupEntity
            {
                CountOfStudents = request.CountOfStudents,
                FacultyId = request.FacultyId,
                Name = request.Name,
                StudentIds = request.StudentIds
            };

            await _dbContext.Groups.AddAsync(groupEntity);
            await _dbContext.SaveChangesAsync();

            request.Id = groupEntity.Id;

            return request;
        }

        public async Task<GroupRequestModel> UpdateGroupAsync(GroupRequestModel request)
        {
            var groupEntity = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == request.Id);

            if(groupEntity == null)
                throw new ArgumentException($"Group with id {request.Id} not found");

            groupEntity.CountOfStudents = request.CountOfStudents;
            groupEntity.FacultyId = request.FacultyId;
            groupEntity.Name = request.Name;
            groupEntity.StudentIds = request.StudentIds;
            
            await _dbContext.SaveChangesAsync();
            
            return request;
        }
    }
}
