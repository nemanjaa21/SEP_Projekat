import React, { useContext } from "react";
import { Routes, Route } from "react-router-dom";
import AuthContext from "../contexts/auth-context.js";
import Login from "../components/Agency/Login/Login.js";
import AgencyDashboard from "../components/Agency/Dashboard/AgencyDashboard.js";

const AppRoutes = () => {
    const authCtx = useContext(AuthContext);
    const isLoggedIn = authCtx.isLoggedIn;
  
    return (
      <Routes>
        <Route path="/" element={isLoggedIn ? <AgencyDashboard /> : <Login />} />
        
      </Routes>
    );
  };
  
  export default AppRoutes;