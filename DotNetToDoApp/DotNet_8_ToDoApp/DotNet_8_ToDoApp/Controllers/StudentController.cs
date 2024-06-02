using DotNet_8_ToDoApp.Interfaces;
using DotNet_8_ToDoApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DotNet_8_ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            try
            {
                var students = await _studentRepository.GetStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                // Log the exception (logging is not shown here)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentById(id);
                if (student == null)
                {
                    return NotFound();
                }

                return student;
            }
            catch (Exception ex)
            {
                // Log the exception (logging is not shown here)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> SaveStudent(Student student)
        {
            try
            {
                var createdStudent = await _studentRepository.SaveStudent(student);
                return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
            }
            catch (Exception ex)
            {
                // Log the exception (logging is not shown here)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            //if (id != student.Id)
            //{
            //    return BadRequest();
            //}

            try
            {
                await _studentRepository.UpdateStudent(student);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (logging is not shown here)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentRepository.DeleteStudent(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (logging is not shown here)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
