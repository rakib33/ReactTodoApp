using DotNet_8_ToDoApp.Controllers;
using DotNet_8_ToDoApp.Interfaces;
using DotNet_8_ToDoApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPITestProject.Controllers
{
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly StudentController _controller;

        public StudentsControllerTests()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _controller = new StudentController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetStudents_ReturnsOkResult_WithListOfStudents()
        {
            // Arrange
            var students = new List<Student>
            {
            new Student { Id = 1, Name = "John"},
            new Student { Id = 2, Name = "Jane" }
            };
            _mockRepo.Setup(repo => repo.GetStudents()).ReturnsAsync(students);

            // Act
            var result = await _controller.GetStudents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnStudents = Assert.IsType<List<Student>>(okResult.Value);
            Assert.Equal(2, returnStudents.Count);
        }

        [Fact]
        public async Task SaveStudent_ReturnsCreatedAtActionResult_WithStudent()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "John" };
            _mockRepo.Setup(repo => repo.SaveStudent(It.IsAny<Student>())).ReturnsAsync(student);

            // Act
            var result = await _controller.SaveStudent(student);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnStudent = Assert.IsType<Student>(createdAtActionResult.Value);
            Assert.Equal(student.Id, returnStudent.Id);
        }

        [Fact]
        public async Task UpdateStudent_ReturnsNoContentResult()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "John"};
            _mockRepo.Setup(repo => repo.UpdateStudent(It.IsAny<Student>())).ReturnsAsync(student);

            // Act
            var result = await _controller.UpdateStudent(1, student);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateStudent_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "John" };

            // Act
            var result = await _controller.UpdateStudent(2, student);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        //[Fact]
        //public async Task DeleteStudent_ReturnsNoContentResult()
        //{
        //    // Arrange
        //    _mockRepo.Setup(repo => repo.DeleteStudent(1)).Returns();

        //    // Act
        //    var result = await _controller.DeleteStudent(1);

        //    // Assert
        //    Assert.IsType<NoContentResult>(result);
        //}

        [Fact]
        public async Task DeleteStudent_ReturnsNotFoundResult_WhenStudentDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteStudent(1)).Throws(new KeyNotFoundException());

            // Act
            var result = await _controller.DeleteStudent(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Add more tests for other methods as needed
    }
}
