using System.ComponentModel.DataAnnotations;

namespace DotNet_8_ToDoApp.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
