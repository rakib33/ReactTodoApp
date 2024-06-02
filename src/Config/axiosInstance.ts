// axiosInstance.ts
import axios, {AxiosInstance, AxiosResponse, AxiosError } from "axios";
import baseURL from './apiConfig';

const axiosInstance: AxiosInstance = axios.create({
  baseURL,
  timeout: 5000, // adjust as needed
});

export default axiosInstance;
