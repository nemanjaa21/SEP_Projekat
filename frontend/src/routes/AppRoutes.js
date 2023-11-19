import React, { useContext } from "react";
import { Routes, Route } from "react-router-dom";
import AuthContext from "../contexts/auth-context.js";
import Login from "../components/Login/Login.js";
import Dashboard from "../components/Dashboard/Dashboard.js";


const AppRoutes = () => {
    const authCtx = useContext(AuthContext);
    const isLoggedIn = authCtx.isLoggedIn;
  
    return (
      <Routes>
        <Route path="/" element={isLoggedIn ? <Dashboard /> : <Login />} />
        
      </Routes>
    );
  };
  
  export default AppRoutes;