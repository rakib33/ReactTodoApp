# DotNet Core 8 & ReactTodoApp
This is basic react js todo app for beginner. 

## Install packages

- Instal [Node](https://nodejs.org/en/download/package-manager)
- Open the directory where you want to create your first react app.
- Open cmd or powershell comand window.
- Now check npm and npx version

  ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/bdd62d73-9fb0-425f-b330-8c89a80a7a61)

## Create react project

- Install npm globally, **npm install -g create-react-app**
  
  ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/f551bea4-3685-481e-a9ea-354ab206ad47)

- Now , we will create our firts react app using typescript template using create-react-app command
- create todo-app, **npx create-react-app todo-app --template typescript**
  
  ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/bf62cec1-ce26-415e-9dd4-2b24bd9f13ac)

- In command promt after successfully creating todo-app it will display the basic npm command
  
  ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/e404c1de-6e61-4fea-9adf-4c9bf976e67b)
     
- We need to go todo-app folder to run our app.

  ```
   cd todo-app
   npm start
  ```
 - Now, our app will open on default browser on your pc.
 - Open the app in visual studio code. **code .**
   
   ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/349df927-e4aa-4d4c-917e-199ee4d88418)

 - The folder structure looks like
   
   ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/d51085e2-24c6-4306-ae4d-379f0888e9ba)

 -  If vs code have an error like **Cannot use JSX unless the '--jsx' flag is provided** press **ctrl + shift + P**
 -  select **TypeScript: Select Typescript version** and then select **use workspace version**
   
   ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/cca05daa-5c08-4d37-af5e-1e1d6162ef52)

   ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/d77ec6e7-285d-42bd-bc5c-871fbbdd6ab6)

  - Now to fetching api we are using axios . install axios

    ```
     npm install axios
    ```
  ## Config
  
  - Create a folder Config, Create a file named **apiConfig.ts** where you can define your API URLs and the baseURL. 

    ```
    // apiConfig.ts
    const baseURL ='http://api.example.com';
    
    export const apiEndPoint ={
     getStudentsApi:baseURL + '/get',
     postStudentAPi: baseURL + '/post',
     putStudentApi:baseURL + '/put',
     deleteStudentApi: baseURL +'delete'
    };
    
    export default baseURL;
    ```
  - Add  **axiosInstance.ts** file under Config folder for api calling. We can reuse this axios 
    instance in our services.
    
    ```
    // axiosInstance.ts
    import axios, {AxiosInstance, AxiosResponse, AxiosError } from "axios";
    import baseURL from './apiConfig';
    
    const axiosInstance: AxiosInstance = axios.create({
      baseURL,
      timeout: 5000, // adjust as needed
    });
    
    export default axiosInstance;

    ```
  - create another **status.ts** class for status code or response message.

    ```
    export const AppStatusMessage ={
    Fetch_Faild_Msg :'Failed to fetch data'
    }
    ```
  - Create a **Model** folder and create **Student.ts** model interface.

    ```
    export interface Student{
    id: number,
    Name: string,
    }
    ```
  
  ## Service
  
  - Now create a folder **Services** and create **StudentService.ts**  
  
    ```
    import baseURL ,{apiEndPoint} from "../Config/apiConfig";
    import {Student} from "../Model/Student";
    import axiosInstance from "../Config/axiosInstance";
    import { AppStatusMessage } from "../Config/status";
    
    interface StudentApiServiceInterface{
        fetchStudentData<T>(endPoint: string): Promise<T>
    }
    
    class StudentApiService implements StudentApiServiceInterface{
      
        async fetchStudentData<T>(endPoint: string): Promise<T>{
            try{
                const response = await axiosInstance.get(endPoint);
                return response.data;
            }catch(error){
                throw new Error(AppStatusMessage.Fetch_Faild_Msg + ': ${error}');
            }
        }
    
        async fetchStudents(): Promise<Student[]>{
            try{
                return await this.fetchStudentData<Student[]>(apiEndPoint.getStudentsApi);
            }catch(error){
                throw new Error(AppStatusMessage.Fetch_Faild_Msg + ': ${error}'); 
            }
        }
    }    
    export default new StudentApiService();
    ```
   ## Components

   - Create a folder **Componets** and add our **StudentComponents.tsx** file

     ```
      import React, {useState , useEffect} from 'react';
      import StudentService from '../Services/StudentService';
      import { apiEndPoint } from '../Config/apiConfig';
      import {Student} from '../Model/Student';
      
      const StudentComponet : React.FC = () => {
      
          const [students,setStudents] = useState<Student[] | null> (null);
          const [error,setError] = useState<any> (null);
         
          useEffect(()=>{
                  const fetchStudents = async () => {
                      try{
                          const studentData = await StudentService.fetchStudentData<Student[]>(apiEndPoint.getStudentsApi);
                          setStudents(studentData);
                      }catch(error){
                          setError(error);
                      }
                  };
                  fetchStudents();
          
          },[]);
      
          return (
              <div>
                {error && <div>Error: {error}</div>}
                {students && (
                  <div>
                    <h1>Students</h1>
                    <ul>
                      {students.map(student => (
                        <li key={student.id}>{student.Name}</li>
                      ))}
                    </ul>
                  </div>
                )}
              </div>
            );
      };
     ```
   
   ## Routing

   -  Install React Router

     ```
     npm install react-router-dom
     ```
   - Set Up Router.Open file named App.tsx (create if you haven't already) and set up the router.

     ```
      import React from 'react';
      import logo from './logo.svg';
      import './App.css';
      import { Routes, Route, BrowserRouter } from 'react-router-dom';
      import StudentComponet from './Components/StudentComponent';
      import DashboardComponent from './Components/DashboardComponent';
      import NotFoundComponent from './Components/NotFound';
      
      const App: React.FC = () =>{
        return(
          <BrowserRouter>
          <Routes>   
            <Route path="/" Component={DashboardComponent} />
            <Route path="/student" Component={StudentComponet} />
            <Route Component={NotFoundComponent} />
        </Routes>
        </BrowserRouter>
        );
      }
      export default App;
     ```
   
   # DotNet 8 ToDo-App

   - Create a project Solution name **DotNet_8_ToDoApp** or your prefarable name.
   - In this project we using SQL Server Database
   - Add nuget package
     - **Microsoft.EntityFrameworkCore**
     - **Microsoft.EntityFrameworkCore.SqlServer**
     - **Microsoft.EntityFrameworkCore.Tools**
     
     ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/035f524e-a33c-4859-9e17-c3e01f7fa939)
   
   - Create a local database connection string in appsettings.json file

     ```
     {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "ConnectionString": {
        "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True"
      }
     }
     ```
   - Create folder Models and create a class **Student** and **ConnectionModel**

     ```
      public class Student
      {
          [Key]
          public int Id { get; set; }
          public string Name { get; set; }
      }

     public class ConnectionModel
      {
          public string DefaultConnection { get; set; }
      }
     ```
   - Create a folder **DataContext** and add **StudentDbContext** class

     ```
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
     ```
  - Create Interfaces Folder and IStudentRepository Interface

    ```
     public interface IStudentRepository
     {
         Task<List<Student>> GetStudents();
         Task<Student> SaveStudent(Student student);
         Task<Student> GetStudentById(int id);
         Task<Student> UpdateStudent(Student student);
         Task DeleteStudent(int id);
     }
    ```
  - Add folder Repositories and create file **StudentRepository.cs**

    ```
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
    ```

  - Create a controller class **StudentController.cs**

    ```    
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
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

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
    ``` 
  - Open Program.cs file and add dependency Injection . I also add Cors. 

    ```
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
          options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
         );
      builder.Services.AddScoped<IStudentRepository,StudentRepository>();
      
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

    ```
   ## Database Code First Migration

    - open package manager console 
    - run command **add-migration Initail-Commit
    
    ![image](https://github.com/rakib33/ReactTodoApp/assets/10026710/b1f1d788-4a96-4e8d-add3-ded5b5492684)

    - run **update-database**
    - Database was created.
   
   ## xUnit Test
   
   ## References
   - https://www.c-sharpcorner.com/article/reactjs-crud-using-net-core-web-api/
   - https://medium.com/@jaydeepvpatil225/product-management-application-using-net-core-6-and-react-js-with-crud-operation-1f8bb9f709ba
