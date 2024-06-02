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