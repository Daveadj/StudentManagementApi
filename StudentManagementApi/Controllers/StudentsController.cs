﻿using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.Contracts;
using StudentManagementApi.Dto;
using StudentManagementApi.Mappings;
using StudentManagementApi.RequestFeatures;

namespace StudentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Get All Students
        /// </summary>
        /// <param name="studentParameters"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAllStudents([FromQuery] StudentParameters studentParameters)
        {
            var students = await _studentRepository.GetAllStudents(studentParameters);
            return Ok(students);
        }

        /// <summary>
        /// Add Student
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newStudent = studentDto.MapDtoToStudentClass();
            await _studentRepository.CreateStudent(newStudent);
            return CreatedAtRoute("GetStudenById", new { id = newStudent.Id }, newStudent);
        }

        /// <summary>
        /// Get Student By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetStudenById")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        /// <summary>
        /// Delete Student Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStudent(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            await _studentRepository.DeleteStudent(student.Id);
            return Ok("Deleted Successfully");
        }

        /// <summary>
        /// Update Student Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentDto"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentDetails(int id, [FromBody] AddStudentDto studentDto)
        {
            var student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            student.MapUpdatDtoToStudentClass(studentDto);
            await _studentRepository.UpdateStudent(student);

            return Ok(student);
        }
    }
}