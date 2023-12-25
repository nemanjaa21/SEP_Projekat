import "./index.css";
import App from "./App";
import { AuthContextProvider } from "./contexts/auth-context";
import { BrowserRouter } from "react-router-dom";
import { createRoot } from "react-dom/client";
import { ToastContainer } from "react-toastify";

createRoot(document.getElementById("root")).render(
  <BrowserRouter>
    
      <AuthContextProvider>
          <App />
        <ToastContainer />
      </AuthContextProvider>
    
  </BrowserRouter>
);