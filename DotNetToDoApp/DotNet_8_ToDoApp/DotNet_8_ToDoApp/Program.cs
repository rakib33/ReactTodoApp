using DotNet_8_ToDoApp.DataContext;
using DotNet_8_ToDoApp.Interfaces;
using DotNet_8_ToDoApp.Models;
using DotNet_8_ToDoApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

builder.Services.AddControllers();
//Configure database context
//builder.Services.AddDbContext<StudentDbContext>(options =>
//    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True")
//   );

// Bind ConnectionStrings section to the ConnectionStrings class
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
// Configure DbContext to use the connection string from IOptions
builder.Services.AddDbContext<StudentDbContext>((serviceProvider, options) =>
{
    var connectionStrings = serviceProvider.GetRequiredService<IOptions<ConnectionStrings>>().Value;
    options.UseSqlServer(connectionStrings.SqlServerConnectionLocaldb);
});

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
