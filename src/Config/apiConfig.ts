// apiConfig.ts

const baseURL ='http://api.example.com';

export const apiEndPoint ={
 getStudentsApi:baseURL + '/get',
 postStudentAPi: baseURL + '/post',
 putStudentApi:baseURL + '/put',
 deleteStudentApi: baseURL +'delete'
};

export default baseURL;