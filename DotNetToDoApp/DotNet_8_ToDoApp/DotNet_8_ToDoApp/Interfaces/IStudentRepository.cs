using DotNet_8_ToDoApp.Models;

namespace DotNet_8_ToDoApp.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();
        Task<Student> SaveStudent(Student student);
        Task<Student> GetStudentById(int id);
        Task<Student> UpdateStudent(Student student);
        Task<bool> DeleteStudent(int id);
    }
}
