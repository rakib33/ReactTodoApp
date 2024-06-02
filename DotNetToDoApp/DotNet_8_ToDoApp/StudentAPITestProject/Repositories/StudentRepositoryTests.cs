using DotNet_8_ToDoApp.DataContext;
using DotNet_8_ToDoApp.Models;
using DotNet_8_ToDoApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPITestProject.Repositories
{
    public class StudentRepositoryTests
    {
        private readonly DbContextOptions<StudentDbContext> _options;

        public StudentRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "StudentTestDb")
                .Options;
        }

        [Fact]
        public async Task GetStudents_ReturnsAllStudents()
        {
            using (var context = new StudentDbContext(_options))
            {
                context.students.Add(new Student { Id = 1, Name = "John" });
                context.students.Add(new Student { Id = 2, Name = "Jane" });
                await context.SaveChangesAsync();
            }

            using (var context = new StudentDbContext(_options))
            {
                var repository = new StudentRepository(context);
                var students = await repository.GetStudents();

                Assert.Equal(2, students.Count);
            }
        }

        [Fact]
        public async Task SaveStudent_AddsStudentToDatabase()
        {
            using (var context = new StudentDbContext(_options))
            {
                var repository = new StudentRepository(context);
                var student = new Student { Name = "Mark" };
                await repository.SaveStudent(student);

                var savedStudent = await context.students.FirstOrDefaultAsync(s => s.Name == "Mark");
                Assert.NotNull(savedStudent);
            }
        }

        [Fact]
        public async Task UpdateStudent_UpdatesExistingStudent()
        {
            using (var context = new StudentDbContext(_options))
            {
                var student = new Student { Id = 1, Name = "John"};
                context.students.Add(student);
                await context.SaveChangesAsync();

                var repository = new StudentRepository(context);
                student.Name = "John Updated";
                await repository.UpdateStudent(student);

                var updatedStudent = await context.students.FindAsync(1);
                Assert.Equal("John Updated", updatedStudent.Name);
            }
        }

        [Fact]
        public async Task DeleteStudent_RemovesStudentFromDatabase()
        {
            using (var context = new StudentDbContext(_options))
            {
                var student = new Student { Id = 1, Name = "John"};
                context.students.Add(student);
                await context.SaveChangesAsync();

                var repository = new StudentRepository(context);
                await repository.DeleteStudent(1);

                var deletedStudent = await context.students.FindAsync(1);
                Assert.Null(deletedStudent);
            }
        }

        // Add more tests for other methods as needed
    }
}
