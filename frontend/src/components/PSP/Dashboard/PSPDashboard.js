import React, { useState } from 'react';
import { Typography, Container, List, ListItem, Select, MenuItem, Button } from '@mui/material';
import { getServiceOffer } from '../../../services/PSPService';

const PSPDashboard = () => {
  const [serviceOffer, setServiceOffer] = useState(null); 
  const [selectedPayment, setSelectedPayment] = useState(''); 

  useEffect(() => {
    const id = localStorage.getItem("serviceOfferId");
    const getServiceOffer = async () => {
      try {
        const data = await getServiceOffer(id);
        setServiceOffer(data); 
      } catch (error) {
        console.error('Greška pri dohvatanju ServiceOfferItem-a:', error);
      }
    };

    getServiceOffer();
  }, []);

  const handlePaymentChange = (event) => {
    setSelectedPayment(event.target.value); 
  };

  const handleRedirect = () => {
    switch (selectedPayment) {
      case 'Card':
        window.location.href = 'link-to-credit-card-payment-page'; 
        break;
      case 'Paypal':
        window.location.href = 'link-to-paypal-payment-page'; 
        break;
      case 'Bitcoin':
        window.location.href = 'link-to-bitcoin-payment-page'; 
        break;
      case 'QR Code':
        window.location.href = 'link-to-qr-code-payment-page';
        break;
      default:
        break;
    }
  };

  return (
    <Container maxWidth="sm">
      <Typography variant="h4" align="center" gutterBottom>
        Offer review
      </Typography>
      <List>
        {serviceOffer.ServiceOfferItems.map((offer) => (
          <ListItem key={offer.id}>
            {offer.OfferName} - Monthly Price: {offer.MonthlyPrice}, Yearly Price: {offer.YearlyPrice}
          </ListItem>
        ))}
      </List>
      <TextField
        id="total-price"
        label="Total Price"
        variant="outlined"
        margin="normal"
        value={serviceOffer.TotalPrice}
        disabled
        fullWidth
      />
      <Typography variant="body1" align="center" gutterBottom>
        Choose payment method:
      </Typography>
      <Select value={selectedPayment} onChange={handlePaymentChange} fullWidth variant="outlined" margin="normal">
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
