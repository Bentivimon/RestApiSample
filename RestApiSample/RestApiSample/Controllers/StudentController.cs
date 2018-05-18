using DataModels.RequestModels;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [Produces("application/json")]    
    public class StudentController : Controller
    {
        private readonly IStudentsClient _studentClient;
        private readonly ILogger _logger;

        public StudentController(IStudentsClient studentClient, ILogger<StudentController> logger)
        {
            _studentClient = studentClient;
            _logger = logger;
        }

        [HttpGet("api/students")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            try
            {
                return Json(await _studentClient.GetStudentsAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("api/student")]
        public async Task<IActionResult> GetStudentByIdAsync([FromQuery] long studentId)
        {
            try
            {
                return Json(await _studentClient.GetStudentByIdAsync(studentId));
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

        [HttpPost("api/student")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentMessage request)
        {
            try
            {
                return Json(await _studentClient.AddStudentAsync(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("api/student")]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] StudentMessage requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Json(await _studentClient.UpdateStudentAsync(requestModel));
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
    }
}