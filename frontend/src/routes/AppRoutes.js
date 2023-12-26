import React, { useContext } from "react";
import { Routes, Route } from "react-router-dom";
import AuthContext from "../contexts/auth-context.js";
import Login from "../components/Agency/Login/Login.js";
import Dashboard from "../components/Agency/Dashboard/Dashboard.js";
import PSPDashboard from "../components/PSP/Dashboard/PSPDashboard.js";
import ServiceOfferItem from "../components/Agency/ServiceOfferItem/ServiceOfferItem.js";
import PaymentServices from "../components/Agency/PaymentServices/PaymentServices.js";
import CardForm from "../components/CardForm/CardForm.js";
import BankSuccess from "../components/Bank/BankSuccess.js";
import QRCode from "../components/QRCode/QRCode.js";

const AppRoutes = () => {
    const authCtx = useContext(AuthContext);
    const isLoggedIn = authCtx.isLoggedIn;
  
    return (
      <Routes>
        <Route path="/" element={isLoggedIn ? <Dashboard /> : <Login />} />
        <Route path="/serviceOfferItem" element={<ServiceOfferItem />} />
        <Route path="/paymentServices" element={<PaymentServices />}/>
        <Route path="/pspDashboard" element={<PSPDashboard />} />
        <Route path="/bank/card" element={<CardForm />} />
        <Route path="/bank/success" element={<BankSuccess />} />
        <Route path="/bank/qrcode" element={<QRCode />} />
      </Routes>
    );
  };
  
  export default AppRoutes;