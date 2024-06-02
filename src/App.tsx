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
      <Route path="/" Component={DashboardComponent}  />
      <Route path="/student" Component={StudentComponet} element={<StudentComponet/>} />
      <Route path="/dashboard" Component={DashboardComponent} element={<DashboardComponent/>} />
      <Route path="*" Component={NotFoundComponent} element={<NotFoundComponent/>} />
  </Routes>
  </BrowserRouter>
  );
}
export default App;
