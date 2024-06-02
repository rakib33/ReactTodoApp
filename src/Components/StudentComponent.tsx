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

export default StudentComponet;