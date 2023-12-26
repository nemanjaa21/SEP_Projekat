import React from 'react';
import {
  Container,
  Box
} from "@mui/material";
import NavBar from '../NavBar/NavBar';

const BankSuccess = () => {
  return (
    <>
    <NavBar />
      <Box sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        backgroundColor: "#243b55",
      }}>
        <Container maxWidth="sm">
          <div style={{ width: '100%', height: '100vh', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
            <img src="https://static.vecteezy.com/system/resources/previews/019/520/923/original/payment-success-paid-bill-concept-illustration-flat-design-eps10-modern-graphic-element-for-landing-page-empty-state-ui-infographic-icon-vector.jpg" alt="Payment Success" style={{ maxWidth: '100%', maxHeight: '100%' }} />
          </div>
        </Container>
      </Box>
    </>
  );
};

export default BankSuccess;