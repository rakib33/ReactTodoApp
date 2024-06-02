using DotNet_8_ToDoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet_8_ToDoApp.DataContext
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) 
        { 
        
        } 
        public DbSet<Student> students {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().ToTable("Student").HasKey(s=>s.Id);
        }
    }
}
