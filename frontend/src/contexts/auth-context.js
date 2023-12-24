import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../services/AuthService.js";
import { jwtDecode } from "jwt-decode";

const AuthContext = React.createContext({
  isLoggedIn: false,
  token: "",
  role: "",
  onLogout: () => {},
  onLogin: (logInData) => {},
});

const decodeToken = (token) => {
  console.log("token", token);
  try {
    const decoded = jwtDecode(token);
    return decoded;
  } catch (error) {
    console.log("Error decoding token:", error);
    return null;
  }
};

export const AuthContextProvider = (props) => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [token, setToken] = useState("");
  const [role, setRole] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const loggedIn = sessionStorage.getItem("isLoggedIn");
    const currentToken = sessionStorage.getItem("token");
    const currentRole = sessionStorage.getItem("role");
    
    if (loggedIn === "1") {
      setIsLoggedIn(true);
      setToken(currentToken);  
      setRole(currentRole);    
    }
  }, []);

  const logInHandler = async (logInData) => {
    try {
      const response = await login(logInData);
      const decodedToken = decodeToken(response.data.token);
      let role = decodedToken.UserType;

      setIsLoggedIn(true);
      setToken(response.data.token);
      setRole(role);

      sessionStorage.setItem("isLoggedIn", "1");
      sessionStorage.setItem("token", response.data.token);
      sessionStorage.setItem("role", role);
      navigate("/");
    } catch (error) {
      alert(error);
    }
  };

  const logOutHandler = async () => {
    setIsLoggedIn(false);
    sessionStorage.removeItem("isLoggedIn");
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("role");
    navigate("/");
  };

  return (
    <AuthContext.Provider
      value={{
        isLoggedIn: isLoggedIn,
        token: token,
        role: role,
        onLogout: logOutHandler,
        onLogin: logInHandler,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthContext;