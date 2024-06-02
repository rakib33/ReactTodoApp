using DotNet_8_ToDoApp.DataContext;
using DotNet_8_ToDoApp.Interfaces;
using DotNet_8_ToDoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet_8_ToDoApp.Repositories
{
    public class StudentRepository : IStudentRepository
    { 
        private readonly StudentDbContext _context;
        public StudentRepository(StudentDbContext studentDbContext) { 
          _context = studentDbContext;
        }
        public async Task<bool> DeleteStudent(int id)
        {
            var student = _context.students.Where(t => t.Id == id).FirstOrDefault();
            if (student != null)
            {
                _context.students.Remove(student);
                return true;
            }
            return false;
        }

        public async Task<Student> GetStudentById(int id)
        {       
           return  await _context.students.FindAsync(id);
        }

        public async Task<List<Student>> GetStudents()
        {        
          return await _context.students.ToListAsync();           
        }

        public async Task<Student> SaveStudent(Student student)
        {
            _context.students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return student;
        }
    }
}
