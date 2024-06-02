using DotNet_8_ToDoApp.DataContext;
using DotNet_8_ToDoApp.Interfaces;
using DotNet_8_ToDoApp.Models;
using DotNet_8_ToDoApp.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins",
    builder =>
    {
        builder.WithOrigins("http://192.168.1.7:3000/")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});
// Add services to the container.
//Configure IOptions
builder.Services.Configure<ConnectionModel>(builder.Configuration.GetSection("ConnectionString"));

builder.Services.AddControllers();
//Configure database context
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True")
   );
builder.Services.AddScoped<IStudentRepository,StudentRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
