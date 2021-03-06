﻿using System;
using System.Threading.Tasks;
using DataModels.RequestModels;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Services.Abstractions;

namespace StudentsAPI.Controllers
{
    [Produces("application/json")]
    public class StudentsController: Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("gateway/students")]
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

        [HttpGet("gateway/students/{groupId}")]
        public async Task<IActionResult> GetStudentsOfGroupAsync(long groupId)
        {
            try
            {
                return Json(await _studentService.GetStudentsOfGroup(groupId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("gateway/student")]
        public async Task<IActionResult> GetStudentByIdAsync([FromQuery] long studentId)
        {
            try
            {
                return Json(await _studentService.GetStudentAsync(studentId));
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

        [HttpPost("gateway/student")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentMessage request)
        {
            try
            {
                return Json(await _studentService.AddStudentAsync(request));
            }
            catch (Exception ex)
            {
                //Can add logger
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("gateway/student")]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] StudentMessage requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Json(await _studentService.UpdateStudentAsync(requestModel));
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
