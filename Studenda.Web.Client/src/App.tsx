import React from 'react';
import './styles/app.css';
import { BrowserRouter, Navigate, Route } from 'react-router-dom';
import Login from './pages/loginpage';
import Form1 from './pages/formpage';
import AppRouter from "./components/AppRouter"
import Navbar from './components/Navbar';

function App() {
  return (
    <div>
      <BrowserRouter>
        <Navbar/>
        <AppRouter/>
      </BrowserRouter>
    </div>
  )
}

export default App
