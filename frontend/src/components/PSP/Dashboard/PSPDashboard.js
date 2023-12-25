import React, { useState, useEffect } from "react";
import {
  Typography,
  Container,
  List,
  ListItem,
  Select,
  MenuItem,
  Button,
  TextField,
  Box
} from "@mui/material";
import NavBar from "../../NavBar/NavBar.js";
import {
  bitcoinPayment,
  payPalPayment,
  processPayment,
  qrCodePayment,
} from "../../../services/PSPService.js";
import { getServiceOfferById } from "../../../services/AgencyService.js";
import { getAllPaymentServices } from "../../../services/AgencyService.js";

const PSPDashboard = () => {
  const [serviceOffer, setServiceOffer] = useState(null);
  const [selectedPayment, setSelectedPayment] = useState("");
  const [paymentServices, setPaymentServices] = useState([]);

  useEffect(() => {
    const id = localStorage.getItem("serviceOfferId");
    const getServiceOffer = async () => {
      try {
        const response = await getServiceOfferById(id);
        setServiceOffer(response.data);
      } catch (error) {
        console.error("Greška pri dohvatanju ServiceOfferItem-a:", error);
      }
    };

    const getPaymentServices = async () => {
      try {
        const response = await getAllPaymentServices(1);
        setPaymentServices(response.data);
      }
      catch (error) {
        console.error("Greška pri dohvatanju PaymentServices-a:", error);
      }
    }

    getPaymentServices();
    getServiceOffer();
  }, []);

  const getCardPayment = async () => {
    try {
      let pspRequest = {
        amount: serviceOffer.totalPrice.toFixed(2),
        paymentTypeId: 5
      }
      const response = await processPayment(pspRequest, process.env.REACT_APP_MERCHANT_API_KEY);
      return response.data;
    } catch (error) {
      console.error("Greška pri placanju karticom:", error);
    }
  };

  const getBitcoinPayment = async () => {
    try {
      const response = await bitcoinPayment();
      return response.data;
    } catch (error) {
      console.error("Greška pri bitcoin placanju:", error);
    }
  };

  const getQrServicePayment = async () => {
    try {
      const response = await qrCodePayment();
      return response.data;
    } catch (error) {
      console.error("Greška pri QR placanju:", error);
    }
  };

  const getPayPalServicePayment = async () => {
      try {
         const response = await payPalPayment(serviceOffer);
         window.location.replace(response.data); 
      }
      catch(error) {
          console.log("Error processing PaypalPayment: ", error);
      }
  };

  const handlePaymentChange = (event) => {
    setSelectedPayment(event.target.value);
  };

  const handleRedirect = async () => {
    let paymentResult = '';
    switch (selectedPayment) {
      case "Credit Card Payment":
        paymentResult = await getCardPayment();
        break;
      case "Bitcoin Payment":
        paymentResult = await getBitcoinPayment();
        break;
      case "QR Code Payment":
        paymentResult = await getQrServicePayment();
        break;
      case "PayPal Payment":
        paymentResult = await getPayPalServicePayment();
        break;
      default:
        break;
    }
  };

  function checkOfferName(eOfferName) {
    if (eOfferName === 0) {
      return "Codification of laws";
    } else if (eOfferName === 1) {
      return "Publication of laws on internet";
    } else if (eOfferName === 2) {
      return "Issuance of laws printed form";
    } else if (eOfferName === 3) {
      return "Issuance of laws electronic form";
    } else {
      return "Unknown";
    }
  }

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
      <Typography variant="h4" align="center" gutterBottom>
        Offer review
      </Typography>
      {serviceOffer && serviceOffer.serviceOfferItems ? (
        <>
          <List>
            {serviceOffer.serviceOfferItems.map((offer) => (
              <ListItem key={offer.id}>
                {checkOfferName(offer.offerName)} - Price ${offer.selectedPrice.toFixed(2)}
              </ListItem>
            ))}
          </List>
          <TextField
            id="total-price"
            label="Total Price"
            variant="outlined"
            margin="normal"
            value={`$${serviceOffer.totalPrice.toFixed(2)}`}
            disabled
            fullWidth
          />
        </>
      ) : (
        <Typography variant="body1" align="center">
          Loading...
        </Typography>
      )}
      <Typography variant="body1" align="center" gutterBottom>
        Choose payment method:
      </Typography>
      <Select
        value={selectedPayment}
        onChange={handlePaymentChange}
        fullWidth
        variant="outlined"
      >
        {paymentServices.filter(service => service.subscribed).map(service => (
          <MenuItem key={service.id} value={service.name}>
            {service.name}
          </MenuItem>
        ))}
      </Select>
      <Button variant="contained" onClick={handleRedirect}>
        Continue on payment
      </Button>
    </Container>
      </Box>
    </>
  );
};

export default PSPDashboard;
