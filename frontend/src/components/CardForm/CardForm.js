import { useState } from 'react';
import { payWithCard } from '../../services/BankService';
import {
  Container,
  Button,
  TextField,
  Box
} from "@mui/material";
import NavBar from '../NavBar/NavBar';

const CardForm = () => {
  const [formData, setFormData] = useState({
    Pan: '',
    SecurityCode: '',
    CardHolderName: '',
    ExpirationDate: '',
    PaymentId: sessionStorage.getItem("paymentId"),
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await payWithCard(formData);
      window.location.href = response.data.url;
      console.log(response.data);
    } catch (error) {
      console.error('Error:', error);
    }
  };

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
      <form onSubmit={handleSubmit}>
        <Box sx={{ '& .MuiTextField-root': { m: 1, width: '100%' } }}>
          <TextField
            type="text"
            name="Pan"
            value={formData.Pan}
            label="Card Number"
            placeholder="Card Number"
            onChange={handleChange}
          />
          <TextField
            type="text"
            name="SecurityCode"
            value={formData.SecurityCode}
            label="Security Code"
            placeholder="Security Code"
            onChange={handleChange}
          />
          <TextField
            type="text"
            name="CardHolderName"
            value={formData.CardHolderName}
            label="Cardholder's Name"
            placeholder="Cardholder's Name"
            onChange={handleChange}
          />
          <TextField
            type="text"
            name="ExpirationDate"
            value={formData.ExpirationDate}
            label="Expiration Date"
            placeholder="Expiration Date"
            onChange={handleChange}
          />
        </Box>
        <Button type="submit" variant="contained" color="primary">
          Submit
        </Button>
      </form>
    </Container>
      </Box>
    </>
  );
};

export default CardForm;