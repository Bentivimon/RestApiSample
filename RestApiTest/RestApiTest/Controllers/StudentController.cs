using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApiTest.Data.Models;
using RestApiTest.Services.Abstractions;

namespace RestApiTest.Controllers
{
    [Produces("application/json")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("api/students")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            try
            {
                return Json(await _studentService.GetAllStudentsAsync());
            }
            catch (Exception ex)
            {
                //Can add logger
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("api/student/{studentId}")]
        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] long studentId)
        {
            try
            {
                return Json(await _studentService.GetStudentByIdAsync(studentId));
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

        [HttpPost("api/student")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentEntity student)
        {
            try
            {
                return Json(await _studentService.AddStudentAsync(student));
            }
            catch (Exception ex)
            {
                //Can add logger
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("api/student")]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] StudentEntity student)
        {
            try
            {
                return Json(await _studentService.UpdateStudentAsync(student));
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

        [HttpDelete("api/student/{studentId}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] long studentId)
        {
            try
            {
                await _studentService.DeleteStudentAsync(studentId);
                return Ok();
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
