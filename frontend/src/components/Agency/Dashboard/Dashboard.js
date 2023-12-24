import React, { useContext } from "react";
import AuthContext from "../../../contexts/auth-context.js";
import { useNavigate } from "react-router-dom";
import NavBar from "../../NavBar/NavBar.js";
import { Box, Button } from "@mui/material";
import {
  FormatListBulleted,
  ShoppingCart,
  LocalMall,
} from "@mui/icons-material";

const Dashboard = () => {
  const authCtx = useContext(AuthContext);
  const navigate = useNavigate();
  const role = authCtx.role;

  const isAgencyUser = (role === "AGENCY_REGISTRATION_EMPLOYEE" || 
                        role === "AGENCY_REGISTRATION_DIRECTOR" || 
                        role === "AGENCY_CODIFICATION_LAYER" || 
                        role === "AGENCY_CODIFICATION_DIRECTOR");
  const isGovernmentUser = (role === "GOVERNMENT_PRIME_MINISTER" || 
                            role === "GOVERNMENT_DIRECTOR" || 
                            role === "GOVERNMENT_EMPLOYEE");
  // const isAgencyUser = (role === 0 ||  role === 1 || role === 2 || role === 3);
  // const isGovernmentUser = (role === 4 || role === 5 || role === 6);                         
  
  const paymentServiceHandler = async (event) => {
    navigate("/paymentServices");
  };

  const registerAgencyHandler = async () => {
    navigate("/registerAgency");
  };

  const serviceOfferItemsHandler = async () => {
    navigate("/serviceOfferItem");
  };

  return (
    <>
      <NavBar />
      {isAgencyUser && (
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "100vh",
            backgroundColor: "#243b55",
          }}
        >
          <Box sx={{ m: 2 }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "10px 20px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <FormatListBulleted
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={paymentServiceHandler}
            >
              Payment Services
            </Button>
          </Box>
          <Box sx={{ m: 2 }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "10px 20px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <LocalMall
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={registerAgencyHandler}
            >
              Register Agency
            </Button>
          </Box>
          {/* <Box sx={{ m: 2 }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "10px 20px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <FormatListBulleted
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={ordersHandler}
            >
              Orders
            </Button>
          </Box> */}
        </Box>
      )}

      {isGovernmentUser && (
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "100vh",
            backgroundColor: "#243b55",
          }}
        >
          <Box sx={{ m: 2 }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "20px 30px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <ShoppingCart
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={serviceOfferItemsHandler}
            >
              Service Offer Items
            </Button>
          </Box>
          {/* <Box sx={{ m: 2, backgroundColor: "#243b55" }}>
            <Button
              sx={{
                color: "#141e30",
                fontSize: "40px",
                padding: "20px 30px",
                "&:hover": {
                  backgroundColor: "#03e9f4",
                },
              }}
              variant="contained"
              startIcon={
                <FormatListBulleted
                  sx={{ fontSize: "40px", width: "40px", height: "40px" }}
                />
              }
              size="large"
              color="primary"
              onClick={ordersHandler}
            >
              Orders
            </Button>
          </Box> */}
        </Box>
      )}
    </>
  );
};

export default Dashboard;
