using System;
using System.Threading.Tasks;
using DataModels.RequestModels;
using GroupsAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GroupsAPI.Controllers
{
    [Produces("application/json")]
    public class GroupController: Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("api/groups")]
        public async Task<IActionResult> GetAllGroupsAsync()
        {
            try
            {
                return Json(await _groupService.GetAllGroupsAsync());
            }
            catch (Exception ex)
            {
                //Can add logger
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("api/group")]
        public async Task<IActionResult> GetGroupByIdAsync([FromQuery] long groupId)
        {
            try
            {
                return Json(await _groupService.GetGroupAsync(groupId));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                //Can add logger
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("api/group")]
        public async Task<IActionResult> AddGroupAsync([FromQuery] GroupRequestModel request)
        {
            try
            {
                return Json(await _groupService.AddGroupAsync(request));
            }
            catch (Exception ex)
            {
                //Can add logger
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("api/group")]
        public async Task<IActionResult> UpdateGroupAsync([FromBody] GroupRequestModel requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Json(await _groupService.UpdateGroupAsync(requestModel));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                //Can add logger
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
