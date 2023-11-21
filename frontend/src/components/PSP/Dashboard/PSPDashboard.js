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
} from "@mui/material";
import {
  bitcoinPayment,
  creditCardPayment,
  payPalPayment,
  qrCodePayment,
} from "../../../services/PSPService.js";
import { getServiceOfferById } from "../../../services/AgencyService.js";

const PSPDashboard = () => {
  const [serviceOffer, setServiceOffer] = useState(null);
  const [selectedPayment, setSelectedPayment] = useState("");

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

    getServiceOffer();
  }, []);

  const getCardPayment = async () => {
    try {
      const response = await creditCardPayment();
      console.log(response.data);
    } catch (error) {
      console.error("Greška pri placanju karticom:", error);
    }
  };

  const getBitcoinPayment = async () => {
    try {
      const response = await bitcoinPayment();
      console.log(response.data);
    } catch (error) {
      console.error("Greška pri bitcoin placanju:", error);
    }
  };

  const getQrServicePayment = async () => {
    try {
      const response = await qrCodePayment();
      console.log(response.data);
    } catch (error) {
      console.error("Greška pri QR placanju:", error);
    }
  };

  const getPayPalServicePayment = async () => {
    try {
      const response = await payPalPayment();
      console.log(response.data);
    } catch (error) {
      console.error("Greška pri PayPal placanju:", error);
    }
  };

  const handlePaymentChange = (event) => {
    setSelectedPayment(event.target.value);
  };

  const handleRedirect = () => {
    switch (selectedPayment) {
      case "Card":
        getCardPayment();
        break;
      case "Bitcoin":
        getBitcoinPayment();
        break;
      case "QR Code":
        getQrServicePayment();
        break;
      case "Paypal":
        getPayPalServicePayment();
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
    <Container maxWidth="sm">
      <Typography variant="h4" align="center" gutterBottom>
        Offer review
      </Typography>
      {serviceOffer && serviceOffer.serviceOfferItems ? (
        <>
          <List>
            {serviceOffer.serviceOfferItems.map((offer) => (
              <ListItem key={offer.id}>
                {checkOfferName(offer.offerName)} - Monthly Price: ${offer.monthlyPrice}, Yearly
                Price: ${offer.yearlyPrice}
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
        <MenuItem value="Card">Card</MenuItem>
        <MenuItem value="Paypal">Paypal</MenuItem>
        <MenuItem value="Bitcoin">Bitcoin</MenuItem>
        <MenuItem value="QR Code">QR Code</MenuItem>
      </Select>
      <Button variant="contained" onClick={handleRedirect}>
        Continue on payment
      </Button>
    </Container>
  );
};

export default PSPDashboard;
