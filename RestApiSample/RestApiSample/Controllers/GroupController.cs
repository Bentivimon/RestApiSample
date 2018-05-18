using System;
using System.Threading.Tasks;
using DataModels.RequestModels;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gateway.Controllers
{
    [Produces("application/json")]    
    public class GroupController : Controller
    {
        private readonly IGroupClient _groupClient;
        private readonly IStudentsClient _studentsClient;
        private readonly ILogger _logger;

        public GroupController(IGroupClient groupClient, IStudentsClient studentsClient, ILogger<GroupController> logger)
        {
            _groupClient = groupClient;
            _studentsClient = studentsClient;
            _logger = logger;
        }

        [HttpGet("api/groups")]
        public async Task<IActionResult> GetAllGroupsAsync()
        {
            try
            {
                return Json(await _groupClient.GetAllGroupsAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("api/group")]
        public async Task<IActionResult> GetGroupByIdAsync([FromQuery] long groupId)
        {
            try
            {
                return Json(await _groupClient.GetGroupByIdAsync(groupId));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("api/group")]
        public async Task<IActionResult> AddGroupAsync([FromQuery] GroupMessage request)
        {
            try
            {
                return Json(await _groupClient.AddGroupAsync(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("api/group")]
        public async Task<IActionResult> UpdateGroupAsync([FromBody] GroupMessage requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Json(await _groupClient.UpdateGroupAsync(requestModel));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("api/group/students")]
        public async Task<IActionResult> GetGroupWithStudents([FromQuery] long groupId)
        {
            try
            {
                var groupTask = _groupClient.GetGroupByIdAsync(groupId);
                var studentsOfGroupTask = _studentsClient.GetStudentsOfGroupAsync(groupId);

                await Task.WhenAll(groupTask, studentsOfGroupTask);

                var groupMessage = new ExtendedGroupMessage()
                {
                    Id = groupTask.Result.Id,
                    FacultyId = groupTask.Result.FacultyId,
                    CountOfStudents = groupTask.Result.CountOfStudents,
                    Name = groupTask.Result.Name,
                };

                groupMessage.Studnets = studentsOfGroupTask.Result;

                return Json(groupMessage);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}